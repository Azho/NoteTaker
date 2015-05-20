using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NoteTakerApplication.Models
{

    public class Keybind
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Keybinds require a modifier key (i.e. Ctrl, Shift, Alt)")]
        public string Modifier { get; set; }

        [Required(ErrorMessage = "Please set an activation key for your keybind.")]
        public string Key { get; set; }

        public string Description { get; set; }

       
    }
}