using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Project.Models
{
    public class Device
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Display(Name = "Device Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(10000000, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 50 characters")]
        [Display(Name = "Device Description")]
        public string? Description { get; set; }

        [Range(0, 1000000, ErrorMessage = "Price must be between 0 and 1,000,000")]
        [Display(Name = "Device Price")]
        public decimal? Price { get; set; }

        public string? imageURL { get; set; }

        [ForeignKey("categoryId")]
        [ValidateNever]
        public Category category { get; set; }

   
        public int categoryId { get; set; }
        
    }
}
