using NoteTakerApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoteTakerApplication.ViewModels
{
    public class SearchDetailViewModel
    {
        public int resultId { get; set; }
        [Display(Name = "Title")]
        public string resultTitle { get; set; }

        public string Description { get; set; }

        [Display(Name = "Saves")]
        public int? TimesSaved { get; set; }

        public string Author { get; set; }
    }
}