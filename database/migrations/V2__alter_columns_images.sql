ALTER TABLE produto_imagens
DROP COLUMN imagemBase64;

ALTER TABLE produto_imagens
ADD imagemUrl VARCHAR(500);

ALTER TABLE categorias
ADD imagemUrl VARCHAR(500);

drop table compras_imagens


CREATE TABLE compras_itens (
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