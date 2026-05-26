// Filtro de faixa de preço
const rangeInput = document.getElementById('faixa-preco');
const rangeOutput = document.getElementById('faixa-preco-value');

if (rangeInput && rangeOutput) {
    rangeOutput.textContent = rangeInput.value;

    rangeInput.addEventListener('input', function () {
        rangeOutput.textContent = this.value;
    });
}

// Botões de tamanho (toggle ativo)
document.querySelectorAll('.size-btn').forEach(function (btn) {
    btn.addEventListener('click', function () {
        this.classList.toggle('active');
    });
});
