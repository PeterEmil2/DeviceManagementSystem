using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(100000, MinimumLength = 2, ErrorMessage = "Description must be between 2 and 100 characters")]
        [Display(Name = "Description Name")]
        public string? Description { get; set; }


        public List<Device> devices { get; set; }
    }
}
