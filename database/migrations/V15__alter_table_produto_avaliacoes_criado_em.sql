-- Data e hora em que a avaliação foi feita
ALTER TABLE produto_avaliacoes
    ADD COLUMN criado_em TIMESTAMPTZ NOT NULL DEFAULT NOW();
