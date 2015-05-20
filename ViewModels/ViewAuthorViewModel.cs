using NoteTakerApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteTakerApplication.ViewModels
{
    public class ViewAuthorViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        //How to get that user's list of shorthand? Feed again into how to make content specific to user?
        //public List<string> TheirShorthand { get; set; }
        public IList<NoteViewModel> UserNotes { get; set; }
    
    }
}