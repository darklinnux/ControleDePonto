# Relatório de Desenvolvimento - Projeto de Registro de Pontos

## Introdução
Neste relatório, descrevo o desenvolvimento de uma ferramenta para registro de pontos, criada como parte de um processo seletivo. A ferramenta inclui funcionalidades como cadastro de colaborador, registro de ponto e geração de relatórios.

## Tecnologias Utilizadas
- Linguagem: C#
- Framework: ASP.NET Core
- Padrões: SOLID
- Arquitetura: Camada de Controller, Modelo, Serviço e Repositório
- Testes: Moq e xUnit
- Frontend: React

## Desafios
- Aprendizado acelerado de C# utilizando ASP.NET Core e Entity Framework em apenas dois dias.
- Adaptação aos princípios do SOLID e à arquitetura em camadas.

## Pontos de Melhoria
1. Ajustar os serviços para maior generalização, conforme os princípios do SOLID.
2. Aprimorar a recepção de modelos nos serviços.
3. Corrigir a diferenciação entre DTOs e modelos.
4. Refatorar o serviço de usuário para melhor organização.

## Considerações Finais
A experiência foi desafiadora e enriquecedora. Apesar do prazo apertado, aprendi muito, incluindo a configuração de rotas, implementação de middlewares, geração de banco de dados com Entity Framework e criação de filtros e tokens para autenticação.

## Procedimento de Execução do Projeto

1. Clone o projeto:
   ```
   git clone https://github.com/darklinnux/ControleDePonto.git
   ```

2. Acesse a pasta do projeto:
   ```
   cd ControleDePonto
   ```

3. Acesse a pasta frontend:
   ```
   cd frontend
   ```

4. Instale as dependências do frontend usando Yarn:
   ```
   yarn install
   ```

5. Execute o comando para construir a aplicação frontend:
   ```
   yarn build
   ```

6. Volte para a pasta anterior:
   ```
   cd ..
   ```

7. Execute o Docker Compose para iniciar os contêineres:
   ```
   docker-compose up
   ```

8. Entre na pasta do backend:
   ```
   cd backend
   ```

9. Atualize o banco de dados utilizando o Entity Framework Core:
   ```
   dotnet database update
   ```

10. Restaure as dependências do backend:
    ```
    dotnet restore
    ```

11. Execute a aplicação backend:
    ```
    dotnet run
    ```
12. Acesse a pagina através do link abaixo.
    http://localhost:8000/registrar  

Certifique-se de ter as seguintes ferramentas instaladas e configuradas corretamente em seu ambiente de desenvolvimento:
- Git
- Node.js
- Yarn
- Docker
- .NET Core SDK
- Docker Compose
