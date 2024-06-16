using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.Models
{
    public class ProductDTO
    {
        [Required(ErrorMessage ="Name can not be empty")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Description can not be empty")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Price can not be empty")]
        public decimal? Price { get; set; }
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Category can not be empty")]
        public string? Category { get; set; }
        [Required(ErrorMessage = "Brand can not be empty")]
        public string? Brand { get; set; }
    }
}
