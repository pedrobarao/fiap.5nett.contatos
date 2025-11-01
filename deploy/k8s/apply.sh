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

echo "Aplicando Metrics Server..."
kubectl apply -f deploy/k8s/sa-metrics.yaml

echo "Criando Horizontal Pod Autoscaler..."
kubectl apply -f deploy/k8s/cadastro-api/hpa-api.yaml
kubectl apply -f deploy/k8s/consulta-api/hpa-api.yaml

echo "Forçando rollout dos Deployments..."
kubectl -n contatos-app rollout restart deploy cadastro-contatos-api
kubectl -n contatos-app rollout restart deploy consulta-contatos-api

echo "Aguardando conclusão do rollout..."
kubectl -n contatos-app rollout status deploy cadastro-contatos-api --timeout=180s
kubectl -n contatos-app rollout status deploy consulta-contatos-api --timeout=180s

echo "Verificando recursos criados..."
kubectl get all -n contatos-app

echo "Aplicação implantada com sucesso!"
