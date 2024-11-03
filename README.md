<img src="https://dev.azure.com/RM550841/59de0c82-656a-42f9-857a-88ff284802e5/_apis/git/repositories/cd9ac2c8-fef1-4146-bac7-59b7a62c8d20/items?path=/backend-.net/banner_geniusxp.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=main&resolveLfs=true&%24format=octetStream&api-version=5.0">

# Sobre o GeniusXP
GeniusXP é uma plataforma centralizada para gestão de eventos que simplifica operações como inscrições, pagamentos e check-in, enquanto aumenta o engajamento com enquetes e networking. A inteligência artificial da GeniusXP utiliza as preferências dos usuários para oferecer uma experiência altamente personalizada e otimizar o planejamento. Com análise de sentimento e assistência virtual, a plataforma proporciona interações mais significativas, elevando a eficiência da gestão e tornando os eventos mais impactantes para cada participante.

# Arquitetura de Software Utilizada
Para a arquitetura da API do projeto GeniusXP, optamos por adotar uma abordagem **monolítica**. Essa decisão é justificada por diversos fatores que levam em conta tanto o contexto atual do projeto quanto seus requisitos técnicos e operacionais.

A abordagem monolítica se destaca pela sua simplicidade inicial. No estágio atual de desenvolvimento da GeniusXP, que foca em eventos menores e está em fase de crescimento e construção de uma base sólida de clientes, é mais eficiente lidar com uma única base de código. Essa estrutura permite um desenvolvimento mais rápido e menos complexo, uma vez que todos os componentes — desde a interface de usuário até a lógica de negócios e a camada de dados — estão agrupados e organizados em uma única aplicação. Isso reduz a necessidade de gerenciar múltiplos serviços ou lidar com a comunicação entre eles, algo que seria necessário em uma arquitetura de microsserviços.

A complexidade adicional trazida por microsserviços, como a necessidade de orquestrar a comunicação entre serviços, gerenciar transações distribuídas e implementar sistemas de monitoramento avançados, pode não ser justificável nesta fase inicial do projeto. Um sistema monolítico, por outro lado, tem uma implementação mais simples e direta, facilitando o desenvolvimento, testes e depuração.

Embora a arquitetura monolítica possa, em alguns casos, oferecer limitações de escalabilidade, essas limitações só se tornam relevantes conforme o projeto cresce substancialmente. Nesse momento inicial, os eventos menores que a GeniusXP está organizando e gerenciando não exigem a escalabilidade granular e distribuída de uma arquitetura de microsserviços. Quando a base de clientes e a complexidade das operações crescerem, podemos considerar uma migração para microsserviços, mas isso será feito de maneira gradual e estratégica, conforme a demanda exigir.

# Arquitetura da Solução
<img src="https://dev.azure.com/RM550841/59de0c82-656a-42f9-857a-88ff284802e5/_apis/git/repositories/cd9ac2c8-fef1-4146-bac7-59b7a62c8d20/items?path=/backend-.net/GeniusXP_Devops_Arq.png&versionDescriptor%5BversionOptions%5D=0&versionDescriptor%5BversionType%5D=0&versionDescriptor%5Bversion%5D=main&resolveLfs=true&%24format=octetStream&api-version=5.0">

# Design Pattern Aplicado

Na implementação da API do projeto GeniusXP, decidimos utilizar o padrão de criação Builder. O padrão Builder é eficaz quando se deseja fornecer uma maneira flexível e controlada de construir objetos complexos, separando a criação de um objeto da sua representação final.

O uso do Builder traz benefícios claros:

- **Facilita a manutenção:** O código se torna mais modular e organizado, permitindo a criação de instâncias de classes de forma fluida e flexível.
- **Controle sobre a construção de objetos complexos:** Com o Builder, é possível gerenciar a criação de objetos que possuem diversos atributos opcionais ou diferentes configurações sem comprometer a clareza do código.
- **Encapsulamento de lógica complexa:** Ele permite que a complexidade da construção do objeto seja ocultada dentro do builder, deixando o código principal mais limpo e de fácil entendimento.

Assim, o padrão Builder foi implementado para otimizar a construção das classes da API, promovendo maior flexibilidade e manutenção eficiente.

# Instruções para executar o projeto (Local)
Para executar o projeto localmente, é necessário possuir alguns programas e ferramentas instaladas e seguir esses passos:

