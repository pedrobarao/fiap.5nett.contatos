#!/bin/bash

echo "Removendo recursos Kubernetes..."

echo "Removendo Deployments e Services..."
kubectl delete -f deploy/k8s/rabbitmq.yaml
kubectl delete -f deploy/k8s/postgres.yaml
kubectl delete -f deploy/k8s/mongodb.yaml
kubectl delete -f deploy/k8s/prometheus.yaml
kubectl delete -f deploy/k8s/grafana.yaml
kubectl delete -f deploy/k8s/cadastro-api.yaml
kubectl delete -f deploy/k8s/consulta-api.yaml

echo "Removendo ConfigMaps..."
kubectl delete -f deploy/k8s/prometheus-config.yaml
kubectl delete -f deploy/k8s/grafana-config.yaml

echo "Removendo PVCs..."
kubectl delete -f deploy/k8s/rabbitmq-pvc.yaml
kubectl delete -f deploy/k8s/postgres-pvc.yaml
kubectl delete -f deploy/k8s/mongodb-pvc.yaml
kubectl delete -f deploy/k8s/prometheus-pvc.yaml
kubectl delete -f deploy/k8s/grafana-pvc.yaml

echo "Removendo namespace..."
kubectl delete -f deploy/k8s/namespace.yaml

echo "Recursos removidos com sucesso!" 