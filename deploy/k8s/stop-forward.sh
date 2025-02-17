#!/bin/bash

echo "Parando todos os port-forwards..."
pkill -f "kubectl port-forward.*contatos-app"

echo "Port-forwards parados com sucesso!" 