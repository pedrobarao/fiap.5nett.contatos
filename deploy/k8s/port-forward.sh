#!/bin/bash

echo "Iniciando port-forward para os serviços..."
echo "Para parar, pressione Ctrl+C"
echo ""
echo "Serviços disponíveis em:"
echo "RabbitMQ UI: http://localhost:15672"
echo "PostgreSQL: localhost:5432"
echo "MongoDB: localhost:27017"
echo "Prometheus: http://localhost:9090"
echo "Grafana: http://localhost:3000"
echo "Cadastro API: http://localhost:8080"
echo "Cadastro API Swagger: http://localhost:8080/swagger"
echo ""

trap 'kill $(jobs -p)' SIGINT SIGTERM EXIT

kubectl port-forward -n contatos-app service/rabbitmq 15672:15672 --address 0.0.0.0 & 
kubectl port-forward -n contatos-app service/postgres 5432:5432 --address 0.0.0.0 &
kubectl port-forward -n contatos-app service/mongodb 27017:27017 --address 0.0.0.0 &
kubectl port-forward -n contatos-app service/prometheus 9090:9090 --address 0.0.0.0 &
kubectl port-forward -n contatos-app service/grafana 3000:3000 --address 0.0.0.0 &
kubectl port-forward -n contatos-app service/cadastro-contatos-api 8080:8080 --address 0.0.0.0 &

wait 