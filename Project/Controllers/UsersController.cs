using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Project.Context;
using Project.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Project.Controllers
{
    public class UsersController : Controller
    {

        applicationDbContext context = new applicationDbContext();



        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsersController(IWebHostEnvironment env)
        {
            _webHostEnvironment = env;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult getSginUpView()
        {
                var user = new User();
                ViewBag.userRole = "";
                return View("signUp", user);
            
        }
        public IActionResult saveUserData(User u)
        {
            ViewBag.userRole = "";
            if (ModelState.IsValid)
            {
                context.users.Add(u);
                context.SaveChanges();
            }
            else {
                ModelState.AddModelError(string.Empty,"Empty fields");

                return View("signUp");
            }
            return View("signIn");
        }
        private bool AuthenticateUser(string email, string password)
        {
            // TODO: Implement your logic to authenticate the user against the database
            // For example, retrieve the user from the database and compare passwords

            // For demonstration purposes, let's assume you have a method in your context
            // that can authenticate the user by email and password
            var authenticatedUserEmail =  context.users.FirstOrDefault(e=>e.Email== email && e.Password == password);

            if (authenticatedUserEmail != null)
            {
                return true;

            }
            return false;
        }
        public IActionResult getSignInPage()
        {
            ViewBag.userRole = "";

            return View("signIn");
        }

        [HttpPost]
        public async Task<IActionResult> signIn(User user)
        {
            applicationDbContext context = new applicationDbContext();
                // TODO: Authenticate user against the database
                // Example code (replace with actual authentication logic):
                bool isAuthenticated =  AuthenticateUser(user.Email, user.Password);

                if (isAuthenticated)
                {
                var u = context.users.FirstOrDefault(e => e.Email == user.Email);

                    if (u != null)
                    {
                        appData.CurrentUserId = u.Id;

                        appData.CurrentUserRole = u.roleName;
                    }
                    return RedirectToAction("Index", "Devices"); // Redirect to a protected page
                }
                else
                {
                ViewBag.userRole = "";

                ModelState.AddModelError("", "Invalid email or password");
                }
            

            return View(user); // Return to the sign-in view with errors
        }
        public IActionResult LogOutAction()
        {


            appData.CurrentUserId = -1;

            appData.CurrentUserRole = "";

            var user = new User();
            ViewBag.userRole ="";
            return View("signUp", user);
        }
    }

    
    
}
