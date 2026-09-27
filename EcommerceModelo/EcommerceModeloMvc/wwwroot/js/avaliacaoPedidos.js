// Preenche o modal de avaliação com os dados do produto clicado
document.addEventListener('DOMContentLoaded', () => {
    const modal = document.getElementById('modalAvaliacao');
    if (!modal) return;

    modal.addEventListener('show.bs.modal', (event) => {
        const botao = event.relatedTarget;
        const nota = botao.dataset.nota;

        document.getElementById('avaliacaoProdutoId').value = botao.dataset.produtoId;
        document.getElementById('avaliacaoProdutoNome').textContent = botao.dataset.produtoNome;
        document.getElementById('avaliacaoDescricao').value = botao.dataset.descricao ?? '';

        modal.querySelectorAll('input[name="Nota"]').forEach((radio) => {
            radio.checked = radio.value === nota;
        });
    });
});
