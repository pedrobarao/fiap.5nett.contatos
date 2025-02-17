#!/bin/bash

#chmod +x deploy/k8s/apply.sh
#chmod +x deploy/k8s/delete.sh
#chmod +x deploy/k8s/port-forward.sh

echo "Criando recursos Kubernetes..."

echo "Criando namespace..."
kubectl apply -f deploy/k8s/namespace.yaml

echo "Criando PVCs..."
kubectl apply -f deploy/k8s/rabbitmq-pvc.yaml
kubectl apply -f deploy/k8s/postgres-pvc.yaml
kubectl apply -f deploy/k8s/mongodb-pvc.yaml
kubectl apply -f deploy/k8s/prometheus-pvc.yaml
kubectl apply -f deploy/k8s/grafana-pvc.yaml

echo "Criando ConfigMaps..."
kubectl apply -f deploy/k8s/prometheus-config.yaml
kubectl apply -f deploy/k8s/grafana-config.yaml

echo "Criando Deployments e Services..."
kubectl apply -f deploy/k8s/postgres.yaml
kubectl apply -f deploy/k8s/mongodb.yaml
kubectl apply -f deploy/k8s/rabbitmq.yaml
kubectl apply -f deploy/k8s/prometheus.yaml
kubectl apply -f deploy/k8s/grafana.yaml
kubectl apply -f deploy/k8s/cadastro-api.yaml

echo "Aguardando pods iniciarem..."
kubectl wait --namespace contatos-app \
  --for=condition=ready pod \
  --selector=app=rabbitmq \
  --timeout=90s
kubectl wait --namespace contatos-app \
  --for=condition=ready pod \
  --selector=app=postgres \
  --timeout=90s
kubectl wait --namespace contatos-app \
  --for=condition=ready pod \
  --selector=app=mongodb \
  --timeout=90s
kubectl wait --namespace contatos-app \
  --for=condition=ready pod \
  --selector=app=cadastro-contatos-api \
  --timeout=90s

echo "Verificando recursos criados..."
kubectl get all -n contatos-app

echo ""
echo "Para acessar os serviços, execute:"
echo "./deploy/k8s/port-forward.sh"
echo ""
echo "Credenciais:"
echo "RabbitMQ UI: admin/contatos"
echo "PostgreSQL: contatos/contatos"
echo "MongoDB: contatos/contatos"

echo "Aplicação implantada com sucesso!" 