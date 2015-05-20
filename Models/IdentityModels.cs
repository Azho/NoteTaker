using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoteTakerApplication.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "First Name:")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name:" )]
        public string LastName { get; set; }
        //public ICollection<Shorthand> UserShorthand { get; set; }
        //public ICollection<Note> UserNotes { get; set; }
        public string About { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public IDbSet<Profile> Profiles { get; set; }

        public IDbSet<User_Note> Users_Note { get; set; }

        public IDbSet<Note> Notes { get; set; }

        //public IDbSet<Keybind> Keybinds { get; set; }

        //public IDbSet<Shorthand> Shorthands { get; set; }

        //public IDbSet<ShorthandSearchTypes> SearchTypes { get; set; }

        public ApplicationDbContext()   
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new DatabaseInitializer());

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}