#!/bin/bash

echo "Removendo recursos Kubernetes..."

echo "Removendo Deployments e Services..."
kubectl delete -f deploy/k8s/rabbitmq/rabbitmq.yaml
kubectl delete -f deploy/k8s/rabbitmq/service.yaml
kubectl delete -f deploy/k8s/cadastro-api/postgres.yaml
kubectl delete -f deploy/k8s/cadastro-api/postgres-service.yaml
kubectl delete -f deploy/k8s/consulta-api/mongodb.yaml
kubectl delete -f deploy/k8s/consulta-api/mongodb-service.yaml
kubectl delete -f deploy/k8s/prometheus/prometheus.yaml
kubectl delete -f deploy/k8s/prometheus/service.yaml
kubectl delete -f deploy/k8s/grafana/grafana.yaml
kubectl delete -f deploy/k8s/grafana/service.yaml
kubectl delete -f deploy/k8s/cadastro-api/cadastro-api.yaml
kubectl delete -f deploy/k8s/cadastro-api/service.yaml
kubectl delete -f deploy/k8s/consulta-api/consulta-api.yaml
kubectl delete -f deploy/k8s/consulta-api/service.yaml

echo "Removendo ConfigMaps..."
kubectl delete -f deploy/k8s/prometheus/prometheus-config.yaml
kubectl delete -f deploy/k8s/grafana/grafana-config.yaml

echo "Removendo PVCs..."
kubectl delete -f deploy/k8s/rabbitmq/rabbitmq-pvc.yaml
kubectl delete -f deploy/k8s/cadastro-api/postgres-pvc.yaml
kubectl delete -f deploy/k8s/consulta-api/mongodb-pvc.yaml
kubectl delete -f deploy/k8s/prometheus/prometheus-pvc.yaml
kubectl delete -f deploy/k8s/grafana/grafana-pvc.yaml

echo "Removendo namespace..."
kubectl delete -f deploy/k8s/namespace.yaml

echo "Recursos removidos com sucesso!" 