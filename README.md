# Local Tester - Pub/Sub GCP

#### Projeto criado com o objetivo de auxiliar a manipulação do emulador do Pub/Sub do GCP

##### Passos para utilização:

- Clonar o projeto
- Abrir com o Visual Studio
- Alterar o Startup project para o docker-compose
- Rodar a aplicação pelo Visual Studio

Ao realizar estes procedimentos devem ser criados dois containers no docker.

- gcp.emulator-1: Neste container está o emulador do Pub/Sub GCP
- GCP.PubSub.Local.Tester.API: Neste container é a API por onde será realizada as manipulações com o emulador

Também será aberto em seu navegador o Swagger da API citada acima, contendo as explicações de como utilizar cada método.
