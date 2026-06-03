CREATE TABLE produto_estoque (
    Id SERIAL PRIMARY KEY,
    Quantidade INT,
    Tamanho VARCHAR(100),
    Produto INT NOT NULL,
    CONSTRAINT fk_produto_estoque_produto FOREIGN KEY (Produto) REFERENCES produtos(id)
);