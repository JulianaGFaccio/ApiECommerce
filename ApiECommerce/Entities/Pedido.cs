using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace ApiECormmerce.Entities
{
    public class Pedido
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string? Endereco { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Valor { get; set; }
        public DateTime dataPedido { get; set; }
        public int UsuarioId { get; set; }
        public ICollection<DetalhePedido>? DetalhesPedidos { get; set; }

    }
}
