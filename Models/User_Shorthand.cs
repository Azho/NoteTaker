using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoteTakerApplication.Models
{
    public class User_Shorthand
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int ShorthandId { get; set; }

        public virtual Shorthand Shorthand { get; set; }


    }
}