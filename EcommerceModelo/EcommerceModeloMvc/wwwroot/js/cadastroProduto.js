(function () {
    'use strict';

    let arquivos = []; // { file, objectUrl, principal }

    const inputFile = document.getElementById('inputImagens');
    const previewGrid = document.getElementById('previewGrid');
    const inputPrincipalIndex = document.getElementById('imagemPrincipalIndex');
    const btnAdicionar = document.getElementById('btnAdicionarImagem');
    const form = document.getElementById('formCadastroProduto');
    const erroImagens = document.getElementById('erroImagens');

    btnAdicionar.addEventListener('click', () => inputFile.click());

    inputFile.addEventListener('change', () => {
        const novos = Array.from(inputFile.files);
        novos.forEach(f => arquivos.push({ file: f, objectUrl: URL.createObjectURL(f), principal: false }));
        if (arquivos.length === 1) arquivos[0].principal = true;
        inputFile.value = '';
        renderizar();
    });

    function renderizar() {
        previewGrid.innerHTML = '';
        atualizarInputPrincipal();

        arquivos.forEach((item, idx) => {
            const card = document.createElement('div');
            card.className = 'preview-card' + (item.principal ? ' principal' : '');
            card.dataset.idx = idx;

            card.innerHTML = `
                ${item.principal ? '<span class="badge bg-primary badge-principal">Principal</span>' : ''}
                <div class="img-wrapper">
                    <img src="${item.objectUrl}" alt="Preview ${idx + 1}" data-scale="100">
                </div>
                <div class="card-actions">
                    <button type="button" class="btn-action btn-principal" title="Marcar como principal">
                        ${item.principal
                            ? '<svg width="14" height="14" fill="currentColor" viewBox="0 0 24 24"><path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/></svg>'
                            : '<svg width="14" height="14" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24"><path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"/></svg>'
                        }
                        Principal
                    </button>
                    <button type="button" class="btn-action btn-remover" title="Remover imagem">
                        <svg width="14" height="14" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
                            <polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/><path d="M10 11v6"/><path d="M14 11v6"/><path d="M9 6V4h6v2"/>
                        </svg>
                        Remover
                    </button>
                </div>
                <div class="scale-control">
                    <label>Zoom: <span class="scale-value">100%</span></label>
                    <input type="range" min="50" max="200" value="100" class="form-range">
                </div>`;

            // Slider de zoom
            const slider = card.querySelector('input[type=range]');
            const scaleLabel = card.querySelector('.scale-value');
            const img = card.querySelector('img');
            slider.addEventListener('input', () => {
                const v = slider.value;
                img.style.transform = `scale(${v / 100})`;
                scaleLabel.textContent = `${v}%`;
            });

            // Marcar principal
            card.querySelector('.btn-principal').addEventListener('click', () => {
                arquivos.forEach(a => a.principal = false);
                arquivos[idx].principal = true;
                renderizar();
            });

            // Remover
            card.querySelector('.btn-remover').addEventListener('click', () => {
                URL.revokeObjectURL(item.objectUrl);
                arquivos.splice(idx, 1);
                if (arquivos.length > 0 && !arquivos.some(a => a.principal))
                    arquivos[0].principal = true;
                renderizar();
            });

            previewGrid.appendChild(card);
        });

        esconderErroImagens();
    }

    function atualizarInputPrincipal() {
        const idx = arquivos.findIndex(a => a.principal);
        inputPrincipalIndex.value = idx >= 0 ? idx : 0;
    }

    function esconderErroImagens() {
        if (erroImagens) erroImagens.style.display = 'none';
    }

    // Substituir o input de arquivos do form antes do submit
    form.addEventListener('submit', function (e) {
        if (arquivos.length === 0) {
            e.preventDefault();
            if (erroImagens) {
                erroImagens.textContent = 'Adicione ao menos uma imagem do produto.';
                erroImagens.style.display = 'block';
            }
            return;
        }

        // Recriar o input de arquivo com os arquivos selecionados via DataTransfer
        const dt = new DataTransfer();
        arquivos.forEach(a => dt.items.add(a.file));
        inputFile.files = dt.files;
        inputFile.style.display = 'block'; // garantir que o campo seja enviado
    });
})();
