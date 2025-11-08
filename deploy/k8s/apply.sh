#!/bin/bash
set -Eeuo pipefail
trap 'rc=$?; echo "[ERRO] Linha $LINENO: comando \"$BASH_COMMAND\" falhou (exit $rc)" >&2; exit $rc' ERR

echo "Iniciando criação de recursos Kubernetes..."

echo "Criando namespace..."
kubectl apply -f deploy/k8s/namespace.yaml

echo "Criando Secrets..."
kubectl apply -f deploy/k8s/secrets.yaml

echo "Criando Persistent Volumes Claims..."

echo "Criando Deployments..."
kubectl apply -f deploy/k8s/cadastro-api/deploy-api.yaml
kubectl apply -f deploy/k8s/consulta-api/deploy-api.yaml

echo "Criando Services..."
kubectl apply -f deploy/k8s/cadastro-api/svc-api.yaml
kubectl apply -f deploy/k8s/consulta-api/svc-api.yaml

echo "Criando Ingress Controller..."
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.1/deploy/static/provider/cloud/deploy.yaml

echo "Aguardando Ingress-NGINX Controller ficar pronto..."
kubectl rollout status -n ingress-nginx deploy/ingress-nginx-controller --timeout=180s
kubectl wait -n ingress-nginx --for=condition=available deploy/ingress-nginx-controller --timeout=60s || true

echo "Aguardando Jobs de admission concluírem..."
kubectl wait -n ingress-nginx --for=condition=complete job/ingress-nginx-admission-create --timeout=120s || true
kubectl wait -n ingress-nginx --for=condition=complete job/ingress-nginx-admission-patch --timeout=120s || true

echo "Verificando endpoints do serviço de admission..."
for i in {1..30}; do
  if kubectl get -n ingress-nginx endpoints ingress-nginx-controller-admission -o jsonpath='{.subsets[*].addresses[*].ip}' | grep -qE '.+'; then
    echo "Endpoints disponíveis."
    break
  fi
  echo "Aguardando endpoints... tentativa $i/30"
  sleep 5
  if [ "$i" -eq 30 ]; then
    echo "[ERRO] Endpoints do serviço de admission não disponíveis a tempo." >&2
    exit 1
  fi
done

echo "Aplicando Ingress da aplicação..."
kubectl apply -f deploy/k8s/ingress.yaml

echo "Aplicando Metrics Server (com Kustomize + patch de resources)..."
kubectl apply -k deploy/k8s/metrics
kubectl rollout status -n kube-system deploy/metrics-server --timeout=120s

echo "Criando Horizontal Pod Autoscaler..."
kubectl apply -f deploy/k8s/cadastro-api/hpa-api.yaml
kubectl apply -f deploy/k8s/consulta-api/hpa-api.yaml

echo "Verificando recursos criados..."
kubectl get all -n contatos-app

echo "Aplicação implantada com sucesso!"
