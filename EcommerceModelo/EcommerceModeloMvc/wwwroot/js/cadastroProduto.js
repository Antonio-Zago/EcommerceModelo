(function () {
    'use strict';

    /* ═══════════════════════════════════════
       IMAGENS
    ═══════════════════════════════════════ */
    let arquivos = []; // { file, objectUrl, principal }

    const inputFile        = document.getElementById('inputImagens');
    const previewGrid      = document.getElementById('previewGrid');
    const inputPrincipal   = document.getElementById('imagemPrincipalIndex');
    const btnAdicionarImg  = document.getElementById('btnAdicionarImagem');
    const form             = document.getElementById('formCadastroProduto');
    const erroImagens      = document.getElementById('erroImagens');

    btnAdicionarImg.addEventListener('click', () => inputFile.click());

    inputFile.addEventListener('change', () => {
        Array.from(inputFile.files).forEach(f =>
            arquivos.push({ file: f, objectUrl: URL.createObjectURL(f), principal: false })
        );
        if (arquivos.length === 1) arquivos[0].principal = true;
        inputFile.value = '';
        renderizarImagens();
    });

    function renderizarImagens() {
        previewGrid.innerHTML = '';
        atualizarInputPrincipal();

        arquivos.forEach((item, idx) => {
            const card = document.createElement('div');
            card.className = 'preview-card' + (item.principal ? ' principal' : '');
            card.innerHTML = `
                ${item.principal ? '<span class="badge bg-primary badge-principal">Principal</span>' : ''}
                <div class="img-wrapper">
                    <img src="${item.objectUrl}" alt="Preview ${idx + 1}">
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
                            <polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/>
                            <path d="M10 11v6"/><path d="M14 11v6"/><path d="M9 6V4h6v2"/>
                        </svg>
                        Remover
                    </button>
                </div>
                <div class="scale-control">
                    <label>Zoom: <span class="scale-value">100%</span></label>
                    <input type="range" min="50" max="200" value="100" class="form-range">
                </div>`;

            const slider = card.querySelector('input[type=range]');
            const scaleLabel = card.querySelector('.scale-value');
            const img = card.querySelector('img');
            slider.addEventListener('input', () => {
                img.style.transform = `scale(${slider.value / 100})`;
                scaleLabel.textContent = `${slider.value}%`;
            });

            card.querySelector('.btn-principal').addEventListener('click', () => {
                arquivos.forEach(a => a.principal = false);
                arquivos[idx].principal = true;
                renderizarImagens();
            });

            card.querySelector('.btn-remover').addEventListener('click', () => {
                URL.revokeObjectURL(item.objectUrl);
                arquivos.splice(idx, 1);
                if (arquivos.length > 0 && !arquivos.some(a => a.principal))
                    arquivos[0].principal = true;
                renderizarImagens();
            });

            previewGrid.appendChild(card);
        });

        if (erroImagens) erroImagens.style.display = 'none';
    }

    function atualizarInputPrincipal() {
        const idx = arquivos.findIndex(a => a.principal);
        inputPrincipal.value = idx >= 0 ? idx : 0;
    }

    /* ═══════════════════════════════════════
       TAMANHOS DINÂMICOS
    ═══════════════════════════════════════ */
    // window.OPCOES_TAMANHO = [{ tipo: "Roupa", opcoes: [{id, descricao}] }, ...]
    const opcoesPorTipo   = window.OPCOES_TAMANHO || [];
    const rowsContainer   = document.getElementById('tamanhoEstoqueRows');
    const btnAdicionarTam = document.getElementById('btnAdicionarTamanho');
    const erroTamanhos    = document.getElementById('erroTamanhos');

    // Select de tipo único, fora das linhas
    const selTipoGlobal = rowsContainer.querySelector('.sel-tipo');

    btnAdicionarTam.addEventListener('click', adicionarLinha);

    // Ao mudar o tipo: repopula todos os sel-opcao existentes e limpa seleção
    selTipoGlobal.addEventListener('change', () => {
        rowsContainer.querySelectorAll('.tamanho-row').forEach(row => {
            popularOpcoes(row.querySelector('.sel-opcao'));
        });
    });

    // Vincula remoção na primeira linha (já renderizada no HTML)
    rowsContainer.querySelectorAll('.btn-remover-tamanho').forEach(btn => {
        btn.addEventListener('click', () => {
            btn.closest('.tamanho-row').remove();
            reindexarLinhas();
            atualizarBotoesRemover();
        });
    });

    function adicionarLinha() {
        const idx = rowsContainer.querySelectorAll('.tamanho-row').length;

        const row = document.createElement('div');
        row.className = 'tamanho-row d-flex align-items-center gap-2 mb-2';
        row.innerHTML = `
            <select name="Tamanhos[${idx}].TamanhoId" class="form-select form-select-sm sel-opcao"
                    style="max-width:150px;" required ${selTipoGlobal.value ? '' : 'disabled'}>
                <option value="">Opção</option>
            </select>
            <input name="Tamanhos[${idx}].QtdEstoque" type="number" min="0" placeholder="Qtd."
                   class="form-control form-control-sm" style="max-width:100px;" required />
            <button type="button" class="btn btn-outline-danger btn-sm btn-remover-tamanho" title="Remover">
                <svg width="13" height="13" fill="none" stroke="currentColor" stroke-width="2" viewBox="0 0 24 24">
                    <polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/>
                </svg>
            </button>`;

        // Se já há um tipo selecionado, popula as opções imediatamente
        if (selTipoGlobal.value) {
            popularOpcoes(row.querySelector('.sel-opcao'));
        }

        row.querySelector('.btn-remover-tamanho').addEventListener('click', () => {
            row.remove();
            reindexarLinhas();
            atualizarBotoesRemover();
        });

        rowsContainer.appendChild(row);
        atualizarBotoesRemover();
        if (erroTamanhos) erroTamanhos.style.display = 'none';
    }

    // Popula um sel-opcao com base no tipo global atualmente selecionado
    function popularOpcoes(selOpcao) {
        const grupo = opcoesPorTipo.find(g => g.tipo === selTipoGlobal.value);
        selOpcao.innerHTML = '<option value="">Opção</option>';

        if (grupo && grupo.opcoes.length > 0) {
            grupo.opcoes.forEach(op => {
                const opt = document.createElement('option');
                opt.value       = op.id;
                opt.textContent = op.descricao;
                selOpcao.appendChild(opt);
            });
            selOpcao.disabled = false;
        } else {
            selOpcao.disabled = true;
        }
    }

    // Atualiza os índices name após remoção de uma linha
    function reindexarLinhas() {
        rowsContainer.querySelectorAll('.tamanho-row').forEach((row, i) => {
            row.querySelector('.sel-opcao').name         = `Tamanhos[${i}].TamanhoId`;
            row.querySelector('input[type=number]').name = `Tamanhos[${i}].QtdEstoque`;
        });
    }

    // Desabilita o botão remover quando só há uma linha
    function atualizarBotoesRemover() {
        const rows = rowsContainer.querySelectorAll('.tamanho-row');
        rows.forEach(r => {
            r.querySelector('.btn-remover-tamanho').disabled = rows.length === 1;
        });
    }

    /* ═══════════════════════════════════════
      SUBMIT
   ═══════════════════════════════════════ */
    form.addEventListener('submit', function (e) {
        let valido = true;

        // Validar tamanhos
        const rows = rowsContainer.querySelectorAll('.tamanho-row');
        const tamanhosSelecionados = [];
        rows.forEach(row => {
            const sel = row.querySelector('select').value;
            if (sel) tamanhosSelecionados.push(sel);
        });

        const duplicados = tamanhosSelecionados.filter((t, i) => tamanhosSelecionados.indexOf(t) !== i);
        if (tamanhosSelecionados.length === 0 || duplicados.length > 0) {
            e.preventDefault();
            valido = false;
            if (erroTamanhos) {
                erroTamanhos.textContent = duplicados.length > 0
                    ? `Tamanho duplicado.`
                    : 'Adicione ao menos um tamanho com quantidade.';
                erroTamanhos.style.display = 'block';
            }
        } 

        // Validar imagens
        if (arquivos.length === 0) {
            e.preventDefault();
            valido = false;
            if (erroImagens) {
                erroImagens.textContent = 'Adicione ao menos uma imagem do produto.';
                erroImagens.style.display = 'block';
            }
        }

        if (valido) {
            const dt = new DataTransfer();
            arquivos.forEach(a => dt.items.add(a.file));
            inputFile.files = dt.files;
            inputFile.style.display = 'block';
        }
    });

})();
