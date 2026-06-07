-- Remove a tabela de relacionamento N:N entre categorias e produtos
DROP TABLE IF EXISTS categorias_produtos;

-- Adiciona coluna FK de categoria diretamente na tabela produtos
ALTER TABLE produtos
    ADD COLUMN categoria INT,
    ADD CONSTRAINT fk_produto_categoria
        FOREIGN KEY (categoria)
        REFERENCES categorias(id);
