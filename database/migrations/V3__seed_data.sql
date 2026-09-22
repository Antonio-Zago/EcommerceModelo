-- ── Categorias ───────────────────────────────────────────────────────────────
INSERT INTO categorias (nome, imagemurl) VALUES
    ('Camisetas',  '/images/categorias/camisetas.png'),
    ('Calças',     '/images/categorias/calcas.png'),
    ('Vestidos',   '/images/categorias/vestidos.png'),
    ('Jaquetas',   '/images/categorias/jaquetas.png'),
    ('Shorts',     '/images/categorias/shorts.png');

-- ── Produtos ─────────────────────────────────────────────────────────────────
INSERT INTO produtos (nome, preco, descricao, categoria, qtdestoque, tamanho) VALUES
    ('Camiseta Básica Branca',      49.90,  'Camiseta 100% algodão, corte regular, perfeita para o dia a dia.', 1,        30, 'M'),
    ('Camiseta Estampada Preta',    69.90,  'Camiseta com estampa exclusiva, tecido leve e respirável.',  1,               20, 'G'),
    ('Calça Jeans Slim',           159.90,  'Calça jeans com modelagem slim, cinco bolsos, lavagem escura.', 2,            15, '42'),
    ('Calça Moletom Cinza',         99.90,  'Calça confortável para uso casual, cintura elástica com cordão.', 2,          25, 'M'),
    ('Vestido Floral Midi',        189.90,  'Vestido midi com estampa floral, tecido fluido, alças finas.',  3,            10, 'P'),
    ('Vestido Preto Básico',       149.90,  'Vestido curto preto, versátil para diversas ocasiões.', 3,               18, 'M'),
    ('Jaqueta Jeans Feminina',     219.90,  'Jaqueta jeans com detalhes destroyed, modelagem oversized.',   4,             12, 'G'),
    ('Jaqueta Bomber Verde',       249.90,  'Jaqueta bomber com forro interno, bolsos laterais com zíper.',   4,            8, 'GG'),
    ('Short Jeans Feminino',        89.90,  'Short jeans de cintura alta, barra desfiada, modelagem confortável.', 5,      22, '38'),
    ('Short Moletom Masculino',     79.90,  'Short de moletom com bolso lateral, elástico na cintura.',     5,             28, 'G');

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
    (1,  '/images/produtos/camiseta-branca.png'),
    (2,  '/images/produtos/camiseta-preta.png'),
    (3,  '/images/produtos/calca-jeans-slim.png'),
    (4,  '/images/produtos/calca-moletom.png'),
    (5,  '/images/produtos/vestido-floral.png'),
    (6,  '/images/produtos/vestido-preto.png'),
    (7,  '/images/produtos/jaqueta-jeans.png'),
    (8,  '/images/produtos/jaqueta-bomber.png'),
    (9,  '/images/produtos/short-jeans.png'),
    (10, '/images/produtos/short-moletom.png');
