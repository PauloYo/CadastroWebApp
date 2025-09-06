-- Script para criação do banco de dados CadastroWebApp
-- Compatível com PostgreSQL (Railway)

-- Remover tabelas se existirem (para recriação)
DROP TABLE IF EXISTS "Pedidos" CASCADE;
DROP TABLE IF EXISTS "Clientes" CASCADE;

-- Criar tabela Clientes
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

-- Criar tabela Pedidos
CREATE TABLE "Pedidos" (
    "Id" SERIAL PRIMARY KEY,
    "ClienteId" INTEGER NOT NULL,
    "DataPedido" TIMESTAMP NOT NULL DEFAULT NOW(),
    "ValorTotal" DECIMAL(10,2) NOT NULL,
    "Status" VARCHAR(50) NOT NULL DEFAULT 'Pendente',
    "Descricao" VARCHAR(500) NULL,
    "DataCadastro" TIMESTAMP NOT NULL DEFAULT NOW(),
    "DataModificacao" TIMESTAMP NULL,
    
    -- Foreign Key
    CONSTRAINT "FK_Pedido_Cliente" FOREIGN KEY ("ClienteId") REFERENCES "Clientes"("Id"),
    
    -- Constraints
    CONSTRAINT "CK_Pedido_ValorTotal" CHECK ("ValorTotal" > 0),
    CONSTRAINT "CK_Pedido_Status" CHECK ("Status" IN ('Pendente', 'Processando', 'Enviado', 'Entregue', 'Cancelado'))
);

-- Índices para melhor performance
CREATE INDEX "IX_Cliente_Nome" ON "Clientes"("Nome");
CREATE INDEX "IX_Cliente_Email" ON "Clientes"("Email");
CREATE INDEX "IX_Pedido_ClienteId" ON "Pedidos"("ClienteId");
CREATE INDEX "IX_Pedido_DataPedido" ON "Pedidos"("DataPedido");
CREATE INDEX "IX_Pedido_Status" ON "Pedidos"("Status");

-- =====================================
-- POPULAÇÃO DO BANCO COM DADOS DE TESTE
-- =====================================

-- Inserir clientes com dados limpos
INSERT INTO "Clientes" ("Nome", "Email", "DataNascimento", "Genero", "DataCadastro") VALUES
('João Silva Santos', 'joao.silva@email.com', '1985-03-15', 'Masculino', '2024-01-15 10:30:00'),
('Maria Oliveira Costa', 'maria.costa@gmail.com', '1992-07-22', 'Feminino', '2024-01-20 14:45:00'),
('Pedro Henrique Souza', 'pedro.souza@outlook.com', '1978-11-08', 'Masculino', '2024-02-03 09:15:00'),
('Ana Carolina Lima', 'ana.lima@yahoo.com', '1995-05-12', 'Feminino', '2024-02-10 16:20:00'),
('Carlos Roberto Mendes', 'carlos.mendes@hotmail.com', '1980-09-25', 'Masculino', '2024-02-15 11:10:00'),
('Juliana Fernandes', 'juliana.fernandes@empresa.com', '1988-01-30', 'Feminino', '2024-03-01 08:45:00'),
('Rafael Almeida', 'rafael.almeida@gmail.com', '1993-12-05', 'Masculino', '2024-03-05 13:30:00'),
('Camila Santos', 'camila.santos@email.com', '1991-04-18', 'Feminino', '2024-03-12 15:50:00'),
('Bruno Costa Silva', 'bruno.costa@outlook.com', '1987-08-14', 'Masculino', '2024-03-18 10:25:00'),
('Fernanda Rodrigues', 'fernanda.rodrigues@gmail.com', '1994-10-07', 'Feminino', '2024-03-25 12:40:00');

