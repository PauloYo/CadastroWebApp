-- =====================================
-- FASTREQUEST - SISTEMA DE CANTINA WEB
-- =====================================
-- Script para criação completa do banco de dados
-- Compatível com PostgreSQL (Railway)
-- 
-- Versão: 2.0.0 - Sistema completo com produtos e cardápio
-- Data: 06/09/2025

-- Remover tabelas se existirem (para recriação limpa)
DROP TABLE IF EXISTS "__EFMigrationsHistory" CASCADE;
DROP TABLE IF EXISTS "ItensPedido" CASCADE;
DROP TABLE IF EXISTS "Pedidos" CASCADE;
DROP TABLE IF EXISTS "Produtos" CASCADE;
DROP TABLE IF EXISTS "Clientes" CASCADE;

-- =====================================
-- TABELA DE CLIENTES
-- =====================================
CREATE TABLE "Clientes" (
    "Id" SERIAL PRIMARY KEY,
    "Nome" VARCHAR(100) NOT NULL,
    "Email" VARCHAR(100) NULL,
    "DataNascimento" TIMESTAMP NULL,
    "Genero" VARCHAR(20) NULL,
    "DataCadastro" TIMESTAMP NOT NULL DEFAULT NOW(),
    "DataModificacao" TIMESTAMP NULL,
    
    -- Constraints
    CONSTRAINT "CK_Cliente_Email" CHECK ("Email" LIKE '%@%' OR "Email" IS NULL),
    CONSTRAINT "CK_Cliente_DataNascimento" CHECK ("DataNascimento" <= NOW() OR "DataNascimento" IS NULL)
);

-- =====================================
-- TABELA DE PRODUTOS (CARDÁPIO)
-- =====================================
CREATE TABLE "Produtos" (
    "Id" SERIAL PRIMARY KEY,
    "Nome" VARCHAR(100) NOT NULL,
    "Descricao" VARCHAR(500) NULL,
    "Preco" DECIMAL(10,2) NOT NULL,
    "Categoria" VARCHAR(50) NOT NULL,
    "Disponivel" BOOLEAN NOT NULL DEFAULT TRUE,
    "DataCadastro" TIMESTAMP NOT NULL DEFAULT NOW(),
    "DataModificacao" TIMESTAMP NULL,
    
    -- Constraints
    CONSTRAINT "CK_Produto_Preco" CHECK ("Preco" > 0),
    CONSTRAINT "CK_Produto_Categoria" CHECK ("Categoria" IN (
        'Lanches', 'Bebidas', 'Sobremesas', 'Pratos Executivos', 
        'Saladas', 'Petiscos', 'Sucos', 'Cafés', 'Doces', 'Salgados'
    ))
);

-- =====================================
-- TABELA DE PEDIDOS
-- =====================================
CREATE TABLE "Pedidos" (
    "Id" SERIAL PRIMARY KEY,
    "ClienteId" INTEGER NOT NULL,
    "DataPedido" TIMESTAMP NOT NULL DEFAULT NOW(),
    "ValorTotal" DECIMAL(10,2) NOT NULL,
    "Status" VARCHAR(50) NOT NULL DEFAULT 'Pendente',
    "TipoEntrega" VARCHAR(50) NOT NULL DEFAULT 'Balcão',
    "Observacoes" VARCHAR(500) NULL,
    "DataCadastro" TIMESTAMP NOT NULL DEFAULT NOW(),
    "DataModificacao" TIMESTAMP NULL,
    
    -- Foreign Key
    CONSTRAINT "FK_Pedido_Cliente" FOREIGN KEY ("ClienteId") REFERENCES "Clientes"("Id") ON DELETE CASCADE,
    
    -- Constraints
    CONSTRAINT "CK_Pedido_ValorTotal" CHECK ("ValorTotal" > 0),
    CONSTRAINT "CK_Pedido_Status" CHECK ("Status" IN ('Pendente', 'Processando', 'Enviado', 'Entregue', 'Cancelado')),
    CONSTRAINT "CK_Pedido_TipoEntrega" CHECK ("TipoEntrega" IN ('Balcão', 'Mesa', 'Delivery', 'Viagem'))
);

-- =====================================
-- TABELA DE ITENS DO PEDIDO (CARRINHO)
-- =====================================
CREATE TABLE "ItensPedido" (
    "Id" SERIAL PRIMARY KEY,
    "PedidoId" INTEGER NOT NULL,
    "ProdutoId" INTEGER NOT NULL,
    "Quantidade" INTEGER NOT NULL,
    "PrecoUnitario" DECIMAL(10,2) NOT NULL,
    "Observacao" VARCHAR(200) NULL,
    
    -- Foreign Keys
    CONSTRAINT "FK_ItemPedido_Pedido" FOREIGN KEY ("PedidoId") REFERENCES "Pedidos"("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ItemPedido_Produto" FOREIGN KEY ("ProdutoId") REFERENCES "Produtos"("Id") ON DELETE RESTRICT,
    
    -- Constraints
    CONSTRAINT "CK_ItemPedido_Quantidade" CHECK ("Quantidade" > 0),
    CONSTRAINT "CK_ItemPedido_PrecoUnitario" CHECK ("PrecoUnitario" > 0)
);

-- =====================================
-- TABELA DE MIGRAÇÕES (Entity Framework)
-- =====================================
CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

-- =====================================
-- ÍNDICES PARA PERFORMANCE
-- =====================================

-- Índices para Clientes
CREATE INDEX "IX_Cliente_Nome" ON "Clientes"("Nome");
CREATE INDEX "IX_Cliente_Email" ON "Clientes"("Email");
CREATE INDEX "IX_Cliente_DataCadastro" ON "Clientes"("DataCadastro");

-- Índices para Produtos
CREATE INDEX "IX_Produto_Categoria" ON "Produtos"("Categoria");
CREATE INDEX "IX_Produto_Disponivel" ON "Produtos"("Disponivel");
CREATE INDEX "IX_Produto_Nome" ON "Produtos"("Nome");
CREATE INDEX "IX_Produto_Preco" ON "Produtos"("Preco");

-- Índices para Pedidos
CREATE INDEX "IX_Pedido_ClienteId" ON "Pedidos"("ClienteId");
CREATE INDEX "IX_Pedido_DataPedido" ON "Pedidos"("DataPedido");
CREATE INDEX "IX_Pedido_Status" ON "Pedidos"("Status");
CREATE INDEX "IX_Pedido_TipoEntrega" ON "Pedidos"("TipoEntrega");

-- Índices para ItensPedido
CREATE INDEX "IX_ItemPedido_PedidoId" ON "ItensPedido"("PedidoId");
CREATE INDEX "IX_ItemPedido_ProdutoId" ON "ItensPedido"("ProdutoId");

-- =====================================
-- REGISTROS DE MIGRAÇÃO
-- =====================================
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES
('20250906025738_InitialCreate', '9.0.0'),
('20250906145133_AddProdutoAndItemPedido', '9.0.0'),
('20250906152619_RemoveImagemUrlColumn', '9.0.0');

-- =====================================
-- SCRIPT COMPLETO - PRONTO PARA USO
-- =====================================
-- Este script cria todo o esquema do banco FastRequest
-- Sistema de cantina com clientes, produtos e pedidos
-- Versão atualizada sem campo ImagemUrl (removido)
-- 
-- Para usar: Execute este script em um banco PostgreSQL vazio
-- Resultado: Banco completo pronto para receber dados
-- =====================================
