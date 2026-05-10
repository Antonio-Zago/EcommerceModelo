CREATE TABLE Produtos (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Preco NUMERIC(10,2) NOT NULL,
    Descricao VARCHAR(5000),
    QtdEstoque INT,
    Tamanho VARCHAR(100)
);

CREATE TABLE categorias (
    Id SERIAL PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL
);

CREATE TABLE categorias_produtos (
    categoria INT NOT NULL,
    produto INT NOT NULL,

    PRIMARY KEY (categoria, produto),

    CONSTRAINT fk_categoria
        FOREIGN KEY (categoria)
        REFERENCES categorias(id),

    CONSTRAINT fk_produto
        FOREIGN KEY (produto)
        REFERENCES produtos(id)
);

CREATE TABLE produto_imagens (
    id SERIAL PRIMARY KEY,
    produto INT NOT NULL,
    imagemBase64 TEXT NOT NULL,

    CONSTRAINT fk_produto_imagem
        FOREIGN KEY (produto)
        REFERENCES produtos(id)
);

CREATE TABLE compras (
    Id SERIAL PRIMARY KEY
);

CREATE TABLE compras_imagens (
    compra INT NOT NULL,
    produto INT NOT NULL,

    PRIMARY KEY (compra, produto),

    CONSTRAINT fk_compra
        FOREIGN KEY (compra)
        REFERENCES compras(id),

    CONSTRAINT fk_produto
        FOREIGN KEY (produto)
        REFERENCES produtos(id)
);