-- Inserir clientes com dados incompletos (para testes)
INSERT INTO "Clientes" ("Nome", "Email", "DataNascimento", "Genero", "DataCadastro") VALUES
('José', NULL, '1975-06-20', 'Masculino', '2024-04-01 09:00:00'),
('Ricardo Santos', 'ricardo@teste.com', '1990-03-10', NULL, '2024-04-03 14:30:00'),
('Patrícia Oliveira', 'patricia@email.com', '1989-12-25', 'Feminino', '2024-04-04 16:45:00'),
('ANTÔNIO CARLOS', 'antonio.carlos@email.com', NULL, 'Masculino', '2024-04-05 08:20:00'),
('Sônia Lima', NULL, '1982-07-15', NULL, '2024-04-06 11:35:00'),
('Marcos Vinícius da Silva e Santos', 'marcos.vinicius.santos@empresa.com.br', '1983-09-12', 'Masculino', '2024-04-08 15:10:00'),
('Helena', 'helena@email.com', '1999-11-30', 'Prefiro não informar', '2024-04-09 17:25:00'),
('Roberto Carvalho', NULL, '1976-05-08', 'Masculino', '2024-04-10 09:40:00');

-- Inserir pedidos com dados limpos
INSERT INTO "Pedidos" ("ClienteId", "DataPedido", "ValorTotal", "Status", "Descricao", "DataCadastro") VALUES
(1, '2024-01-16 10:30:00', 150.75, 'Entregue', 'Pedido de livros técnicos', '2024-01-16 10:30:00'),
(1, '2024-02-20 14:15:00', 89.90, 'Entregue', 'Material de escritório', '2024-02-20 14:15:00'),
(2, '2024-01-25 16:45:00', 275.50, 'Entregue', 'Equipamentos de informática', '2024-01-25 16:45:00'),
(2, '2024-03-10 09:20:00', 45.80, 'Processando', 'Acessórios para notebook', '2024-03-10 09:20:00'),
(3, '2024-02-05 11:30:00', 320.00, 'Enviado', 'Móveis para escritório', '2024-02-05 11:30:00'),
(4, '2024-02-12 13:45:00', 95.25, 'Entregue', 'Material de limpeza', '2024-02-12 13:45:00'),
(4, '2024-03-15 15:20:00', 180.40, 'Pendente', 'Produtos de higiene', '2024-03-15 15:20:00'),
(5, '2024-02-18 08:50:00', 450.00, 'Entregue', 'Equipamentos de segurança', '2024-02-18 08:50:00'),
(6, '2024-03-02 10:15:00', 75.30, 'Cancelado', 'Cancelado pelo cliente', '2024-03-02 10:15:00'),
(6, '2024-03-20 14:40:00', 220.85, 'Processando', 'Novo pedido após cancelamento', '2024-03-20 14:40:00'),
(7, '2024-03-08 12:25:00', 135.60, 'Enviado', 'Produtos eletrônicos', '2024-03-08 12:25:00'),
(8, '2024-03-14 16:30:00', 89.99, 'Entregue', 'Cosméticos e perfumaria', '2024-03-14 16:30:00'),
(9, '2024-03-22 09:45:00', 350.25, 'Pendente', 'Pedido em grande quantidade', '2024-03-22 09:45:00'),
(10, '2024-03-28 11:50:00', 125.40, 'Processando', 'Material esportivo', '2024-03-28 11:50:00');

-- Inserir pedidos com dados de teste variados
INSERT INTO "Pedidos" ("ClienteId", "DataPedido", "ValorTotal", "Status", "Descricao", "DataCadastro") VALUES
(1, '2024-04-01 00:00:00', 0.01, 'Pendente', NULL, '2024-04-01 00:00:00'),
(11, '2024-04-02 23:59:59', 9999.99, 'Entregue', 'Pedido com valor alto para teste', '2024-04-02 23:59:59'),
(12, '2024-04-03 12:30:00', 50.00, 'Pendente', '', '2024-04-03 12:30:00'),
(13, '2024-04-04 15:45:00', 100.50, 'Processando', 'Descrição normal', '2024-04-04 15:45:00'),
(2, '2024-04-05 08:15:00', 25.75, 'Cancelado', NULL, '2024-04-05 08:15:00'),
(3, '2024-01-01 00:00:01', 199.90, 'Entregue', 'Pedido do ano novo', '2024-01-01 00:00:01');

-- Confirmação de inserção
SELECT 'Dados inseridos com sucesso!' as "Resultado";
