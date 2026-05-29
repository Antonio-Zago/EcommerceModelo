-- ── Categorias ───────────────────────────────────────────────────────────────
INSERT INTO categorias (nome, imagemurl) VALUES
    ('Camisetas',  '/images/categorias/camisetas.jpg'),
    ('Calças',     '/images/categorias/calcas.jpg'),
    ('Vestidos',   '/images/categorias/vestidos.jpg'),
    ('Jaquetas',   '/images/categorias/jaquetas.jpg'),
    ('Shorts',     '/images/categorias/shorts.jpg');

-- ── Produtos ─────────────────────────────────────────────────────────────────
INSERT INTO produtos (nome, preco, descricao, qtdestoque, tamanho) VALUES
    ('Camiseta Básica Branca',      49.90,  'Camiseta 100% algodão, corte regular, perfeita para o dia a dia.',         30, 'M'),
    ('Camiseta Estampada Preta',    69.90,  'Camiseta com estampa exclusiva, tecido leve e respirável.',                 20, 'G'),
    ('Calça Jeans Slim',           159.90,  'Calça jeans com modelagem slim, cinco bolsos, lavagem escura.',             15, '42'),
    ('Calça Moletom Cinza',         99.90,  'Calça confortável para uso casual, cintura elástica com cordão.',           25, 'M'),
    ('Vestido Floral Midi',        189.90,  'Vestido midi com estampa floral, tecido fluido, alças finas.',              10, 'P'),
    ('Vestido Preto Básico',       149.90,  'Vestido curto preto, versátil para diversas ocasiões.',                     18, 'M'),
    ('Jaqueta Jeans Feminina',     219.90,  'Jaqueta jeans com detalhes destroyed, modelagem oversized.',                12, 'G'),
    ('Jaqueta Bomber Verde',       249.90,  'Jaqueta bomber com forro interno, bolsos laterais com zíper.',               8, 'GG'),
    ('Short Jeans Feminino',        89.90,  'Short jeans de cintura alta, barra desfiada, modelagem confortável.',       22, '38'),
    ('Short Moletom Masculino',     79.90,  'Short de moletom com bolso lateral, elástico na cintura.',                  28, 'G');

-- ── Relação categorias × produtos ────────────────────────────────────────────
INSERT INTO categorias_produtos (categoria, produto) VALUES
    (1, 1),  -- Camisetas → Camiseta Básica Branca
    (1, 2),  -- Camisetas → Camiseta Estampada Preta
    (2, 3),  -- Calças    → Calça Jeans Slim
    (2, 4),  -- Calças    → Calça Moletom Cinza
    (3, 5),  -- Vestidos  → Vestido Floral Midi
    (3, 6),  -- Vestidos  → Vestido Preto Básico
    (4, 7),  -- Jaquetas  → Jaqueta Jeans Feminina
    (4, 8),  -- Jaquetas  → Jaqueta Bomber Verde
    (5, 9),  -- Shorts    → Short Jeans Feminino
    (5, 10); -- Shorts    → Short Moletom Masculino

-- ── Imagens dos produtos ─────────────────────────────────────────────────────
INSERT INTO produto_imagens (produto, imagemurl) VALUES
    (1,  '/images/produtos/camiseta-branca.jpg'),
    (2,  '/images/produtos/camiseta-preta.jpg'),
    (3,  '/images/produtos/calca-jeans-slim.jpg'),
    (4,  '/images/produtos/calca-moletom.jpg'),
    (5,  '/images/produtos/vestido-floral.jpg'),
    (6,  '/images/produtos/vestido-preto.jpg'),
    (7,  '/images/produtos/jaqueta-jeans.jpg'),
    (8,  '/images/produtos/jaqueta-bomber.jpg'),
    (9,  '/images/produtos/short-jeans.jpg'),
    (10, '/images/produtos/short-moletom.jpg');
