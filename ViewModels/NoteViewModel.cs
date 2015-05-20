using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoteTakerApplication.ViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? TimesSaved { get; set; }
        public int? Report { get; set; }
    }
}