using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Models;


namespace Project.Controllers
{
    public class DevicesController : Controller
    {
        applicationDbContext context = new applicationDbContext();

     
      
        private readonly IWebHostEnvironment _webHostEnvironment ;
        public DevicesController (IWebHostEnvironment env)
        {
            ViewBag.userRole = appData.CurrentUserRole;

            _webHostEnvironment = env;
        }
        public IActionResult Index()
        {
            
            ViewBag.userRole = appData.CurrentUserRole ;
            return View("Index",context.devices.ToList());
        }

        public IActionResult getDeviceForm()
        {
            ViewBag.userRole = appData.CurrentUserRole;

            return View("deviceForm");
        }

        [HttpPost]
        public IActionResult AddNewDevice(Device d ,IFormFile? imageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFormFile != null)
                {
                    string imgExtension = Path.GetExtension(imageFormFile.FileName); // .png
                    Guid imgGuid = Guid.NewGuid(); // xm789-f07li-624yn-uvx98
                    string imgName = imgGuid + imgExtension; // xm789-f07li-624yn-uvx98.png
                    string imgUrl = "\\images\\" + imgName; //  \images\xm789-f07li-624yn-uvx98.png
                    d.imageURL = imgUrl;

                    string imgPath = _webHostEnvironment.WebRootPath + imgUrl;
                 
                    FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                    imageFormFile.CopyTo(imgStream);
                    imgStream.Dispose();
                }
                else
                {
                    d.imageURL = "\\images\\No_Image.png";
                }
            }

            context.devices.Add(d);
            context.SaveChanges();
            ViewBag.userRole = appData.CurrentUserRole;

            return RedirectToAction("Index");
        }
        public IActionResult getDevicedetails(int id)
        {
            ViewBag.userRole = appData.CurrentUserRole;

            ViewBag.userRole = appData.CurrentUserRole;

            Device d =   context.devices.First(e=> e.Id == id);
            return View("Details",d);
        }
        public IActionResult getDeviceByName(string searchQuery)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                string normalizedQuery = searchQuery?.Replace(" ", "").ToLower();
                var matchesDevces = context.devices
                    .Where(d => d.Name.Replace(" ", "").ToLower().Contains(normalizedQuery) || normalizedQuery.Contains(d.Name.Replace(" ", "").ToLower()))

                    .ToList();
                ViewBag.userRole = appData.CurrentUserRole;

                return View("Index", matchesDevces);
            }
            else {
                ViewBag.userRole = appData.CurrentUserRole;

                return View("Index",context.devices.ToList());
            }       
        }
        
        public IActionResult getCategoryValue(int id)
        {
           
                var dev = context.devices
                    .Where(d => d.categoryId == id)
                    .ToList();
            ViewBag.userRole = appData.CurrentUserRole;

            return View("Index", dev); // Assuming you have an "Index" view to display the filtered devices
            
        

        }

        public IActionResult deleteDevice (int id)
        {

            var dev = context.devices.Find(id);
            if (dev != null)
                context.devices.Remove(dev);



            string imgPath = _webHostEnvironment.WebRootPath + dev.imageURL;
                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }



            

         

            context.SaveChanges();
            ViewBag.userRole = appData.CurrentUserRole;

            return View("Index", context.devices.ToList()); // Assuming you have an "Index" view to display the filtered devices



        }


        
  public IActionResult editDevice(Device device, IFormFile? imageFormFile)
        {
            if (imageFormFile != null)
            {
                string imgExtension = Path.GetExtension(imageFormFile.FileName); // .png
                Guid imgGuid = Guid.NewGuid(); // xm789-f07li-624yn-uvx98
                string imgName = imgGuid + imgExtension; // xm789-f07li-624yn-uvx98.png
                string imgUrl = "\\images\\" + imgName; //  \images\xm789-f07li-624yn-uvx98.png
                device.imageURL = imgUrl;

                string imgPath = _webHostEnvironment.WebRootPath + imgUrl;

                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                imageFormFile.CopyTo(imgStream);
                imgStream.Dispose();
            }
            Device? existingDevice = context.devices.Find(device.Id);
            if (existingDevice != null) { 
                existingDevice.Id= device.Id;
                existingDevice.Price = device.Price;
                existingDevice.imageURL = device.imageURL;
                existingDevice.Description = device.Description;
                existingDevice.Name = device.Name;

                context.devices.Update(existingDevice);
            }
            context.SaveChanges();
            return View("Details", existingDevice);

        }

        public IActionResult getUpdateFormDataView(int id)
        {
            var device = context.devices.First(dev => dev.Id == id);
            return View("editDeviceView", device);
        }

      
        
    }
}
