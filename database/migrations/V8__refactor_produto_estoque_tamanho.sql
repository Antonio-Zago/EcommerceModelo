-- Cria tabela de tipos de tamanho (ex: Roupa, Calçado, Único)
CREATE TABLE tipos_tamanhos (
    Id   SERIAL PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL
);

-- Cria tabela de opções de tamanho (ex: P, M, G, 38, 40...)
CREATE TABLE opcao_tamanhos (
    Id        SERIAL PRIMARY KEY,
    Descricao VARCHAR(100) NOT NULL,
    Tipo      INT NOT NULL,
    CONSTRAINT fk_opcao_tamanho_tipo
        FOREIGN KEY (Tipo)
        REFERENCES tipos_tamanhos(Id)
);

-- Remove a coluna Tamanho VARCHAR e adiciona FK para opcao_tamanho
ALTER TABLE produto_estoque
    DROP COLUMN Tamanho,
    ADD COLUMN Tamanho INT,
    ADD CONSTRAINT fk_produto_estoque_tamanho
        FOREIGN KEY (Tamanho)
        REFERENCES opcao_tamanhos(Id);
