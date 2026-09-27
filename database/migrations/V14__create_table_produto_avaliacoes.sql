-- Cria tabela de avaliações de produtos
CREATE TABLE produto_avaliacoes (
    id        SERIAL PRIMARY KEY,
    produto   INT           NOT NULL,
    usuario   INT           NOT NULL,
    descricao VARCHAR(2000),
    nota      INT           NOT NULL,
    CONSTRAINT fk_produto_avaliacoes_produto
        FOREIGN KEY (produto)
        REFERENCES produtos(id),
    CONSTRAINT fk_produto_avaliacoes_usuario
        FOREIGN KEY (usuario)
        REFERENCES usuarios(id),
    CONSTRAINT ck_produto_avaliacoes_nota
        CHECK (nota BETWEEN 1 AND 5)
);

CREATE INDEX idx_produto_avaliacoes_produto ON produto_avaliacoes (produto);
