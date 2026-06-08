using Application.Dtos.Importacao;
using Application.Interfaces;
using ClosedXML.Excel;
using Domain.Models;
using EcommerceModeloMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers;

public class ProdutosController : Controller
{
    private readonly IProdutoService _produtoService;
    private readonly ICategoriaService _categoriaService;
    private readonly IOpcaoTamanhoService _opcaoTamanhoService;
    private readonly IImportacaoProdutosService _importacaoProdutosService;
    private readonly IWebHostEnvironment _env;

    private static readonly string[] _extensoesPermitidas = [".jpg", ".jpeg", ".png", ".webp"];
    private const long _tamanhoMaximoPorArquivo = 5 * 1024 * 1024;

    public ProdutosController(
        IProdutoService produtoService,
        ICategoriaService categoriaService,
        IOpcaoTamanhoService opcaoTamanhoService,
        IImportacaoProdutosService importacaoProdutosService,
        IWebHostEnvironment env)
    {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
        _opcaoTamanhoService = opcaoTamanhoService;
        _importacaoProdutosService = importacaoProdutosService;
        _env = env;
    }

    public async Task<IActionResult> Cadastrar()
    {
        await PopularViewBagAsync();
        return View(new CadastroProdutoViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(CadastroProdutoViewModel viewModel)
    {
        if (viewModel.Tamanhos == null || viewModel.Tamanhos.Count == 0)
            ModelState.AddModelError("Tamanhos", "Adicione ao menos um tamanho com quantidade.");
        else if (viewModel.Tamanhos.GroupBy(t => t.TamanhoId).Any(g => g.Count() > 1))
            ModelState.AddModelError("Tamanhos", "Não é permitido cadastrar o mesmo tamanho mais de uma vez.");

        if (viewModel.Imagens == null || viewModel.Imagens.Count == 0)
            ModelState.AddModelError("Imagens", "Adicione ao menos uma imagem do produto.");

        if (viewModel.Imagens != null)
        {
            foreach (var arquivo in viewModel.Imagens)
            {
                var ext = Path.GetExtension(arquivo.FileName).ToLowerInvariant();
                if (!_extensoesPermitidas.Contains(ext))
                    ModelState.AddModelError("Imagens", $"O arquivo \"{arquivo.FileName}\" possui formato não permitido. Use JPG, PNG ou WebP.");
                else if (arquivo.Length > _tamanhoMaximoPorArquivo)
                    ModelState.AddModelError("Imagens", $"O arquivo \"{arquivo.FileName}\" excede o tamanho máximo de 5 MB.");
            }
        }

        if (!ModelState.IsValid)
        {
            await PopularViewBagAsync();
            return View(viewModel);
        }

        try
        {
            var produto = new Produto
            {
                Nome = viewModel.Nome,
                Preco = viewModel.Preco,
                Descricao = viewModel.Descricao,
                CategoriaId = viewModel.CategoriaId
            };

            var estoques = viewModel.Tamanhos!
                .Select(t => (t.TamanhoId, t.QtdEstoque))
                .ToList();

            var imagens = viewModel.Imagens!
                .Select(f => (f.OpenReadStream(), f.FileName))
                .ToList();

            var pastaFisica = Path.Combine(_env.WebRootPath, "images", "produtos");

            await _produtoService.CadastrarComEstoqueAsync(produto, estoques, imagens, viewModel.ImagemPrincipalIndex, pastaFisica);

            TempData["Sucesso"] = $"Produto \"{viewModel.Nome}\" cadastrado com sucesso.";
            return RedirectToAction(nameof(Cadastrar));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Erro ao cadastrar produto: {ex.Message}");
            await PopularViewBagAsync();
            return View(viewModel);
        }
    }

    public IActionResult ImportarViaExcel()
    {
        ResultadoImportacaoDto? resultado = null;
        if (TempData["ResultadoImportacao"] is string json)
            resultado = System.Text.Json.JsonSerializer.Deserialize<ResultadoImportacaoDto>(json);

        ViewBag.Resultado = resultado;
        return View(new ImportarProdutosViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ImportarViaExcel(ImportarProdutosViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        var ext = Path.GetExtension(viewModel.Arquivo!.FileName).ToLowerInvariant();
        if (ext != ".xlsx")
        {
            ModelState.AddModelError(string.Empty, "Apenas arquivos .xlsx são aceitos.");
            return View(viewModel);
        }

        try
        {
            var linhas = ExtrairLinhasDoExcel(viewModel.Arquivo!);

            if (linhas.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "O arquivo não contém dados.");
                return View(viewModel);
            }

            var resultado = await _importacaoProdutosService.ImportarAsync(linhas, _env.WebRootPath);
            TempData["ResultadoImportacao"] = System.Text.Json.JsonSerializer.Serialize(resultado);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Erro ao processar o arquivo: {ex.Message}");
            return View(viewModel);
        }

        return RedirectToAction(nameof(ImportarViaExcel));
    }

    public async Task<IActionResult> DownloadModeloExcel()
    {
        var categorias = (await _categoriaService.ObterTodosAsync()).ToList();
        var opcoesTamanho = (await _opcaoTamanhoService.ObterTodosComTipoAsync()).ToList();

        using var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add("Produtos");

        var headers = new[] { "Nome", "Preco", "Descricao", "Categoria", "TipoTamanho", "OpcaoTamanho", "QtdEstoque", "Foto1", "Foto2", "Foto3" };
        for (int i = 0; i < headers.Length; i++)
        {
            var cell = ws.Cell(1, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromArgb(13, 110, 253);
            cell.Style.Font.FontColor = XLColor.White;
        }

        var categoriaNome = categorias.FirstOrDefault()?.Nome ?? "Camisetas";
        var primeiraOpcao = opcoesTamanho.FirstOrDefault();
        var segundaOpcao = opcoesTamanho.Skip(1).FirstOrDefault();
        var tipoEx1 = primeiraOpcao?.Tipo?.Nome ?? "Letras";
        var opcaoEx1 = primeiraOpcao?.Descricao ?? "M";
        var tipoEx2 = segundaOpcao?.Tipo?.Nome ?? "Letras";
        var opcaoEx2 = segundaOpcao?.Descricao ?? "G";

        ws.Cell(2, 1).Value = "Camiseta Branca Básica";
        ws.Cell(2, 2).Value = 59.90;
        ws.Cell(2, 3).Value = "Camiseta básica de algodão, corte clássico.";
        ws.Cell(2, 4).Value = categoriaNome;
        ws.Cell(2, 5).Value = tipoEx1;
        ws.Cell(2, 6).Value = opcaoEx1;
        ws.Cell(2, 7).Value = 10;
        ws.Cell(2, 8).Value = "/images/produtos/camiseta-branca.png";
        ws.Cell(2, 9).Value = "/images/produtos/camiseta-preta.png";

        ws.Cell(3, 1).Value = "Camiseta Branca Básica";
        ws.Cell(3, 5).Value = tipoEx2;
        ws.Cell(3, 6).Value = opcaoEx2;
        ws.Cell(3, 7).Value = 5;

        ws.Cell(4, 1).Value = "Calça Jeans Slim";
        ws.Cell(4, 2).Value = 149.90;
        ws.Cell(4, 3).Value = "Calça jeans slim fit, lavagem escura.";
        ws.Cell(4, 4).Value = categoriaNome;
        ws.Cell(4, 5).Value = tipoEx1;
        ws.Cell(4, 6).Value = opcaoEx1;
        ws.Cell(4, 7).Value = 8;
        ws.Cell(4, 8).Value = "/images/produtos/calca-jeans-slim.png";

        var wsRef = workbook.Worksheets.Add("Referência");
        wsRef.Cell(1, 1).Value = "Categorias disponíveis";
        wsRef.Cell(1, 1).Style.Font.Bold = true;
        for (int i = 0; i < categorias.Count; i++)
            wsRef.Cell(i + 2, 1).Value = categorias[i].Nome;

        wsRef.Cell(1, 3).Value = "Tipos de Tamanho";
        wsRef.Cell(1, 4).Value = "Opções de Tamanho";
        wsRef.Cell(1, 3).Style.Font.Bold = true;
        wsRef.Cell(1, 4).Style.Font.Bold = true;
        for (int i = 0; i < opcoesTamanho.Count; i++)
        {
            wsRef.Cell(i + 2, 3).Value = opcoesTamanho[i].Tipo?.Nome ?? "";
            wsRef.Cell(i + 2, 4).Value = opcoesTamanho[i].Descricao;
        }

        ws.Columns().AdjustToContents();
        wsRef.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "modelo_importacao_produtos.xlsx");
    }

    private static List<LinhaImportacaoDto> ExtrairLinhasDoExcel(IFormFile arquivo)
    {
        using var workbook = new XLWorkbook(arquivo.OpenReadStream());
        var ws = workbook.Worksheet(1);
        var rows = ws.RangeUsed()?.RowsUsed().Skip(1).ToList();

        if (rows == null || rows.Count == 0)
            return [];

        return rows
            .Select((row, idx) =>
            {
                var fotos = new[] { row.Cell(8).GetString().Trim(), row.Cell(9).GetString().Trim(), row.Cell(10).GetString().Trim() }
                    .Where(f => !string.IsNullOrWhiteSpace(f))
                    .ToList();

                return new LinhaImportacaoDto
                {
                    NumeroLinha = idx + 2,
                    Nome = row.Cell(1).GetString().Trim(),
                    Preco = row.Cell(2).GetString().Trim(),
                    Descricao = row.Cell(3).GetString().Trim(),
                    Categoria = row.Cell(4).GetString().Trim(),
                    TipoTamanho = row.Cell(5).GetString().Trim(),
                    OpcaoTamanho = row.Cell(6).GetString().Trim(),
                    QtdEstoque = row.Cell(7).GetString().Trim(),
                    UrlsImagens = fotos,
                };
            })
            .Where(l => !string.IsNullOrWhiteSpace(l.Nome))
            .ToList();
    }

    private async Task PopularViewBagAsync()
    {
        ViewBag.Categorias = await _categoriaService.ObterTodosAsync();
        ViewBag.OpcoesTamanho = await _opcaoTamanhoService.ObterTodosComTipoAsync();
    }
}
