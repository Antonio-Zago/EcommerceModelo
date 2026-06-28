using Application.Dtos.Importacao;
using Application.Interfaces;
using Domain.Enums;
using Domain.Models;

namespace Application.Services;

public class ImportacaoProdutosService : IImportacaoProdutosService
{
    private static readonly string[] _extensoesPermitidas = [".jpg", ".jpeg", ".png", ".webp"];

    private readonly IProdutoService _produtoService;
    private readonly ICategoriaService _categoriaService;
    private readonly IOpcaoTamanhoService _opcaoTamanhoService;

    public ImportacaoProdutosService(
        IProdutoService produtoService,
        ICategoriaService categoriaService,
        IOpcaoTamanhoService opcaoTamanhoService)
    {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
        _opcaoTamanhoService = opcaoTamanhoService;
    }

    public async Task<ResultadoImportacaoDto> ImportarAsync(
        IEnumerable<LinhaImportacaoDto> linhas,
        string webRootPath,
        IProgress<(int processados, int total)>? progresso = null)
    {
        var resultado = new ResultadoImportacaoDto();

        var categorias = (await _categoriaService.ObterTodosAsync()).ToList();
        var opcoesTamanho = (await _opcaoTamanhoService.ObterTodosComTipoAsync()).ToList();
        var pastaFisica = Path.Combine(webRootPath, "images", "produtos");

        var grupos = linhas
            .Where(l => !string.IsNullOrWhiteSpace(l.Nome))
            .GroupBy(l => l.Nome)
            .ToList();

        var total = grupos.Count;
        var processados = 0;
        progresso?.Report((0, total));

        foreach (var grupo in grupos)
        {
            var nomeProduto = grupo.Key;
            var primeiraLinha = grupo.First();

            if (!TryParsePreco(primeiraLinha.Preco, out var preco))
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\" (linha {primeiraLinha.NumeroLinha}): preço inválido \"{primeiraLinha.Preco}\".");
                continue;
            }

