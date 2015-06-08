using NoteTakerApplication.Infrastructure;
using NoteTakerApplication.Models;
using NoteTakerApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteTakerApplication.Controllers
{
    public class SearchController : Controller
    {
        private IGenericRepository _repo;
        public SearchController(IGenericRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel search)
        {
            //Case insensitive querying of published Shorthands based on filter type
            List<SearchDetailViewModel> searchReturn = new List<SearchDetailViewModel>();

            if (search.Type == "Title")
            {
                searchReturn = (from m in _repo.Query<Note>() where m.Title.ToLower().Contains(search.SearchPhrase.ToLower()) && m.Published == true orderby m.TimesSaved descending select new SearchDetailViewModel { resultId = m.Id, resultTitle = m.Title, Author = m.Author, Description = m.Description, TimesSaved = m.TimesSaved }).ToList();
            }
            else if (search.Type == "Tags")
            {
                searchReturn = (from m in _repo.Query<Note>() where m.Tags.ToLower().Contains(search.SearchPhrase.ToLower()) && m.Published == true orderby m.TimesSaved descending select new SearchDetailViewModel { resultId = m.Id, resultTitle = m.Title, Author = m.Author, Description = m.Description, TimesSaved = m.TimesSaved }).ToList();
            }
            else if (search.Type == "Author")
            {
                searchReturn = (from m in _repo.Query<Note>() where m.Author.ToLower() == search.SearchPhrase.ToLower() && m.Published == true orderby m.TimesSaved descending select new SearchDetailViewModel { resultId = m.Id, resultTitle = m.Title, Author = m.Author, Description = m.Description, TimesSaved = m.TimesSaved }).ToList();
            }
            else
            {
                return View();
            }
            return View(searchReturn);
        }

        public ActionResult FullView(string id)
        {
            var authorProfile = (from a in _repo.Query<ApplicationUser>() where a.UserName == id select new ViewAuthorViewModel { UserName = a.UserName, FirstName = a.FirstName, LastName = a.LastName, About = a.About }).SingleOrDefault();
            authorProfile.UserNotes = (from a in _repo.Query<User_Note>() where a.User.UserName == id && a.Note.Published == true select new NoteViewModel { Title = a.Note.Title, TimesSaved = a.Note.TimesSaved }).ToList();
            return View(authorProfile);
        }

        public ActionResult NotesView(int id)
        {
            var foundNote = (from n in _repo.Query<Note>() where n.Id == id select new NoteViewModel { Id = n.Id, Title = n.Title, Content = n.Content, TimesSaved = n.TimesSaved }).ToList();
            return View(foundNote);
        }
    }
}
