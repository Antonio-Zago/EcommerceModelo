using Application.BackgroundJobs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceModeloMvc.Controllers;

public class ImportacoesController : Controller
{
    private readonly IJobStore _jobStore;

    public ImportacoesController(IJobStore jobStore)
    {
        _jobStore = jobStore;
    }

    /// <summary>Lista todos os jobs de importação.</summary>
    public IActionResult Index() => View(_jobStore.ObterTodos());

    /// <summary>Página de acompanhamento de um job específico (com polling JS).</summary>
    public IActionResult Detalhes(Guid jobId)
    {
        var job = _jobStore.Obter(jobId);
        if (job == null) return NotFound();
        return View(job);
    }

    /// <summary>Endpoint JSON consultado pelo polling do browser.</summary>
    [HttpGet]
    public IActionResult Status(Guid jobId)
    {
        var job = _jobStore.Obter(jobId);
        if (job == null) return NotFound();

        return Ok(new
        {
            status       = job.Status,
            processados  = job.Processados,
            total        = job.Total,
            importados   = job.ProdutosImportados,
            erros        = job.Erros,
            concluidoEm  = job.ConcluidoEm
        });
    }
}
