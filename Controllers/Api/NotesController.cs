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
    public class NotesController : ApiController
    {
        private IGenericRepository _repo;
        public NotesController(IGenericRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Notes
        public IEnumerable<Note> Get()
        {

            var notes = from m in _repo.Query<Note>() where m.Username == this.User.Identity.Name select m;
            //where m.UserName == this.User.Identity.Name

            return notes;



        }

        // GET: api/Notes/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Notes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Notes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Notes/5
        public void Delete(int id)
        {
        }
    }
}
