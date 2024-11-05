using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductUpdateDescriptionDto
    {
        [Required]
        public int Id { get; set; }


        [Required]
        public required string Description { get; set; }
    }
}
