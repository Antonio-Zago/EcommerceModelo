const rangeInput = document.getElementById('faixa-preco');
const rangeOutput = document.getElementById('faixa-preco-value');

// Set initial value
rangeOutput.textContent = rangeInput.value;

rangeInput.addEventListener('input', function () {
    rangeOutput.textContent = this.value + "R$";
});