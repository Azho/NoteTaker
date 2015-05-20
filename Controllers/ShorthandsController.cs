using Microsoft.AspNet.Identity;
using NoteTakerApplication.Infrastructure;
using NoteTakerApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteTakerApplication.Controllers
{
    public class ShorthandsController : Controller
    {
       private IGenericRepository _repo;
        public ShorthandsController(IGenericRepository repo)
        {
            _repo = repo;
        } 
        // GET: Shorthands
        public ActionResult Index()
        {
            var shorthands = (from m in _repo.Query<Shorthand>() where m.Author == this.User.Identity.Name select m).ToList();
            if (shorthands != null)
            {
                return View(shorthands.ToList());
            }
            return View();
        } 

        // GET: Shorthands/Create
       
        public ActionResult Create()
        {
            var test = this.User.Identity.Name;
            return View();
        }

        // POST: Shorthands/Create
        [HttpPost]
        public ActionResult Create(Shorthand shorthand)
        {

            if (ModelState.IsValid)
            {
                
                shorthand.Published = false;
                shorthand.Author = this.User.Identity.Name;
                shorthand.UserId = this.User.Identity.GetUserId();
                _repo.Add<Shorthand>(shorthand);
                _repo.SaveChanges();
                _repo.Add<User_Shorthand>(new User_Shorthand()
                {
                    ShorthandId = shorthand.Id,
                    UserId = this.User.Identity.GetUserId()
                });
                _repo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Shorthands/Edit/5
        public ActionResult Edit(int id)
        {
            var shorthand = _repo.Find<Shorthand>(id);
            if (shorthand != null)
            {
                return View(shorthand);
            }
            return View();
        }

        // POST: Shorthands/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Shorthand shorthand)
        {
            if (ModelState.IsValid)
            {
                var original = _repo.Find<Shorthand>(id);
                original.Title = shorthand.Title;
                original.Tags = shorthand.Tags;
                _repo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Shorthands/Delete/5
        public ActionResult Delete(int id)
        {
            var shorthand = _repo.Find<Shorthand>(id);
            if (shorthand != null)
            {
                return View(shorthand);
            }
            return View();
        }

        // POST: Shorthands/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Shorthand shorthand)
        {
            _repo.Delete<Shorthand>(id);
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Authorize]
        public ActionResult Publish(int id)
        {
            //I think I'm doing this in a really janky way. Pretty sure this is the caveman way. Seek second opinion.
            //How to prevent anyone from calling this?
            var currentShorthand = _repo.Find<Shorthand>(id);
            currentShorthand.Published = true;
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Unpublish(int id)
        {
            //Is this deleting it from the user's profile, too?
            //How to prevent anyone from calling this?

            var publishedShorthand = _repo.Find<Shorthand>(id);
            publishedShorthand.Published = false;
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }


       //            ShorthandId = shorthand.Id,
        //            UserId = this.User.Identity.GetUserId()
        //        });

        // POST: Shorthands/Delete/5
        [HttpPost]
        public ActionResult Save(int id)
        {
            var original = _repo.Find<Shorthand>(id);
            _repo.Add<User_Shorthand>(new User_Shorthand
            {
                UserId = this.User.Identity.GetUserId(),
                ShorthandId = id
            });
            original.TimesSaved++;
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
