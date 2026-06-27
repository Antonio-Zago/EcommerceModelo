

-- Novas colunas de item
ALTER TABLE compras_itens
    ADD COLUMN quantidade     INT            NOT NULL DEFAULT 1,
    ADD COLUMN preco_unitario NUMERIC(10,2)  NOT NULL DEFAULT 0,
    ADD COLUMN nome_produto   VARCHAR(200)   NOT NULL DEFAULT '',
    ADD COLUMN tamanho_id     INT            REFERENCES opcao_tamanhos(id);

-- Expande a tabela de compras
ALTER TABLE compras
    ADD COLUMN usuario              INT            NOT NULL DEFAULT 0,
    ADD COLUMN status               VARCHAR(30)    NOT NULL DEFAULT 'confirmado',
    ADD COLUMN forma_pagamento      VARCHAR(30)    NOT NULL DEFAULT '',
    ADD COLUMN endereco_cep         VARCHAR(10)    NOT NULL DEFAULT '',
    ADD COLUMN endereco_rua         VARCHAR(200)   NOT NULL DEFAULT '',
    ADD COLUMN endereco_numero      VARCHAR(20)    NOT NULL DEFAULT '',
    ADD COLUMN endereco_complemento VARCHAR(100),
    ADD COLUMN endereco_bairro      VARCHAR(100)   NOT NULL DEFAULT '',
    ADD COLUMN endereco_cidade      VARCHAR(100)   NOT NULL DEFAULT '',
    ADD COLUMN endereco_uf          CHAR(2)        NOT NULL DEFAULT '',
    ADD COLUMN total                NUMERIC(10,2)  NOT NULL DEFAULT 0,
    ADD COLUMN criado_em            TIMESTAMPTZ    NOT NULL DEFAULT NOW(),
    ADD COLUMN entrega_prevista     DATE           NOT NULL DEFAULT CURRENT_DATE;

ALTER TABLE compras
    ADD CONSTRAINT fk_compra_usuario FOREIGN KEY (usuario) REFERENCES usuarios(id);
