using NoteTakerApplication.Infrastructure;
using NoteTakerApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteTakerApplication.Controllers
{
    public class KeybindsController : Controller
    {
        private IGenericRepository _repo;
        public KeybindsController(IGenericRepository repo)
        {
            _repo = repo;
        }

        // GET: Keybinds
        public ActionResult Index()
        {
            //var keybinds = from k in _db.Keybinds select k;
            //Should list out the keybinding. Point to shorthand view.
            //Return to view Shorthand Create
            //var user = this.User.Identity.
            var keybinds = from k in _repo.Query<Keybind>() select k;
            return View(keybinds.ToList());
           
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Keybinds/Create
        [HttpPost]
        public ActionResult Create(Keybind keybind)
        {
            if (ModelState.IsValid)
            {
                _repo.Add<Keybind>(keybind);
                _repo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Keybinds/Edit/5
        public ActionResult Edit(int id)
        {
            var keybind = _repo.Find<Keybind>(id);
            if (keybind != null)
            {
                return View(keybind);
            }
            return View();
        }

        // POST: Keybinds/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Keybind keybind)
        {
            if (ModelState.IsValid)
            {
                var original = _repo.Find<Keybind>(keybind.Id);
                original.Key = keybind.Key;
                original.Modifier = keybind.Modifier;
                original.Description = keybind.Description;
                _repo.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }

        // GET: Keybinds/Delete/5
        public ActionResult Delete(int id)
        {
            var keybind = _repo.Find<Keybind>(id);
            if (keybind != null)
            {
                return View(keybind);
            }
            return View();
        }

        // POST: Keybinds/Delete/5
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult DeleteReally(int id)
        {
            _repo.Delete<Keybind>(id);
            _repo.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}