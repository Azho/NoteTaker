using NoteTakerApplication.Infrastructure;
using NoteTakerApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NoteTakerApplication.Controllers.Api
{
    public class ShorthandsController : ApiController
    {
        private IGenericRepository _repo;
        public ShorthandsController(IGenericRepository repo)
        {
            _repo = repo;
        }
        public List<Shorthand> shorthands;

        public ShorthandsController()
        {
            shorthands = new List<Shorthand>
            {
                new Shorthand { Id= 0, Title = "Default", Tags = "blank, default, plain, original", Author = this.User.Identity.Name, Published = false},
                new Shorthand { Id = 1, Title = "Title Two", Tags = "argh, um, bleh", Author = this.User.Identity.Name, Published = true},
                new Shorthand { Id = 2, Title = "Title Three", Tags = "halp, oh no, eep", Author = this.User.Identity.Name, Published = true}
            };
        }

        // GET: api/Shorthands
        public IEnumerable<Shorthand> Get()
        {

            var shorthands = (from m in _repo.Query<Shorthand>() where m.Author == "4zho" select m).ToList();
            if (shorthands != null)
            {
                //return View(shorthands.ToList());
            }
            return null;

            //return shorthands;

            //var shorthands = _repo.Query<Shorthand>().Where(m => m.Author == this.User.Identity.Name).ToList();
            //var shorthands = from m in _repo.Query<Shorthand>() select m;
            //(from m in _repo.Query<Shorthand>() where m.Author == this.User.Identity.Name select m).ToList();
            //if (shorthands != null)
            //{
            //    return shorthands.ToList();
            //}
            //return null; //Fix later 

        }

        // GET: api/Shorthands/5
        public string Get(int id)
        {
            return "value";
        }

        //Getting published shorthands
        [Route("api/shorthands/getPublishedShorthands")]
        public IEnumerable<Shorthand> getPublishedShorthands()
        {
            return shorthands.Where(s => s.Published == true);
        }


        // POST: api/Shorthands
        public void Post(Shorthand shorthand)
        {
            if (ModelState.IsValid && shorthand.Author == this.User.Identity.Name)
            {
                if (shorthand.Id > 0)
                {
                    var originalShorthand = shorthands.Find(x => x.Id == shorthand.Id);
                    originalShorthand.Title = shorthand.Title;
                    originalShorthand.Tags = shorthand.Tags;
                }
                else
                {
                    shorthands.Add(shorthand);
                }
                //Extra stuff goes here but the generic repo needs to be working.

            }
        }

        // PUT: api/Shorthands/5
        //public void Put(int id, [FromBody]string value)
        //{

        //}

        // DELETE: api/Shorthands/5
        public void Delete(int id)
        {
            var originalShorthand = shorthands.Find(x => x.Id == id);
            shorthands.Remove(originalShorthand);
            //shorthands.SaveChanges();
        }
    }
}
