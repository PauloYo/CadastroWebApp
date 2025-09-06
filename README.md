# 🛒 CadastroWebApp

## 📋 Descrição
CadastroWebApp é uma **aplicação web moderna** desenvolvida em ASP.NET Core 9.0 para gerenciamento completo de **Clientes e Pedidos**. O sistema oferece funcionalidades CRUD completas com interface responsiva, busca avançada, paginação e validação em tempo real.

## 🚀 Tecnologias Utilizadas
- **ASP.NET Core 9.0 MVC** - Framework principal
- **Entity Framework Core** - ORM para acesso a dados
- **PostgreSQL** - Banco de dados (hospedado no Railway)
- **Bootstrap 5.3.0** - Framework CSS responsivo
- **FontAwesome 6.4.0** - Biblioteca de ícones
- **jQuery Validation** - Validação client-side
- **Npgsql** - Provider PostgreSQL para .NET

## 🏗️ Estrutura do Projeto

### Controllers
- **HomeController**: Gerencia páginas institucionais (Home, Privacy)
- **ClientesController**: CRUD completo de clientes com busca e paginação
- **PedidosController**: CRUD completo de pedidos com relacionamento cliente-pedido

### Models
- **Cliente**: Entidade cliente com validações e relacionamentos
- **Pedido**: Entidade pedido vinculada ao cliente
- **AppDbContext**: Contexto Entity Framework Core
- **ErrorViewModel**: Modelo para tratamento de erros

### Repositórios (Repository Pattern)
- **ClienteRepository**: Operações de dados para clientes
- **PedidoRepository**: Operações de dados para pedidos com Include() automático

### Views
- **Home**: Página inicial moderna e responsiva
- **Clientes**: CRUD completo (Index, Create, Edit, Details, Delete)
- **Pedidos**: CRUD completo com seleção de cliente e status
- **Shared**: Layout Bootstrap 5 profissional

## ✨ Funcionalidades

### 👥 Gestão de Clientes
- ✅ **Listagem** com busca por nome/email e paginação
- ✅ **Cadastro** com validação completa de formulário
- ✅ **Edição** de dados existentes
- ✅ **Visualização** detalhada de perfil
- ✅ **Exclusão** com confirmação de segurança

### 📦 Gestão de Pedidos  
- ✅ **Listagem** com busca por cliente/ID e paginação
- ✅ **Cadastro** com seleção de cliente e validação
- ✅ **Edição** completa de pedidos existentes
- ✅ **Visualização** detalhada com dados do cliente
- ✅ **Exclusão** com confirmação de segurança
- ✅ **Status coloridos** (Pendente, Processando, Enviado, Entregue, Cancelado)

### 🎨 Interface e UX
- ✅ **Design responsivo** Bootstrap 5
- ✅ **Ícones FontAwesome** em toda interface
- ✅ **Validação em tempo real** client/server-side
- ✅ **Mensagens de feedback** com TempData
- ✅ **Data/hora picker** automático
- ✅ **Formatação de moeda** brasileira (R$)

## 🗄️ Banco de Dados
A aplicação utiliza **PostgreSQL** hospedado no **Railway** com as seguintes tabelas:

### 📊 Estrutura das Tabelas

**Cliente**
- `Id` (SERIAL, PK) - Identificador único
- `Nome` (VARCHAR(100)) - Nome completo do cliente
- `Email` (VARCHAR(100)) - Email único do cliente  
- `DataNascimento` (TIMESTAMP) - Data de nascimento
- `Genero` (VARCHAR(20)) - Gênero do cliente
- `DataCadastro` (TIMESTAMP) - Data de cadastro automática

**Pedido**
- `Id` (SERIAL, PK) - Identificador único
- `ClienteId` (INT, FK) - Referência ao cliente
- `DataPedido` (TIMESTAMP) - Data/hora do pedido
- `ValorTotal` (DECIMAL(10,2)) - Valor monetário do pedido
- `Status` (VARCHAR(50)) - Status atual do pedido
- `Descricao` (VARCHAR(500)) - Descrição detalhada

### 🔗 Relacionamentos
- **Cliente** 1:N **Pedido** (Um cliente pode ter vários pedidos)
- **Foreign Key**: `Pedido.ClienteId` → `Cliente.Id`
- **Cascade Delete**: Configurado no Entity Framework

