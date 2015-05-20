using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoteTakerApplication.Models
{
    public class User_Note
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int NoteId { get; set; }

        public virtual Note Note { get; set; }

        public int Reported { get; set; }
    }
}