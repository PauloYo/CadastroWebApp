# 🍔 FastRequest - Sistema de Cantina Web

## 📋 Descrição
**FastRequest** é uma **aplicação web moderna** desenvolvida em ASP.NET Core 9.0 para gerenciamento completo de **cantina escolar/empresarial**. O sistema oferece funcionalidades completas de gerenciamento de clientes, produtos do cardápio e pedidos, com interface responsiva, sistema de ícones por categoria e experiência otimizada para mobile.

## 🚀 Tecnologias Utilizadas
- **ASP.NET Core 9.0 MVC** - Framework principal
- **Entity Framework Core** - ORM para acesso a dados
- **PostgreSQL** - Banco de dados (hospedado no Railway)
- **Bootstrap 5.3.0** - Framework CSS responsivo
- **FontAwesome 6.4.0** - Biblioteca de ícones (sistema de categorias)
- **jQuery Validation** - Validação client-side
- **Npgsql** - Provider PostgreSQL para .NET

## 🏗️ Estrutura do Projeto

### Controllers
- **HomeController**: Gerencia páginas institucionais (Home, Privacy)
- **ClienteController**: CRUD completo de clientes com busca e paginação
- **PedidoController**: CRUD completo de pedidos com carrinho de compras
- **ProdutosController**: CRUD de produtos com cardápio público e gestão admin

### Models
- **Cliente**: Entidade cliente com validações e relacionamentos
- **Pedido**: Entidade pedido vinculada ao cliente
- **Produto**: Entidade produto com categorias e sistema de ícones
- **ItemPedido**: Entidade para itens individuais dos pedidos
- **AppDbContext**: Contexto Entity Framework Core

### Repositórios (Repository Pattern)
- **ClienteRepository**: Operações de dados para clientes
- **PedidoRepository**: Operações de dados para pedidos com Include() automático
- **ProdutoRepository**: Operações de dados para produtos com filtros por categoria

### Views
- **Home**: Página inicial moderna e responsiva
- **Clientes**: CRUD completo (Index, Create, Edit, Details, Delete)
- **Pedidos**: CRUD completo com seleção de produtos e carrinho
- **Produtos**: CRUD admin + Cardápio público com sistema de ícones
- **Shared**: Layout Bootstrap 5 profissional

## ✨ Funcionalidades

### 👥 Gestão de Clientes
- ✅ **Listagem** com busca por nome/email e paginação
- ✅ **Cadastro** com validação completa de formulário
- ✅ **Edição** de dados existentes
- ✅ **Visualização** detalhada de perfil
- ✅ **Exclusão** com confirmação de segurança

### 🍔 Gestão de Produtos (Cardápio)
- ✅ **CRUD Administrativo** completo para produtos
- ✅ **Cardápio Público** responsivo com filtros por categoria
- ✅ **Sistema de Ícones** FontAwesome por categoria (🍔 Lanches, ☕ Bebidas, 🍰 Sobremesas, etc.)
- ✅ **Categorias Visuais** com cores diferenciadas
- ✅ **Busca e Filtros** por categoria e nome
- ✅ **Gestão de Disponibilidade** (ativar/desativar produtos)

### 📦 Gestão de Pedidos  
- ✅ **Listagem** com busca por cliente/ID e paginação
- ✅ **Cadastro** com seleção de produtos do cardápio
- ✅ **Carrinho de Compras** com múltiplos itens
- ✅ **Cálculo Automático** de valores totais
- ✅ **Edição** completa de pedidos existentes
- ✅ **Visualização** detalhada com dados do cliente
- ✅ **Exclusão** com confirmação de segurança
- ✅ **Status coloridos** (Pendente, Processando, Enviado, Entregue, Cancelado)

### 🎨 Interface e UX
- ✅ **Design responsivo** Bootstrap 5
- ✅ **Sistema de ícones** FontAwesome por categoria de produto
- ✅ **Interface Mobile-First** otimizada para cantinas
- ✅ **Validação em tempo real** client/server-side
- ✅ **Mensagens de feedback** com TempData
- ✅ **Data/hora picker** automático
- ✅ **Formatação de moeda** brasileira (R$)
- ✅ **Cards visuais** para produtos com ícones grandes
- ✅ **Filtros dinâmicos** no cardápio público

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

**Produto**
- `Id` (SERIAL, PK) - Identificador único
- `Nome` (VARCHAR(100)) - Nome do produto
- `Descricao` (VARCHAR(500)) - Descrição detalhada
- `Preco` (DECIMAL(10,2)) - Preço do produto
- `Categoria` (VARCHAR(50)) - Categoria (Lanches, Bebidas, Sobremesas, etc.)
- `Disponivel` (BOOLEAN) - Status de disponibilidade
- `DataCadastro` (TIMESTAMP) - Data de cadastro automática

**Pedido**
- `Id` (SERIAL, PK) - Identificador único
- `ClienteId` (INT, FK) - Referência ao cliente
- `DataPedido` (TIMESTAMP) - Data/hora do pedido
- `ValorTotal` (DECIMAL(10,2)) - Valor monetário do pedido
- `Status` (VARCHAR(50)) - Status atual do pedido
- `TipoEntrega` (VARCHAR(50)) - Tipo de entrega (Balcão, Mesa, etc.)
- `Observacoes` (VARCHAR(500)) - Observações do pedido

**ItemPedido**
- `Id` (SERIAL, PK) - Identificador único
- `PedidoId` (INT, FK) - Referência ao pedido
- `ProdutoId` (INT, FK) - Referência ao produto
- `Quantidade` (INT) - Quantidade do item
- `PrecoUnitario` (DECIMAL(10,2)) - Preço no momento do pedido
- `Observacoes` (VARCHAR(200)) - Observações do item