### 📈 Dados de Teste
- **18 clientes** pré-cadastrados para demonstração
- **20 pedidos** com diferentes status e valores
- **Relacionamentos** completos entre clientes e pedidos

## 🚀 Como Executar

### 📋 Pré-requisitos
- **.NET 9.0 SDK** ou superior
- **Visual Studio 2022** ou **VS Code**
- **Git** para controle de versão
- Acesso à internet (para conexão com Railway PostgreSQL)

### ⚡ Execução Rápida
1. **Clone o repositório**
   ```bash
   git clone https://github.com/PauloYo/CadastroWebApp.git
   cd CadastroWebApp
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Execute a aplicação**
   ```bash
   dotnet run
   ```

4. **Acesse no navegador**
   ```
   https://localhost:7173
   ```

### 🔧 Configuração Personalizada

#### Banco de Dados
A aplicação está configurada para usar PostgreSQL no Railway. A string de conexão está em `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=trolley.proxy.rlwy.net;Port=12027;Database=railway;Username=postgres;Password=IWztuihqsKmzsKMsxNsCjxbZbThQNrFg"
  }
}
```

#### Para usar banco local PostgreSQL:
1. Instale PostgreSQL localmente
2. Atualize a connection string em `appsettings.Development.json`
3. Execute as migrations:
   ```bash
   dotnet ef database update
   ```

#### Executar script SQL manual:
Se preferir configurar o banco manualmente, execute o arquivo `database_setup.sql` que contém:
- Criação das tabelas
- Dados de teste (18 clientes + 20 pedidos)
- Configurações de relacionamento

## 📁 Estrutura de Arquivos

```
CadastroWebApp/
├── 📁 Controllers/
│   ├── ClienteController.cs      # CRUD de clientes
│   ├── HomeController.cs         # Páginas institucionais  
│   └── PedidoController.cs       # CRUD de pedidos
├── 📁 Data/
│   ├── ClienteRepository.cs      # Repository para clientes
│   └── PedidoRepository.cs       # Repository para pedidos
├── 📁 Models/
│   ├── AppDbContext.cs           # Contexto Entity Framework
│   ├── Cliente.cs                # Entidade cliente
│   ├── Pedido.cs                 # Entidade pedido
│   └── ErrorViewModel.cs         # Modelo de erro
├── 📁 Views/
│   ├── 📁 Clientes/              # Views do CRUD de clientes
│   ├── 📁 Pedidos/               # Views do CRUD de pedidos
│   ├── 📁 Home/                  # Páginas institucionais
│   └── 📁 Shared/                # Layout e componentes
├── 📁 wwwroot/                   # Arquivos estáticos
├── 📁 Migrations/                # Migrations Entity Framework
├── database_setup.sql            # Script PostgreSQL completo
├── appsettings.json              # Configurações da aplicação
└── Program.cs                    # Ponto de entrada da aplicação
```

## 🎯 Funcionalidades Implementadas

### ✅ CRUD Completo
- [x] **Clientes**: Create, Read, Update, Delete
- [x] **Pedidos**: Create, Read, Update, Delete
- [x] **Relacionamentos**: Cliente-Pedido funcionando

### ✅ Interface de Usuário
- [x] **Design responsivo** para desktop e mobile
- [x] **Busca e paginação** em todas as listagens
- [x] **Validação de formulários** client e server-side
- [x] **Mensagens de feedback** para ações do usuário

### ✅ Funcionalidades Avançadas
- [x] **Status coloridos** para pedidos
- [x] **Formatação de data/hora** brasileira
- [x] **Formatação de moeda** (R$)
- [x] **Confirmação de exclusão** com modal

## 🔮 Melhorias Futuras
- 🔐 **Autenticação e autorização** de usuários
- 📊 **Dashboard** com gráficos e estatísticas
- 📧 **Sistema de notificações** por email
- 📱 **PWA** (Progressive Web App)
- 🌙 **Tema escuro** para a interface
- 📈 **Relatórios** em PDF/Excel
- 🔍 **Filtros avançados** de busca
- 🌐 **Internacionalização** (i18n)

## 📄 Licença
Este projeto está sob a licença **MIT**. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

## 👨‍💻 Autor
Desenvolvido com ❤️ por **PauloYo**

---
⭐ **Gostou do projeto? Deixe uma estrela no GitHub!**
