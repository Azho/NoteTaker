using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteTakerApplication.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Please enter a title.")]
        public string Title { get; set; }

        public string Author { get; set; }

        [Required]
        public string Tags { get; set; }

        //Increments when other users saves the note to their own profiles. Decrements when other users report it.
        [Display(Name = "Saves")]
        public int? TimesSaved { get; set; }

        //when saved, + 1
        public bool Published { get; set; }

        //when reported, +1
        [Display(Name = "Reports")]
        public int Report { get; set; }

        public string Description { get; set; }
        
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Content { get; set; }

        public string Username { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

    }
}