            if (string.IsNullOrWhiteSpace(primeiraLinha.Descricao))
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\" (linha {primeiraLinha.NumeroLinha}): descrição em branco.");
                continue;
            }

            if (!TryParseGenero(primeiraLinha.Genero, out var genero))
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\" (linha {primeiraLinha.NumeroLinha}): gênero \"{primeiraLinha.Genero}\" inválido. Use \"Masculino\" ou \"Feminino\".");
                continue;
            }

            var ehInfantil = ParseEhInfantil(primeiraLinha.EhInfantil);

            var categoria = categorias.FirstOrDefault(c =>
                c.Nome.Equals(primeiraLinha.Categoria, StringComparison.OrdinalIgnoreCase));
            if (categoria == null)
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\" (linha {primeiraLinha.NumeroLinha}): categoria \"{primeiraLinha.Categoria}\" não encontrada.");
                continue;
            }

            if (!TryResolverEstoques(grupo, nomeProduto, opcoesTamanho, resultado, out var estoques))
                continue;

            var urlsImagens = grupo.First().UrlsImagens
                .Where(u => !string.IsNullOrWhiteSpace(u))
                .ToList();

            if (urlsImagens.Count == 0)
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\": nenhuma imagem informada.");
                continue;
            }

            if (!TryAbrirStreamsImagens(urlsImagens, nomeProduto, webRootPath, resultado, out var streams))
                continue;

            try
            {
                var produto = new Produto
                {
                    Nome = nomeProduto,
                    Preco = preco,
                    Descricao = primeiraLinha.Descricao,
                    CategoriaId = categoria.Id,
                    Genero = genero,
                    EhInfantil = ehInfantil
                };

                await _produtoService.CadastrarComEstoqueAsync(produto, estoques, streams, 0, pastaFisica);
                resultado.ProdutosImportados.Add(nomeProduto);
            }
            finally
            {
                foreach (var (stream, _) in streams)
                    stream.Dispose();
            }

            processados++;
            progresso?.Report((processados, total));
        }

        resultado.ProdutosCadastrados = resultado.ProdutosImportados.Count;
        return resultado;
    }

    private static bool TryParseGenero(string valor, out Genero genero)
    {
        genero = default;
        return valor.Equals("Masculino", StringComparison.OrdinalIgnoreCase)
            ? (genero = Genero.Masculino) == Genero.Masculino
            : valor.Equals("Feminino", StringComparison.OrdinalIgnoreCase)
                && (genero = Genero.Feminino) == Genero.Feminino;
    }

    private static bool ParseEhInfantil(string valor)
        => valor.Equals("Sim", StringComparison.OrdinalIgnoreCase) ||
           valor.Equals("S", StringComparison.OrdinalIgnoreCase) ||
           valor.Equals("true", StringComparison.OrdinalIgnoreCase);

    private static bool TryParsePreco(string valor, out decimal preco)
        => decimal.TryParse(
            valor.Replace(',', '.'),
            System.Globalization.NumberStyles.Any,
            System.Globalization.CultureInfo.InvariantCulture,
            out preco) && preco > 0;

    private static bool TryResolverEstoques(
        IGrouping<string, LinhaImportacaoDto> grupo,
        string nomeProduto,
        List<Domain.Models.OpcaoTamanho> opcoesTamanho,
        ResultadoImportacaoDto resultado,
        out List<(int tamanhoId, int quantidade)> estoques)
    {
        estoques = [];

        foreach (var linha in grupo)
        {
            var opcao = opcoesTamanho.FirstOrDefault(o =>
                o.Descricao.Equals(linha.OpcaoTamanho, StringComparison.OrdinalIgnoreCase) &&
                (string.IsNullOrWhiteSpace(linha.TipoTamanho) ||
                 o.Tipo?.Nome.Equals(linha.TipoTamanho, StringComparison.OrdinalIgnoreCase) == true));

            if (opcao == null)
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\" (linha {linha.NumeroLinha}): tamanho \"{linha.TipoTamanho} - {linha.OpcaoTamanho}\" não encontrado.");
                return false;
            }

            if (!int.TryParse(linha.QtdEstoque, out var qtd) || qtd < 0)
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\" (linha {linha.NumeroLinha}): quantidade \"{linha.QtdEstoque}\" inválida.");
                return false;
            }

            if (estoques.Any(e => e.tamanhoId == opcao.Id))
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\": tamanho \"{linha.OpcaoTamanho}\" duplicado.");
                return false;
            }

            estoques.Add((opcao.Id, qtd));
        }

        if (estoques.Count == 0)
        {
            resultado.Erros.Add($"Produto \"{nomeProduto}\": nenhum tamanho/estoque informado.");
            return false;
        }

        return true;
    }

    private static bool TryAbrirStreamsImagens(
        IEnumerable<string> urls,
        string nomeProduto,
        string webRootPath,
        ResultadoImportacaoDto resultado,
        out List<(Stream stream, string nomeOriginal)> streams)
    {
        streams = [];

        foreach (var url in urls)
        {
            var caminhoRelativo = url.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            var caminhoFisico = Path.Combine(webRootPath, caminhoRelativo);

            if (!File.Exists(caminhoFisico))
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\": imagem não encontrada em \"{url}\".");
                foreach (var (s, _) in streams) s.Dispose();
                return false;
            }

            var ext = Path.GetExtension(caminhoFisico).ToLowerInvariant();
            if (!_extensoesPermitidas.Contains(ext))
            {
                resultado.Erros.Add($"Produto \"{nomeProduto}\": formato de imagem não permitido em \"{url}\".");
                foreach (var (s, _) in streams) s.Dispose();
                return false;
            }

            streams.Add((File.OpenRead(caminhoFisico), Path.GetFileName(caminhoFisico)));
        }

        return true;
    }
}
