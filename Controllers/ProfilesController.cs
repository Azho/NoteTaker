using NoteTakerApplication.Infrastructure;
using NoteTakerApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteTakerApplication.Controllers
{
    public class ProfilesController : Controller
    {
        private IGenericRepository _repo;
        public ProfilesController(IGenericRepository repo)
        {
            _repo = repo;
        }
        private ApplicationUser GetCurrentUser()
        {
            return (from p in _repo.Query<ApplicationUser>() where p.UserName == this.User.Identity.Name select p).FirstOrDefault();
        }

        // GET: Users
        public ActionResult Index()
        {
            var profiles = from m in _repo.Query<ApplicationUser>() select m;
            return View(profiles.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string username)
        {
            var profile = this.GetCurrentUser();
            if (profile != null)
            {
                return View(profile);
            }
            return RedirectToAction("Index");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string username)
        {
            var profile = this.GetCurrentUser();
            if (profile != null)
            {
                return View(profile);
            }
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplicationUser profile)
        {
            if (ModelState.IsValid)
            {
                var original = this.GetCurrentUser();
                original.FirstName = profile.FirstName;
                original.LastName = profile.LastName;
                original.Email = profile.Email;
                original.About = profile.About;
                _repo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string username)
        {
            var profile = this.GetCurrentUser();
            if (profile != null)
            {
                return View(profile);
            }
            return View();
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(string username, ApplicationUser profile)
        {
            var me = this.GetCurrentUser();
            _repo.Delete<ApplicationUser>(me);
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}