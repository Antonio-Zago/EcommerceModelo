-- Status do produto: 1 = Ativo, 2 = Arquivado
ALTER TABLE produtos
    ADD COLUMN status SMALLINT NOT NULL DEFAULT 1;
