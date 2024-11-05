using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Domain.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string ImgUri { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; }

    }
}
