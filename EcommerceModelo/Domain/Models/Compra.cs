using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models;

public class Compra
{
    [Column("id")]
    public int Id { get; set; }

    [Column("usuario")]
    public int UsuarioId { get; set; }

    [Column("status")]
    public string Status { get; set; } = "confirmado";

    [Column("forma_pagamento")]
    public string FormaPagamento { get; set; } = string.Empty;

    public Endereco Endereco { get; set; } = null!;

    [Column("total")]
    public decimal Total { get; set; }

    [Column("criado_em")]
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    [Column("entrega_prevista")]
    public DateOnly EntregaPrevista { get; set; }

    public ICollection<CompraItem> Itens { get; set; } = new List<CompraItem>();
}
