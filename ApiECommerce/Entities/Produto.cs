using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiECormmerce.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(200)]
        public string? Detalhe { get; set; }

        [Required]
        [StringLength(200)]
        public string? UrlImagem { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Preco { get; set; }

        public bool Popular { get; set; }
        public bool MaisVendido { get; set; }
        public int EmEstoque { get; set; }
        public bool Disponivel { get; set; }
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public ICollection<DetalhePedido>? DetalhesPedido { get; set; }

        [JsonIgnore]
        public ICollection<ItemCarrinhoCompra>? ItensCarrinhoCompras { get; set; }

    }
}
