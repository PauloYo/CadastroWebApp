# CadastroWebApp

## Descrição
CadastroWebApp é uma aplicação web ASP.NET Core para gerenciamento de clientes e pedidos. Este sistema permite o cadastro, visualização e exclusão de clientes, com uma interface simples e funcional.

## Tecnologias Utilizadas
- ASP.NET Core MVC
- Microsoft SQL Server
- HTML/CSS
- Bootstrap
- jQuery

## Estrutura do Projeto

### Controllers
- **HomeController**: Gerencia a página inicial e de privacidade da aplicação
- **ClientesController**: Gerencia operações CRUD relacionadas a clientes

### Models
- **Cliente**: Representa um cliente com propriedades como Nome, Email, DataNascimento, etc.
- **Pedido**: Representa um pedido feito por um cliente
- **Database**: Classe responsável por gerenciar conexões com o banco de dados
- **ErrorViewModel**: Modelo para exibição de erros

### Repositórios
- **ClienteRepository**: Gerencia operações de banco de dados relacionadas a clientes

### Views
- **Home**: Views para a página inicial e política de privacidade
- **Clientes**: Views para listagem e criação de clientes
- **Shared**: Layouts e componentes compartilhados

## Funcionalidades
- **Listagem de Clientes**: Visualização de todos os clientes cadastrados
- **Cadastro de Clientes**: Formulário para adicionar novos clientes
- **Exclusão de Clientes**: Opção para remover clientes do sistema

## Banco de Dados
A aplicação utiliza SQL Server com as seguintes tabelas:
- **Cliente**: Armazena informações de clientes
  - Id (INT, PK)
  - Nome (VARCHAR)
  - Email (VARCHAR)
  - DataNascimento (DATETIME)
  - Genero (VARCHAR)
  - DataCadastro (DATETIME)
- **Pedido**: Armazena informações de pedidos
  - Id (INT, PK)
  - ClienteId (INT, FK)
  - DataPedido (DATETIME)
  - ValorTotal (DECIMAL)
  - Status (VARCHAR)
  - Descricao (VARCHAR)

## Como Executar

### Pré-requisitos
- .NET Core SDK
- SQL Server
- Visual Studio ou outro editor de código

### Configuração
1. Clone o repositório
   ```bash
   git clone [URL_DO_REPOSITÓRIO]
   ```

2. Configure a string de conexão no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=SEU_SERVIDOR;Database=CadastroWebApp;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. Execute o script SQL para criar as tabelas necessárias:
   ```sql
   CREATE TABLE Cliente (
       Id INT PRIMARY KEY IDENTITY,
       Nome VARCHAR(100) NOT NULL,
       Email VARCHAR(100),
       DataNascimento DATETIME,
       Genero VARCHAR(20),
       DataCadastro DATETIME DEFAULT GETDATE()
   );

   CREATE TABLE Pedido (
       Id INT PRIMARY KEY IDENTITY,
       ClienteId INT FOREIGN KEY REFERENCES Cliente(Id),
       DataPedido DATETIME DEFAULT GETDATE(),
       ValorTotal DECIMAL(10,2),
       Status VARCHAR(50),
       Descricao VARCHAR(500)
   );
   ```

4. Restaure os pacotes NuGet
   ```bash
   dotnet restore
   ```

5. Execute a aplicação
   ```bash
   dotnet run
   ```

## Próximas Melhorias
- Implementação completa do CRUD de pedidos
- Validação de formulários no lado do cliente
- Paginação na listagem de clientes
- Filtros de busca
- Autenticação e autorização de usuários

## Licença
Este projeto está sob a licença do MIT. Consulte o arquivo [LICENSE](/LICENSE) para mais detalhes.
