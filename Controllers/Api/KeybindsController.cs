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
    public class KeybindsController : ApiController
    {
         private IGenericRepository _repo;
        public KeybindsController(IGenericRepository repo)
        {
            _repo = repo;
        }
        public KeybindsController()
        {
            //if (_repo.Keybinds.Count() == 0)
            //{
            //    var keybinds = new List<Keybind>
            //    {
            //        new Keybind {},
            //        new Keybind {},
            //        new Keybind {},
            //        new Keybind {},
            //        new Keybind {}
            //    };
            //    keybinds.ForEach(k => _repo.Add(k));
            //    _repo.SaveChanges();
            //}
        }
        
        // GET: api/Keybinds
        public IEnumerable<Keybind> Get()
        {
            var keybinds = from k in _repo.Query<Keybind>() select k;
            return keybinds.ToList();
        }

        // GET: api/Keybinds/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Keybinds
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Keybinds/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Keybinds/5
        public void Delete(int id)
        {
        }
    }
}
