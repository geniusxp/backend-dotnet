<img src="https://github.com/geniusxp/backend-.net/blob/main/banner_geniusxp.png">

# Sobre o GeniusXP
GeniusXP é uma plataforma centralizada para gestão de eventos que simplifica operações como inscrições, pagamentos e check-in, enquanto aumenta o engajamento com enquetes e networking. A inteligência artificial da GeniusXP utiliza as preferências dos usuários para oferecer uma experiência altamente personalizada e otimizar o planejamento. Com análise de sentimento e assistência virtual, a plataforma proporciona interações mais significativas, elevando a eficiência da gestão e tornando os eventos mais impactantes para cada participante.

# Arquitetura de Software Utilizada
Para a arquitetura da API do projeto GeniusXP, optamos por adotar uma abordagem **monolítica**. Essa decisão é justificada por diversos fatores que levam em conta tanto o contexto atual do projeto quanto seus requisitos técnicos e operacionais.

A abordagem monolítica se destaca pela sua simplicidade inicial. No estágio atual de desenvolvimento da GeniusXP, que foca em eventos menores e está em fase de crescimento e construção de uma base sólida de clientes, é mais eficiente lidar com uma única base de código. Essa estrutura permite um desenvolvimento mais rápido e menos complexo, uma vez que todos os componentes — desde a interface de usuário até a lógica de negócios e a camada de dados — estão agrupados e organizados em uma única aplicação. Isso reduz a necessidade de gerenciar múltiplos serviços ou lidar com a comunicação entre eles, algo que seria necessário em uma arquitetura de microsserviços.

A complexidade adicional trazida por microsserviços, como a necessidade de orquestrar a comunicação entre serviços, gerenciar transações distribuídas e implementar sistemas de monitoramento avançados, pode não ser justificável nesta fase inicial do projeto. Um sistema monolítico, por outro lado, tem uma implementação mais simples e direta, facilitando o desenvolvimento, testes e depuração.

Embora a arquitetura monolítica possa, em alguns casos, oferecer limitações de escalabilidade, essas limitações só se tornam relevantes conforme o projeto cresce substancialmente. Nesse momento inicial, os eventos menores que a GeniusXP está organizando e gerenciando não exigem a escalabilidade granular e distribuída de uma arquitetura de microsserviços. Quando a base de clientes e a complexidade das operações crescerem, podemos considerar uma migração para microsserviços, mas isso será feito de maneira gradual e estratégica, conforme a demanda exigir.

# Arquitetura da Solução
<img src="https://github.com/geniusxp/backend-.net/blob/main/GeniusXP_Devops_Arq.png">

# Design Pattern Aplicado

Na implementação da API do projeto GeniusXP, decidimos utilizar o padrão de criação Builder. O padrão Builder é eficaz quando se deseja fornecer uma maneira flexível e controlada de construir objetos complexos, separando a criação de um objeto da sua representação final.

O uso do Builder traz benefícios claros:

- **Facilita a manutenção:** O código se torna mais modular e organizado, permitindo a criação de instâncias de classes de forma fluida e flexível.
- **Controle sobre a construção de objetos complexos:** Com o Builder, é possível gerenciar a criação de objetos que possuem diversos atributos opcionais ou diferentes configurações sem comprometer a clareza do código.
- **Encapsulamento de lógica complexa:** Ele permite que a complexidade da construção do objeto seja ocultada dentro do builder, deixando o código principal mais limpo e de fácil entendimento.

Assim, o padrão Builder foi implementado para otimizar a construção das classes da API, promovendo maior flexibilidade e manutenção eficiente.

# Práticas de Clean Code e SOLID

Usamos diversas práticas de Clean Code e princípios SOLID para garantir que nosso código fosse limpo, legível e fácil de manter. Essa abordagem nos permite desenvolver soluções mais eficientes e adaptáveis às necessidades futuras.

### Clean Code

1. **Nomes Descritivos**: As classes, métodos e variáveis têm nomes autoexplicativos que indicam claramente suas responsabilidades. Por exemplo, `CreateEvent`, `FindAllEvents`, e `UpdateUser`.

2. **Métodos Pequenos e Focados**: Cada método tem uma única responsabilidade. O método `CreateEvent`, por exemplo, lida exclusivamente com a criação de eventos, evitando a mistura de lógica de validação ou persistência.

3. **Comentários Úteis**: Utilizamos anotações do Swagger (`[SwaggerOperation]`) para fornecer documentação embutida, facilitando a compreensão do propósito de cada endpoint.

5. **Tratamento de Erros**: Implementamos um tratamento de erros claro, retornando códigos de status apropriados (como `NotFound()` ou `BadRequest()`), permitindo que os consumidores da API entendam rapidamente o que deu errado.

