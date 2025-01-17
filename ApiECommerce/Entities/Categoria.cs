﻿using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace ApiECormmerce.Entities
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [StringLength(200)]
        [Required]
        public string? UrlImagem { get; set; }

        [JsonIgnore]
        public ICollection<Produto>? Produtos{ get; set; }

    }
}
