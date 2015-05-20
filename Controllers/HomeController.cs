using NoteTakerApplication.Infrastructure;
using NoteTakerApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteTakerApplication.Controllers
{
    public class HomeController : Controller
    {
         private IGenericRepository _repo;
        public HomeController(IGenericRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            var notes = (from m in _repo.Query<Note>() where m.Author == this.User.Identity.Name select m).ToList();
           
            return View(notes.ToList()); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}