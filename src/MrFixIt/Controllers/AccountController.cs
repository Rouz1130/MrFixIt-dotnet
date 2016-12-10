using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrFixIt.Models;
using Microsoft.AspNetCore.Identity;
using MrFixIt.ViewModels;

//once the register views and the viewmodel have been created we add two more usings that were currently here 
//Using System.Threading.Tasks
//Using BasicAuthentication.ViewModels.this is so we can use asynchroous methods and to obvioulsy access our RegisterViewModel.
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MrFixIt.Controllers
{
    public class AccountController : Controller
    {
        private MrFixItContext db = new MrFixItContext();


        //Basic User Account Info here...
        private readonly MrFixItContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // dependency injections in our constructor to configure these services =<ApplicationUser> is what is being injected.
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, MrFixItContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        // the commented out IACTION above should jsut be deleted as know we have have continued with it bleow to add authentication for the user.
        // 
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var thisWorker = db.Workers.FirstOrDefault(item => item.UserName == User.Identity.Name);
                return View(thisWorker);
            }
            // If statment above for if user has been authenticated.
            else
            // else statment for if user is not authenticated route back to view *Index.
            {
                return View();
            }
        }


        // First IAction Register bleow is to route it back to its page
        //Get Action will return the Register View file
        public IActionResult Register()
        {
            return View();
        }

        // it grabs the data from the Account/Register file and the mehtod bleow uses it create a user
        //also routes it back to the account folder/register file in Views folder
        //Post Action takes in the infromation submitted from the form in the register view and create a new user.

        [HttpPost]
        //post method is asynchronous so the task/ActionResult will be returned. 
        public async Task<IActionResult> Register(RegisterViewModel model)// the argument for registar is the model.
        {
            // we create ApplicationUser named user that uses the *Email, from the form as UserName.
            var user = new ApplicationUser { UserName = model.Email };
                                                      //CreateAsync takes two arguements a user(which is the applicationuser) with information , and second argument is a password which wiill be hashed when the user is added too the database for security purpose.
                                                      // we create this so we can go to the database and create a user. 
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                //if succedded the controller redirects to the index page.
            {
                //If else statment. If result succeded meaning if user is authenticatd or created it will rout back to the index page for that folder.
                return RedirectToAction("Index");
            }
            else
            {
                //if user was not created then it returns back the view in this case register/view
                return View();
            }
        }

        // comments for registar apply to Login as well.   
        public IActionResult Login()
        {
            return View();
        }
        //Post information for a user who has already signed up another if else statment if user has signed in succesfully else redirect back to a login page.

        [HttpPost]
        //public *async is an operations that lets the other code run while the method is wiating for a return
        // async method can return void or a task. Task is void by default. In case below a Task<IACtionResult> returns an ActionResult a task can also reutrn an int or a string as well.
        public async Task<IActionResult> Login(LoginViewModel model)
        {
                                                                //signInManager's asynchronous  passwordSignINAsync method is so u can sign in user with there credintials.                      
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);// 4 arguments in this parameters username password, the lockoutonFailure and isPersistent is setting default values for password and email.
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        // HttpPost i beleive should be the correct verison but not sure have not encountered HttpGet, only HttpPost but may be fine cause appliation works fine.
        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
