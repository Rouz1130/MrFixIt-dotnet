using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrFixIt.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MrFixIt.Controllers
{
    public class JobsController : Controller
    {
        // giving instanitaing the definiton for context for the database.
        private MrFixItContext db = new MrFixItContext();

        // GET: /<controller>/
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
            // if else statment if user is is correct/autheticated it will return to a a list the job assoiated with he user.
            return View(db.Jobs.Include(i => i.Worker).ToList());
            }else
            {
                return RedirectToAction("Public");
            }
        }

        public IActionResult Public()
        {
            return View(db.Jobs.Include(i => i.Worker).ToList());
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Job job)
        {
            // the public IAction Result create (Job job)this post for both the create method allows to create a job
            // the HttpPost defines a job and allows us to add and save it too the database.
            db.Jobs.Add(job);
            //adds to database
            db.SaveChanges();
            //saves to database
            return RedirectToAction("Index");
        }

        // this is a route to the claim page and to list a job a user wants.
        public IActionResult Claim(int id)
        {
            var thisItem = db.Jobs.FirstOrDefault(items => items.JobId == id);
            return View(thisItem);
        }
        // corrsponding post action  for claim method. seems might have to expand on the code down the line.
        [HttpPost]
        public IActionResult Claim(Job job)
        {
            job.Worker = db.Workers.FirstOrDefault(i => i.UserName == User.Identity.Name);
            db.Entry(job).State = EntityState.Modified; //  allows to update database for the job that was was claimed for the user. 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
