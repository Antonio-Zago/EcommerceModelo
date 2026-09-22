alter table produto_imagens
add column principal bool;

-- Marca todas as imagens (seed) como principais
update produto_imagens
set principal = true;