## Pré-requisitos
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) com o workload de desenvolvimento para ASP.NET Core
- [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet) (versão compatível com o projeto (.NET 8.0))
- Git instalado ([Baixar Git](https://git-scm.com/))
- Banco de Dados Oracle

## Passo 1: Clonar o Repositório
Abra o terminal ou o Git Bash e execute o seguinte comando para clonar o repositório do projeto:

```bash
git clone https://github.com/geniusxp/backend-.net
```

## Passo 2: Navegar até a Pasta do Projeto
Depois de clonar o repositório, navegue até a pasta raiz do projeto:

```bash
cd backend-.net
```

## Passo 3: Restaurar as Dependências
Restaure todas as dependências necessárias para o projeto executando o seguinte comando:

```bash
dotnet restore
```
## Passo 4: Configurar String de Conexão
Edite o arquivo appsettings.json e configure credenciais corretas para acessar seu banco de dados Oracle:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=oracle.fiap.com.br:1521/orcl; User Id={Seu usuário}; Password={Sua senha}"
}
```

## Passo 5: Executar as migrations do projeto
Aplique as migrations executando o seguinte comando para criar as tabelas em seu banco de dados:

```bash
dotnet ef database update
```

## Passo 6: Executar o projeto
Para rodar o projeto, utilize o seguinte comando:

```bash
dotnet run
```
Isso irá iniciar a API localmente. A saída no terminal indicará o endereço no qual a API está sendo executada, geralmente http://localhost:5130

## Passo 7: Acessando documentação e testando o projeto com Swagger
No navegador, acesse o seguinte endereço para visualizar a documentação da API gerada pelo Swagger:

```bash
https://localhost:5130/swagger/index.html
```
Através da interface do Swagger, você pode testar os endpoints diretamente, enviando requisições e recebendo as respostas em tempo real. Isso facilita a verificação da funcionalidade da API sem a necessidade de ferramentas externas.

# Instruções para executar o projeto (Nuvem)
Para executar e testar a API GeniusXP, siga os passos abaixo:

1. Acesse o seguinte endereço para iniciar o projeto:
```
https://geniusxp-api.azurewebsites.net
```

2. Para visualizar a documentação e testar os endpoints da API, acesse o Swagger no seguinte link:
```
https://geniusxp-api.azurewebsites.net/swagger/index.html
```
O Swagger fornece uma interface interativa para explorar os recursos da API diretamente no navegador.

# Endpoints da API
Abaixo estão os principais endpoints disponíveis na API GeniusXP, acessíveis via o Swagger:

## Eventos
- [POST] /api/events

Corpo da requisição:
```json
{
  "name": "string",
  "description": "string",
  "eventType": "string",
  "imageUrl": "string",
  "eventDays": [
    {
      "startDate": "2024-09-15T23:10:16.337Z",
      "endDate": "2024-09-15T23:10:16.337Z",
      "transmissionUrl": "string"
    }
  ],
  "ticketTypes": [
    {
      "price": 0,
      "category": "string",
      "description": "string",
      "availableQuantity": 0
    }
  ]
}
```
- [PUT] /api/events/{id}

Corpo da requisição:

```json
{
  "name": "string",
  "description": "string",
  "eventType": "string",
  "imageUrl": "string"
}
```
- [GET] /api/events
- [GET] /api/events/{id}
- [DELETE] /api/events/{id}

## Ingresso
- [POST] /api/ticket

Corpo da Requisição
```json
{
  "userId": 0,
  "ticketTypeId": 0
}
```
- [PUT] /api/ticket/{ticketNumber}
- [DELETE] /api/ticket/{id}

## Usuário
- [POST] /api/user

Corpo da Requisição
```json
{
  "name": "string",
  "email": "string",
  "password": "string",
  "cpf": "string",
  "dateOfBirth": "2024-09-15",
  "avatarUrl": "string",
  "description": "string",
  "interests": "string"
}
```
- [PUT] /api/user/{id}

Corpo da Requisição
```json
{
  "name": "string",
  "email": "string",
  "password": "string",
  "cpf": "string",
  "dateOfBirth": "2024-09-15",
  "avatarUrl": "string",
  "description": "string",
  "interests": "string"
}
```
- [GET] /api/user
- [GET] /api/user/{id}
- [DELETE] /api/user/{id}

# Vídeo Demonstração
Para ver o vídeo de demonstração da aplicação, acesse: https://youtu.be/UvQf4g5VyBg

# Equipe GeniusXP
- RM99565 - Erick Nathan Capito Pereira
- RM550841 - Lucas Araujo Oliveira Silva
- RM99409 - Michael José Bernardi Da Silva
- RM99577 - Guilherme Dias Gomes
- RM550889 - Hemily Nara da Silva
