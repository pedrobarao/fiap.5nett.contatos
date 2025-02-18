#!/bin/bash

#chmod +x deploy/k8s/apply.sh
#chmod +x deploy/k8s/delete.sh
#chmod +x deploy/k8s/port-forward.sh

echo "Criando recursos Kubernetes..."

echo "Criando namespace..."
kubectl apply -f deploy/k8s/namespace.yaml

echo "Criando Secrets..."
kubectl apply -f deploy/k8s/secrets.yaml

echo "Criando PVCs..."
kubectl apply -f deploy/k8s/rabbitmq/rabbitmq-pvc.yaml
kubectl apply -f deploy/k8s/cadastro-api/postgres-pvc.yaml
kubectl apply -f deploy/k8s/consulta-api/mongodb-pvc.yaml
kubectl apply -f deploy/k8s/prometheus/prometheus-pvc.yaml
kubectl apply -f deploy/k8s/grafana/grafana-pvc.yaml

echo "Criando ConfigMaps..."
kubectl apply -f deploy/k8s/prometheus/prometheus-config.yaml
kubectl apply -f deploy/k8s/grafana/grafana-config.yaml

echo "Criando Deployments e Services..."
kubectl apply -f deploy/k8s/cadastro-api/postgres.yaml
kubectl apply -f deploy/k8s/cadastro-api/postgres-service.yaml
kubectl apply -f deploy/k8s/consulta-api/mongodb.yaml
kubectl apply -f deploy/k8s/consulta-api/mongodb-service.yaml
kubectl apply -f deploy/k8s/rabbitmq/rabbitmq.yaml
kubectl apply -f deploy/k8s/rabbitmq/service.yaml
kubectl apply -f deploy/k8s/prometheus/prometheus.yaml
kubectl apply -f deploy/k8s/prometheus/service.yaml
kubectl apply -f deploy/k8s/grafana/grafana.yaml
kubectl apply -f deploy/k8s/grafana/service.yaml
kubectl apply -f deploy/k8s/cadastro-api/cadastro-api.yaml
kubectl apply -f deploy/k8s/cadastro-api/service.yaml
kubectl apply -f deploy/k8s/consulta-api/consulta-api.yaml
kubectl apply -f deploy/k8s/consulta-api/service.yaml
kubectl apply -f deploy/k8s/cadastro-api/hpa.yaml
kubectl apply -f deploy/k8s/consulta-api/hpa.yaml

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
kubectl wait --namespace contatos-app \
  --for=condition=ready pod \
  --selector=app=consulta-contatos-api \
  --timeout=90s

echo "Verificando recursos criados..."
kubectl get all -n contatos-app

echo ""
echo "Para acessar os serviços, execute:"
echo "./deploy/k8s/scripts/port-forward.sh"
echo ""
echo "Credenciais:"
echo "RabbitMQ UI: admin/contatos"
echo "PostgreSQL: contatos/contatos"
echo "MongoDB: contatos/contatos"

echo "Aplicação implantada com sucesso!" 