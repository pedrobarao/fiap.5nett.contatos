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

echo "Criando Ingress..."
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.1/deploy/static/provider/cloud/deploy.yaml
kubectl apply -f deploy/k8s/ingress.yaml

echo "Aplicando Metrics Server (com Kustomize + patch de resources)..."
mkdir -p deploy/k8s/metrics

cat > deploy/k8s/metrics/kustomization.yaml <<'EOF'
apiVersion: kustomize.config.k8s.io/v1beta1
kind: Kustomization
resources:
  - https://github.com/kubernetes-sigs/metrics-server/releases/download/v0.6.4/components.yaml
patches:
  - target:
      kind: Deployment
      name: metrics-server
      namespace: kube-system
    path: patch-resources.yaml
EOF

cat > deploy/k8s/metrics/patch-resources.yaml <<'EOF'
apiVersion: apps/v1
kind: Deployment
metadata:
  name: metrics-server
  namespace: kube-system
spec:
  template:
    spec:
      containers:
        - name: metrics-server
          resources:
            requests:
              cpu: 50m
              memory: 64Mi
            limits:
              memory: 256Mi
EOF

kubectl apply -k deploy/k8s/metrics
kubectl rollout status -n kube-system deploy/metrics-server --timeout=120s

echo "Criando Horizontal Pod Autoscaler..."
kubectl apply -f deploy/k8s/cadastro-api/hpa-api.yaml
kubectl apply -f deploy/k8s/consulta-api/hpa-api.yaml

echo "Verificando recursos criados..."
kubectl get all -n contatos-app

echo "Aplicação implantada com sucesso!"
