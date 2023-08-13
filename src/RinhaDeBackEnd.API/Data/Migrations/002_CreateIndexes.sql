CREATE INDEX idx_pessoas_nome ON pessoas USING btree(nome);
CREATE INDEX idx_pessoas_apelido ON pessoas USING btree(apelido);
CREATE INDEX idx_pessoas_stack ON pessoas USING gin(stack);
