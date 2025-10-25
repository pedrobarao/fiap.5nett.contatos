#!/bin/bash

# Número de requisições concorrentes (-c) e tempo do teste em segundos (-t)
echo "Iniciando teste de carga na API de Cadastro..."
hey -n 1000 -c 100 -q 50 http://localhost:5001/api/contatos

# Ou usando o Apache Benchmark (ab)
# ab -n 1000 -c 100 http://localhost:5001/api/contatos 