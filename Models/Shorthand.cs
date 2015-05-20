using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoteTakerApplication.Models
{
    public class Shorthand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Author { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }


        public ICollection<Keybind> KeybindList { get; set; }

        //public ICollection<Note> NoteList { get; set; }

        //How to implement tags? I think RegEx in there somewhere.
        [Required]
        public string Tags { get; set; }

        //Increments when other users add the shorthand to their own profiles. Decrements when other users report it.
        [Display(Name = "Saves")]
        public int? TimesSaved { get; set; }

        public bool Published { get; set; }

        //logic: whenever saved, + 1

        //is private true/false
    }
}