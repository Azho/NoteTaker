using NoteTakerApplication.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using NoteTakerApplication.Models;

namespace NoteTakerApplication.Models
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //var accountManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var store = new UserStore<ApplicationUser>(context);
            var accountManager = new UserManager<ApplicationUser>(store);
            //var profiles = new List<Profile>
            //{


            //    new Profile {UserName="HanSolo", FirstName="Joey", LastName="Jenkins", Email = "fakeEmail@fake.com", About = "English major", EmailConfirmed = true},
            //    new Profile {UserName="Handcock", FirstName="BillyBob", LastName="Bobson", Email = "hisEmail@fake.com", About = "Arabic major", EmailConfirmed = true},
            //    new Profile {UserName="MrHan", FirstName="Bubba", LastName="Bubbles", Email = "fakeEmail@fake.com", About = "Business major", EmailConfirmed = true},
            //    new Profile {UserName="handsome", FirstName="Huggins", LastName="Wigglewoggles", Email = "fakeEmail@fake.com", About = "Psychology major", EmailConfirmed = true},
            //    new Profile {UserName="Chewie", FirstName="Bobby", LastName="Benson", Email="notFound@fake.com", About = "Aspiring football player", EmailConfirmed = true},
            //    new Profile {UserName="LukeSucks", FirstName="Parker", LastName="Parker", Email="whereisit@fake.com", About = "Not Spiderman", EmailConfirmed = true},
            //    new Profile {UserName = "4zho", FirstName="M", LastName="P", Email="morey269@gmail.com", About = "Confused", EmailConfirmed = true}
            //};
            //profiles.ForEach(p => accountManager.Create(p, "Blahblah8*"));

            ApplicationUser user = new ApplicationUser()
            {
                Email = "chaz.biroan@yahoo.com",
                UserName = "chaz.biroan@yahoo.com",
                EmailConfirmed = true
            };

            accountManager.Create(user, "a12345");
            //Each new Note has its own shorthand page.
            //var keybinds = new List<Keybind>
            //{
            //    new Keybind {Key="`", Type=KeyDropdown.Text, Description="Turns all text to red"},
            //    new Keybind {Key="/", Type=KeyDropdown.Image, Description="Prints an arrow to indicate causation"},
            //    new Keybind {Key="]", Type=KeyDropdown.Box, Description="Creates a dotted border box for vocabulary and meaning."}
            //};
            //keybinds.ForEach(s => context.Keybinds.Add(s));

            //var shorthands = new List<Shorthand>
            //{
            //    new Shorthand { Title = "Calc Notes", Author = profiles[0], KeybindList=keybinds, TimesSaved = 0, Tags = "help, math, omg"},
            //    new Shorthand { Title = "Arabic Vocab", Author = profiles[1], KeybindList=keybinds, TimesSaved = 0, Tags = "vocabulary, definitions"},
            //    new Shorthand { Title = "Criminology", Author = profiles[2], KeybindList=keybinds, TimesSaved = 0, Tags = "concepts, definitions, help"},
            //    new Shorthand { Title = "Psychology", Author = profiles[6], KeybindList=keybinds, TimesSaved = 4, Tags = "concepts, definitions, help"},
            //    new Shorthand { Title = "Art History", Author = profiles[1], KeybindList=keybinds, TimesSaved = 5, Tags = "concepts, definitions, help"},
            //    new Shorthand { Title = "The Bible As Literature", Author = profiles[5], KeybindList=keybinds, TimesSaved = 2, Tags = "concepts, definitions, help"},
            //    new Shorthand { Title = "New Calc Notes", Author = profiles[6], KeybindList=keybinds, TimesSaved = 0, Tags = "help, math, omg"},
            //    new Shorthand { Title = "Old Calc Notes", Author = profiles[6], KeybindList=keybinds, TimesSaved = 0, Tags = "help, math, omg"}

            //};
            //shorthands.ForEach(s => context.Shorthands.Add(s));

            ////shorthands.ForEach(s => Console.WriteLine("Hi"));

            //var notes = new List<Note>
            //{
            //    new Note {Title="My Note", Content="I dropped this class like a hot coal"},//I need to put in the shorthand to seed, but how?
            //    new Note {Title="Second Note", Content="This one was better."},
            //    new Note {Title="Death Note", Content="This'll be the day that I die"}
            //};
            //notes.ForEach(n => context.Notes.Add(n));


            //var searchTypes = new List<ShorthandSearchTypes>
            //{
            //    new ShorthandSearchTypes {Type = "Title"},
            //    new ShorthandSearchTypes {Type = "Tag"},
            //    new ShorthandSearchTypes {Type = "Author"}
            //};

        }
    }
}