### Princípios SOLID

1. **Single Responsibility Principle (SRP)**: Cada controlador tem uma única responsabilidade. Como por exemplo, o `EventsController` é responsável apenas por eventos, enquanto o `UserController` gerencia usuários, facilitando a manutenção e evolução do código.

2. **Open/Closed Principle (OCP)**: O sistema foi projetado para permitir a adição de novas funcionalidades e extensões sem a necessidade de modificar os controladores existentes. Isso significa que novas classes e recursos podem ser implementados sem alterar a lógica já existente, promovendo uma arquitetura flexível e escalável.

3. **Liskov Substitution Principle (LSP)**: Embora não esteja explicitamente visível no código, a arquitetura de classes permite que subclasses sejam usadas no lugar de suas superclasses sem quebrar a lógica do programa.

4. **Interface Segregation Principle (ISP)**: O design evita que classes sejam forçadas a implementar interfaces que não utilizam. Isso melhora a coesão do sistema.

5. **Dependency Inversion Principle (DIP)**: As dependências são injetadas via construtores (`AppDbContext` e `TokenService`), o que torna os controladores mais testáveis e promove o desacoplamento.

### Exemplos de Aplicação

- **Criação de um Evento**: O método `CreateEvent` utiliza o `EventBuilder` para construir um objeto complexo de evento, separando a construção da representação.

    ```csharp
    var eventBuilder = new EventBuilder();
    var newEvent = eventBuilder
        .Name(request.Name)
        .Description(request.Description)
        .EventType(request.EventType)
        .ImageUrl(request.ImageUrl)
        .Days()
        .TicketTypes()
        .Build();
    ```

- **Retorno de Respostas**: O uso de classes de resposta simplificada (`EventSimplifiedResponse` e `UserSimplifiedResponse`) promove a encapsulação, permitindo alterações nas classes de resposta sem impactar diretamente os controladores.

    ```csharp
    var createdEvent = EventSimplifiedResponse.From(newEvent);
    return CreatedAtAction("FindEventById", new { id = createdEvent.Id }, createdEvent);
    ```

# Implementação da Integração com Serviço Externo de Autenticação JWT
Implementamos uma integração com um serviço externo de autenticação utilizando **JWT (JSON Web Tokens)**. Essa abordagem permite um gerenciamento seguro e eficiente das autenticações de usuários em nossa aplicação.

### Como Usar
1. **Registro e Login**: Os usuários podem se registrar com suas informações ou fazer login usando seu **nome de usuário** e **senha**. Após a autenticação, um token JWT será gerado e enviado ao usuário.

2. **Autorização**: Para acessar os endpoints da API que requerem autenticação, o usuário deve incluir o token JWT no cabeçalho `Authorization` da requisição, no formato:
Bearer {token}

## Benefícios da Autenticação com JWT
- **Segurança**: Os tokens JWT são assinados digitalmente, garantindo que não possam ser alterados por usuários não autorizados. Além disso, a transmissão de dados sensíveis é segura, uma vez que o token pode ser enviado apenas por meio de conexões HTTPS.

- **Escalabilidade**: A autenticação JWT permite que a aplicação seja escalável, pois não é necessário manter sessões no servidor. O token contém todas as informações necessárias sobre a autenticação do usuário, reduzindo a carga no servidor.

## Integração de IA para Previsão de Duração de Evento

Integramos uma Inteligência Artificial para prever a duração ideal de eventos com base em dados como o número de participantes, tipo de evento e número de atividades. O modelo utiliza o Microsoft ML.NET com o algoritmo FastTree para realizar a previsão, treinando com um conjunto de dados específico que inclui as seguintes características:

- **Número de Participantes**: Total de pessoas que participarão do evento.
- **Tipo de Evento**: Classificações aceitas são 'Seminário', 'Webinar', 'Conferência' e 'Workshop'.
- **Número de Atividades**: Quantidade de atividades planejadas para o evento.
- **Data do Evento**: Data em que o evento ocorrerá.

### Funcionamento

O controlador `EventPredictionController` é responsável por receber os dados do evento e realizar a previsão da duração. Ao receber uma solicitação POST com os dados do evento, o sistema verifica as informações e utiliza um modelo previamente treinado para prever a duração.

### Exemplo de Uso

Para prever a duração de um evento, envie uma solicitação POST para o endpoint `/api/EventPrediction/predict` com um corpo JSON no seguinte formato:

```json
{
  "NumberOfParticipants": 100,
  "EventType": "Seminário",
  "NumberOfActivities": 5,
  "EventDate": "2024-10-31"
}
```

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