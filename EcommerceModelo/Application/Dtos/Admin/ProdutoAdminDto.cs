namespace Application.Dtos.Admin;

public class ProdutoAdminDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public string Genero { get; set; } = string.Empty;
    public bool EhInfantil { get; set; }
    public bool Arquivado { get; set; }
    public int EstoqueTotal { get; set; }
    public string ImagemPrincipalUrl { get; set; } = string.Empty;
}
