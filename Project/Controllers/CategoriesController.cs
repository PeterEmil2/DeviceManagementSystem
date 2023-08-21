using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Context;
using Project.Models;

namespace Project.Controllers
{
    
    public class CategoriesController : Controller
    {
        applicationDbContext context = new applicationDbContext();
        public IActionResult Index()
        {
           
            return View("categoryForm");
        }
       

        public IActionResult AddNewCategory(Category c)
        {
            context.categories.Add(c);
            context.SaveChanges();

            return View("categoryForm");
        }
    }
}
