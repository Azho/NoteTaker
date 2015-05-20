using Microsoft.AspNet.Identity;
using NoteTakerApplication.Infrastructure;
using NoteTakerApplication.Models;
using NoteTakerApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace NoteTakerApplication.Controllers
{
    public class NotesController : Controller
    {

        private IGenericRepository _repo;
        public NotesController(IGenericRepository repo)
        {
            _repo = repo;
            //CurrentUser = (from m in _repo.Query<ApplicationUser>() where m.UserName == this.User.Identity.Name select m).FirstOrDefault();
            //     Find<ApplicationUser>(User.Identity.Name);
            // Solve bug in database for ApplicationUser

        }


        // GET: Notes
        [Authorize]
        public ActionResult Index()
        {
            //Thing .UserName = username;
            // There is 
            var userId = this.User.Identity.GetUserId();
            var notes = (from m in _repo.Query<User_Note>().Include(m => m.Note) where m.UserId == userId select m.Note).ToList();
            if (notes != null)
            {
                return View(notes.ToList());
            }
            return View();

        }

        // GET: Notes/Details/5
        //public ActionResult Details(int id)
        //{
        //    var note = _repo.Find<Note>(id);
        //    if (note != null)
        //    {
        //        return View(note);
        //    }
        //    return RedirectToAction("Index");
        //}

        // GET: Notes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        public ActionResult Create(Note note)
        {

            if (ModelState.IsValid)
            {
                note.Published = false;
                note.Author = this.User.Identity.Name;
                note.UserId = this.User.Identity.GetUserId();
                note.TimesSaved = 0;
                note.Report = 0;
                _repo.Add<Note>(note);
                _repo.SaveChanges();
                _repo.Add<User_Note>(new User_Note()
                {
                    NoteId = note.Id,
                    UserId = this.User.Identity.GetUserId()
                });
                _repo.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet );
            }
            return View();
        }

        // GET: Notes/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var note = _repo.Find<Note>(id);
            if (note != null)
            {
                return View(note);
            }
            return View();
        }

        // POST: Notes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Note note)
        {
            if (ModelState.IsValid)
            {
                var original = _repo.Find<Note>(id);
                original.Title = note.Title;
                original.Description = note.Description;
                original.Content = note.Content;
                original.Tags = note.Tags;
                original.Published = note.Published;
                
                _repo.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            } 
            return View();
        }

        //Do the same thing for delete.

        // GET: Notes/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            var note = _repo.Find<Note>(id);
            if (note != null)
            {
                return View(note);
            }
            return View();
        }

        // POST: Notes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Note note)
        {
            if (note.Author == this.User.Identity.Name)
            {
                _repo.Delete<Note>(id);
                _repo.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var userId = this.User.Identity.GetUserId();
                var target = _repo.Query<User_Note>().FirstOrDefault(a => a.NoteId == id && a.UserId == userId).Id;
                _repo.Delete<User_Note>(target);
                _repo.SaveChanges();
            }

           return (RedirectToAction("Index"));
        }

        //This may not be working yet.
        [HttpPost]
        [Authorize]
        public ActionResult Publish(int id)
        {//How to prevent just anyone from calling this
            var currentNote = _repo.Find<Note>(id);
            currentNote.Published = true;
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }

 

        [HttpGet]
        public ActionResult Save(int id)
        {
            var original = _repo.Find<Note>(id);
            
            _repo.Add<User_Note>(new User_Note
            {
                UserId = this.User.Identity.GetUserId(),
                NoteId = id
            });

            original.TimesSaved++;
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Report(int id)
        {
            var target = _repo.Find<User_Note>(new User_Note
            {
                UserId = this.User.Identity.GetUserId(),
                NoteId = id
                
            });
            var original = _repo.Find<Note>(id);
            original.Report++;
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}