### 🔗 Relacionamentos
- **Cliente** 1:N **Pedido** (Um cliente pode ter vários pedidos)
- **Pedido** 1:N **ItemPedido** (Um pedido pode ter vários itens)
- **Produto** 1:N **ItemPedido** (Um produto pode estar em vários itens)
- **Foreign Keys**: Configuradas com cascade appropriado

### 📈 Sistema de Ícones por Categoria
- 🍔 **Lanches** → `fa-hamburger` (amarelo)
- ☕ **Bebidas** → `fa-glass-whiskey` (azul)
- 🍰 **Sobremesas** → `fa-ice-cream` (vermelho)
- 🍽️ **Pratos Executivos** → `fa-utensils` (verde)
- 🥗 **Saladas** → `fa-leaf` (verde)
- 🍪 **Petiscos** → `fa-cookie-bite` (amarelo)
- 🥤 **Sucos** → `fa-cocktail` (azul)
- ☕ **Cafés** → `fa-coffee` (marrom)
- 🍭 **Doces** → `fa-candy-cane` (vermelho)
- 🥖 **Salgados** → `fa-bread-slice` (amarelo)

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

### 🍔 Acessos Principais
- **Página Inicial**: https://localhost:7173
- **Cardápio Público**: https://localhost:7173/Produtos/Cardapio
- **Gestão de Produtos**: https://localhost:7173/Produtos
- **Gestão de Clientes**: https://localhost:7173/Clientes
- **Gestão de Pedidos**: https://localhost:7173/Pedidos

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
│   ├── PedidoController.cs       # CRUD de pedidos com carrinho
│   └── ProdutosController.cs     # CRUD de produtos + cardápio
├── 📁 Data/
│   ├── ClienteRepository.cs      # Repository para clientes
│   ├── PedidoRepository.cs       # Repository para pedidos
│   └── ProdutoRepository.cs      # Repository para produtos
├── 📁 Models/
│   ├── AppDbContext.cs           # Contexto Entity Framework
│   ├── Cliente.cs                # Entidade cliente
│   ├── Pedido.cs                 # Entidade pedido
│   ├── Produto.cs                # Entidade produto
│   ├── ItemPedido.cs             # Entidade item do pedido
│   ├── ItemPedidoDto.cs          # DTO para transferência de dados
│   └── ErrorViewModel.cs         # Modelo de erro
├── 📁 Views/
│   ├── 📁 Clientes/              # Views do CRUD de clientes
│   ├── 📁 Pedidos/               # Views do CRUD de pedidos
│   ├── 📁 Produtos/              # Views admin + cardápio público
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
- [x] **Produtos**: Create, Read, Update, Delete + gestão de disponibilidade
- [x] **Pedidos**: Create, Read, Update, Delete com carrinho de compras
- [x] **Relacionamentos**: Cliente-Pedido-ItemPedido funcionando

### ✅ Sistema de Cardápio
- [x] **Cardápio público** responsivo com filtros por categoria
- [x] **Sistema de ícones** FontAwesome por categoria de produto
- [x] **Gestão visual** de produtos com cards e badges
- [x] **Busca e filtros** dinâmicos no cardápio

### ✅ Interface de Usuário
- [x] **Design responsivo** para desktop e mobile
- [x] **Busca e paginação** em todas as listagens
- [x] **Validação de formulários** client e server-side
- [x] **Mensagens de feedback** para ações do usuário
- [x] **Sistema de ícones** por categoria de produto

### ✅ Funcionalidades Avançadas
- [x] **Status coloridos** para pedidos
- [x] **Formatação de data/hora** brasileira
- [x] **Formatação de moeda** (R$)
- [x] **Confirmação de exclusão** com modal
- [x] **Carrinho de compras** com múltiplos itens
- [x] **Cálculo automático** de valores

## 🔮 Melhorias Futuras
- 🔐 **Autenticação e autorização** de usuários (Admin/Cliente)
- 📊 **Dashboard** com gráficos de vendas e relatórios
- 🛒 **Carrinho persistente** entre sessões
- 📧 **Sistema de notificações** por email/WhatsApp
- 📱 **PWA** (Progressive Web App) para mobile
- 🌙 **Tema escuro** para a interface
- 📈 **Relatórios** de vendas em PDF/Excel
- 🔍 **Filtros avançados** por preço, categoria, etc.
- 🌐 **Múltiplas cantinas** (multi-tenant)
- 💳 **Integração com pagamento** digital
- 📋 **Sistema de mesa/ficha** para entregas
- ⏰ **Horário de funcionamento** automático

## 📝 Changelog Recente

### v2.0.0 - Sistema de Ícones (06/09/2025)
- ✅ **Removida funcionalidade de imagens** dos produtos
- ✅ **Implementado sistema de ícones** FontAwesome por categoria
- ✅ **Atualizada interface** do cardápio com ícones visuais
- ✅ **Migração de banco** para remoção da coluna ImagemUrl
- ✅ **Melhorada experiência mobile** com ícones grandes
- ✅ **Padronização visual** em todas as views de produtos

### v1.0.0 - Base do Sistema (05/09/2025)
- ✅ **Sistema básico** de clientes, produtos e pedidos
- ✅ **CRUD completo** para todas as entidades
- ✅ **Interface responsiva** Bootstrap 5
- ✅ **Banco PostgreSQL** no Railway

## 📄 Licença
Este projeto está sob a licença **MIT**. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

---
⭐ **Gostou do projeto? Deixe uma estrela no GitHub!**
