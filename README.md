# ucondo-052025
Repositório para o handson do processo seletivo da uCondo.

[uCondo-Test.postman_collection.json](https://github.com/user-attachments/files/20443571/uCondo-Test.postman_collection.json)

# Métodos:
* GET /api/accounts-chart

curl --location 'http://localhost:5000/api/accounts-chart'

Retorna a lista das contas cadastradas

* GET /api/accounts-chart/parent
  
curl --location 'http://localhost:5000/api/accounts-chart/parent'

Retorna a lista das contas pais, ou seja, as contas que não aceitam lançamentos.

* POST /api/accounts-chart

curl --location 'http://localhost:5000/api/accounts-chart' \
--header 'Content-Type: application/json' \
--data '{
  
  "code": "3.1",
  "parentCode": "3",
  "name": "T3.1",
  "accountType": "Income",
  "acceptsReleases": true
}'

Cria nova conta na base de dados.

* DELETE /api/accounts-chart

curl --location --request DELETE 'http://localhost:5000/api/accounts-chart/df5dcc31-fc99-4b19-8cfc-f92d60d7b5f7'

Remove a conta da base pelo ID.

* GET /api/accounts-chart/search

curl --location 'http://localhost:5000/api/accounts-chart/search?partialName=1'

Busca pelo início do nome ou do código da conta.

* GET /api/accounts-chart/next-code

curl --location 'http://localhost:5000/api/accounts-chart/next-code?parent=9.9.999.999'

Busca o próximo código disponível no plano de contas

