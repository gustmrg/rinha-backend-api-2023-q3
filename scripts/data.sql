CREATE TABLE IF NOT EXISTS pessoas (
    id UUID PRIMARY KEY,
    apelido VARCHAR(32) UNIQUE NOT NULL,
    nome VARCHAR(100) NOT NULL,
    nascimento DATE NOT NULL,
    stack VARCHAR(32)[] NULL
);

BEGIN;
    CREATE INDEX IF NOT EXISTS idx_pessoas_nome ON pessoas USING btree(nome);
    CREATE INDEX IF NOT EXISTS idx_pessoas_apelido ON pessoas USING btree(apelido);
COMMIT;