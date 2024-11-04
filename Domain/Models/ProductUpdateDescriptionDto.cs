using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ProductUpdateDescriptionDto
    {
        [Required]
        public int Id { get; set; }


        [Required]
        public string Description { get; set; }
    }
}
