-- Script para adicionar as tabelas Produto e ItemPedido ao sistema FastRequest

-- Criar tabela Produtos
CREATE TABLE IF NOT EXISTS "Produtos" (
    "Id" SERIAL PRIMARY KEY,
    "Nome" VARCHAR(100) NOT NULL,
    "Descricao" VARCHAR(500),
    "Preco" DECIMAL(10,2) NOT NULL,
    "Categoria" VARCHAR(50) NOT NULL,
    "Disponivel" BOOLEAN NOT NULL DEFAULT TRUE,
    "ImagemUrl" VARCHAR(200),
    "DataCadastro" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "DataModificacao" TIMESTAMP
);

-- Criar tabela ItensPedido
CREATE TABLE IF NOT EXISTS "ItensPedido" (
    "Id" SERIAL PRIMARY KEY,
    "PedidoId" INTEGER NOT NULL,
    "ProdutoId" INTEGER NOT NULL,
    "Quantidade" INTEGER NOT NULL,
    "PrecoUnitario" DECIMAL(10,2) NOT NULL,
    "Observacao" VARCHAR(200),
    FOREIGN KEY ("PedidoId") REFERENCES "Pedidos"("Id") ON DELETE CASCADE,
    FOREIGN KEY ("ProdutoId") REFERENCES "Produtos"("Id") ON DELETE RESTRICT
);

-- Adicionar novos campos à tabela Pedidos (se não existirem)
ALTER TABLE "Pedidos" 
ADD COLUMN IF NOT EXISTS "Observacoes" VARCHAR(500),
ADD COLUMN IF NOT EXISTS "NumeroMesa" INTEGER,
ADD COLUMN IF NOT EXISTS "TipoEntrega" VARCHAR(50) DEFAULT 'Balcão';

-- Inserir produtos de exemplo para a cantina
INSERT INTO "Produtos" ("Nome", "Descricao", "Preco", "Categoria", "ImagemUrl") VALUES
-- Lanches
('X-Burger Clássico', 'Hambúrguer bovino, queijo, alface, tomate, cebola e molho especial', 15.90, 'Lanches', '/images/x-burger.jpg'),
('X-Tudo Completo', 'Hambúrguer bovino, queijo, presunto, ovo, bacon, alface, tomate e batata palha', 22.90, 'Lanches', '/images/x-tudo.jpg'),
('X-Frango Grelhado', 'Peito de frango grelhado, queijo, alface, tomate e maionese', 18.50, 'Lanches', '/images/x-frango.jpg'),
('Bauru Tradicional', 'Presunto, queijo, tomate, pickles e orégano no pão francês', 12.90, 'Lanches', '/images/bauru.jpg'),

-- Pratos Executivos
('Prato Feito Completo', 'Arroz, feijão, bife acebolado, ovo frito, farofa e salada', 18.90, 'Pratos Executivos', '/images/pf-completo.jpg'),
('Frango à Parmegiana', 'Peito de frango empanado com molho de tomate e queijo, acompanha arroz e batata frita', 24.90, 'Pratos Executivos', '/images/frango-parmegiana.jpg'),
('Feijoada Completa', 'Feijoada tradicional com arroz, farofa, couve refogada e laranja', 28.90, 'Pratos Executivos', '/images/feijoada.jpg'),
('Lasanha à Bolonhesa', 'Lasanha de carne com molho bolonhesa e queijo gratinado', 22.90, 'Pratos Executivos', '/images/lasanha.jpg'),

-- Saladas
('Salada Caesar', 'Alface americana, frango grelhado, croutons, parmesão e molho caesar', 16.90, 'Saladas', '/images/caesar.jpg'),
('Salada Tropical', 'Mix de folhas, manga, abacaxi, castanha de caju e molho de maracujá', 14.90, 'Saladas', '/images/tropical.jpg'),
('Salada Caprese', 'Tomate, mussarela de búfala, manjericão e azeite extra virgem', 18.90, 'Saladas', '/images/caprese.jpg'),

-- Bebidas
('Refrigerante Lata', 'Coca-Cola, Guaraná, Fanta ou Sprite - 350ml', 4.50, 'Bebidas', '/images/refri-lata.jpg'),
('Suco Natural Laranja', 'Suco de laranja natural - 300ml', 6.90, 'Sucos', '/images/suco-laranja.jpg'),
('Suco Natural Abacaxi', 'Suco de abacaxi natural com hortelã - 300ml', 7.50, 'Sucos', '/images/suco-abacaxi.jpg'),
('Água Mineral', 'Água mineral sem gás - 500ml', 3.50, 'Bebidas', '/images/agua.jpg'),

-- Cafés
('Café Expresso', 'Café expresso tradicional', 3.50, 'Cafés', '/images/expresso.jpg'),
('Cappuccino', 'Café com leite vaporizado e canela', 6.90, 'Cafés', '/images/cappuccino.jpg'),
('Café com Leite', 'Café com leite cremoso', 4.50, 'Cafés', '/images/cafe-leite.jpg'),

-- Sobremesas
('Pudim de Leite', 'Pudim de leite condensado com calda de caramelo', 8.90, 'Sobremesas', '/images/pudim.jpg'),
('Brigadeiro Gourmet', 'Brigadeiro artesanal com granulado especial - 3 unidades', 9.90, 'Sobremesas', '/images/brigadeiro.jpg'),
('Mousse de Chocolate', 'Mousse de chocolate meio amargo com chantilly', 10.90, 'Sobremesas', '/images/mousse.jpg'),
('Açaí na Tigela', 'Açaí com granola, banana e mel', 12.90, 'Sobremesas', '/images/acai.jpg'),

-- Petiscos
('Batata Frita Simples', 'Porção de batata frita crocante', 8.90, 'Petiscos', '/images/batata-frita.jpg'),
('Batata Frita com Bacon', 'Batata frita com bacon e queijo derretido', 14.90, 'Petiscos', '/images/batata-bacon.jpg'),
('Coxinha de Frango', 'Coxinha artesanal de frango - 4 unidades', 12.90, 'Petiscos', '/images/coxinha.jpg'),
('Pastéis Variados', 'Pastéis de carne, queijo ou pizza - 4 unidades', 15.90, 'Petiscos', '/images/pasteis.jpg');

-- Inserir alguns itens de pedido de exemplo
INSERT INTO "ItensPedido" ("PedidoId", "ProdutoId", "Quantidade", "PrecoUnitario", "Observacao") 
SELECT 
    p."Id" as "PedidoId",
    pr."Id" as "ProdutoId",
    (RANDOM() * 3 + 1)::INTEGER as "Quantidade",
    pr."Preco" as "PrecoUnitario",
    CASE 
        WHEN RANDOM() > 0.7 THEN 'Sem cebola'
        WHEN RANDOM() > 0.8 THEN 'Bem passado'
        WHEN RANDOM() > 0.9 THEN 'Extra molho'
        ELSE NULL
    END as "Observacao"
FROM "Pedidos" p
CROSS JOIN "Produtos" pr
WHERE RANDOM() > 0.8  -- Adiciona itens aleatoriamente a alguns pedidos
LIMIT 30;

-- Atualizar os valores totais dos pedidos baseado nos itens
UPDATE "Pedidos" 
SET "ValorTotal" = (
    SELECT COALESCE(SUM(ip."Quantidade" * ip."PrecoUnitario"), 0)
    FROM "ItensPedido" ip 
    WHERE ip."PedidoId" = "Pedidos"."Id"
)
WHERE EXISTS (
    SELECT 1 FROM "ItensPedido" ip WHERE ip."PedidoId" = "Pedidos"."Id"
);

COMMIT;
