#!/bin/bash

install_ingress_controller() {
  local ns="ingress-nginx"
  local svc_type="${INGRESS_SERVICE_TYPE:-NodePort}"

  if kubectl -n "$ns" get deployment -l app.kubernetes.io/name=ingress-nginx >/dev/null 2>&1; then
    echo "Ingress controller já presente no namespace $ns — pulando."
    return
  fi

  echo "Instalando ingress-nginx (service.type=$svc_type)..."
    kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller/v1.10.1/deploy/static/provider/cloud/deploy.yaml

  sleep 3

  if [ "$svc_type" = "NodePort" ]; then
    svc_name=$(kubectl -n "$ns" get svc -o name 2>/dev/null | grep controller || true)
    if [ -n "$svc_name" ]; then
      kubectl -n "$ns" patch "$svc_name" -p '{"spec": {"type": "NodePort"}}' || true
      echo "Service do ingress patched para NodePort."
    fi
  else
    kubectl -n "$ns" patch svc ingress-nginx-controller --type='merge' -p "{\"spec\": {\"type\": \"${svc_type}\"}}" >/dev/null 2>&1 || true
    echo "Service mantido como $svc_type."
  fi

  kubectl -n "$ns" wait --for=condition=available deployment -l app.kubernetes.io/name=ingress-nginx --timeout=120s || echo "Timeout aguardando ingress controller."
  echo "Ingress controller pronto."
}

echo "Iniciando criação de recursos Kubernetes..."

echo "Criando namespace..."
kubectl apply -f deploy/k8s/namespace.yaml

echo "Criando Secrets..."
kubectl apply -f deploy/k8s/secrets.yaml

echo "Criando Persistent Volumes Claims..."
#kubectl apply -f deploy/k8s/rabbitmq/pvc-rabbitmq.yaml
#kubectl apply -f deploy/k8s/cadastro-api/pvc-database.yaml
#kubectl apply -f deploy/k8s/consulta-api/pvc-database.yaml

echo "Criando Deployments..."

## Cadastro API
#kubectl apply -f deploy/k8s/cadastro-api/deploy-database.yaml
kubectl apply -f deploy/k8s/cadastro-api/deploy-api.yaml

## Consulta API
#kubectl apply -f deploy/k8s/consulta-api/deploy-database.yaml
kubectl apply -f deploy/k8s/consulta-api/deploy-api.yaml

## RabbitMQ
#kubectl apply -f deploy/k8s/rabbitmq/deploy-rabbitmq.yaml

echo "Criando Services..."
#kubectl apply -f deploy/k8s/cadastro-api/svc-database.yaml
#kubectl apply -f deploy/k8s/consulta-api/svc-database.yaml
kubectl apply -f deploy/k8s/cadastro-api/svc-api.yaml
kubectl apply -f deploy/k8s/consulta-api/svc-api.yaml
#kubectl apply -f deploy/k8s/rabbitmq/svc-rabbitmq.yaml

echo "Criando Ingress..."
install_ingress_controller
kubectl apply -f deploy/k8s/ingress.yaml

echo "Aplicando Metrics Server..."
kubectl apply -f deploy/k8s/sa-metrics.yaml

echo "Criando Horizontal Pod Autoscaler..."
kubectl apply -f deploy/k8s/cadastro-api/hpa-api.yaml
kubectl apply -f deploy/k8s/consulta-api/hpa-api.yaml


echo "Aguardando pods iniciarem..."
#kubectl wait --namespace contatos-app \
#  --for=condition=ready pod \
#  --selector=app=rabbitmq \
#  --timeout=90s
#kubectl wait --namespace contatos-app \
#  --for=condition=ready pod \
#  --selector=app=postgres \
#  --timeout=90s
#kubectl wait --namespace contatos-app \
#  --for=condition=ready pod \
#  --selector=app=mongodb \
#  --timeout=90s
#kubectl wait --namespace contatos-app \
#  --for=condition=ready pod \
#  --selector=app=cadastro-contatos-api \
#  --timeout=90s
#kubectl wait --namespace contatos-app \
#  --for=condition=ready pod \
#  --selector=app=consulta-contatos-api \
#  --timeout=90s

echo "Verificando recursos criados..."
kubectl get all -n contatos-app

echo "Aplicação implantada com sucesso!" 