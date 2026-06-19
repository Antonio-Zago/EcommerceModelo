-- Cria tabela de usuários
CREATE TABLE usuarios (
    id         SERIAL PRIMARY KEY,
    nome       VARCHAR(200) NOT NULL,
    email      VARCHAR(200) NOT NULL UNIQUE,
    senha_hash VARCHAR(100) NOT NULL
);

-- Cria tabela de papéis (ex: Admin, Cliente)
CREATE TABLE papeis (
    id   SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL UNIQUE
);

-- Tabela de junção entre usuários e papéis
CREATE TABLE papel_usuarios (
    usuario INT NOT NULL,
    papel   INT NOT NULL,
    CONSTRAINT pk_papel_usuarios
        PRIMARY KEY (usuario, papel),
    CONSTRAINT fk_papel_usuarios_usuario
        FOREIGN KEY (usuario)
        REFERENCES usuarios(id),
    CONSTRAINT fk_papel_usuarios_papel
        FOREIGN KEY (papel)
        REFERENCES papeis(id)
);
