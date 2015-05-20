using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using NoteTakerApplication.Models;
using Twilio;
using System.Diagnostics;
using SendGrid;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace NoteTakerApplication
{
    //Contains ApplicationUserManager and ApplicationSignInManager classes.
    //Also password validation and confirm messages, etc.

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return configSendGridasync(message);
        }

        private Task configSendGridasync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From = new MailAddress("morey269@gmail.com", "Account Activation");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential("4zho", "Aeromar9");

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            if (transportWeb != null)
            {
                return transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                return Task.FromResult(0);
            }
        }
    }

    //public class EmailService : IIdentityMessageService
    //{
    //    public async Task SendAsync(IdentityMessage message)
    //    {
    //        await configSendGridasync(message);
    //    }

    //    // Use NuGet to install SendGrid (Basic C# client lib) 
    //    private async Task configSendGridasync(IdentityMessage message)
    //    {
    //        var myMessage = new SendGridMessage();
    //        myMessage.AddTo(message.Destination);
    //        myMessage.From = new System.Net.Mail.MailAddress(
    //                            "morey269@gmail.com", "Account Activation");
    //        myMessage.Subject = message.Subject;
    //        myMessage.Text = message.Body;
    //        myMessage.Html = message.Body;

    //        var credentials = new NetworkCredential(
    //                   ConfigurationManager.AppSettings["4zho"],
    //                   ConfigurationManager.AppSettings["Aeramar9"]
    //                   );

    //        // Create a Web transport for sending email.
    //        var transportWeb = new Web(credentials);

    //        // Send the email.
    //        if (transportWeb != null)
    //        {
    //            await transportWeb.DeliverAsync(myMessage);
    //        }
    //        else
    //        {
    //            Trace.TraceError("Failed to create Web transport.");
    //            await Task.FromResult(0);
    //        }
    //    }
    //}

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var Twilio = new TwilioRestClient("ACfe29f8f495654321e9fa3f8aaa302d77", "f804f00b1ac0ecdd8e8c2ce005e7687d");
            var result = Twilio.SendMessage("(443) 798-3611", message.Destination, message.Body);

            // Status is one of Queued, Sending, Sent, Failed or null if the number is not valid
            Trace.WriteLine(result.Status);

            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    //Signs in users?
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
