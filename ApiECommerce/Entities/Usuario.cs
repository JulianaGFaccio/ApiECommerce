using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;

namespace ApiECormmerce.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(150)]
        public string? Email { get; set; }

        [Required]
        [StringLength(100)]
        public string? Senha { get; set; }

        [StringLength(100)]
        public string? UrlImagem { get; set; }

        [StringLength(100)]
        public string? Telefone { get; set; }

        [NotMapped]
        public IFormFile? Imagem { get; set; }

        public ICollection<Pedido>? Pedidos { get; set; }


    }
}
