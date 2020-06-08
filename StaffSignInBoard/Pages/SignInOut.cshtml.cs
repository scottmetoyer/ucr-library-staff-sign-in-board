using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using StaffSignInBoard.Models;

namespace StaffSignInBoard.Pages
{
    public class SignInOutModel : PageModel
    {
        private SignInOutBoardContext _dbContext;

        private readonly ILogger<SignInOutModel> _logger;

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        public List<SelectListItem> Libraries { get; set; }

        public List<SelectListItem> Areas { get; set; }

        public List<SelectListItem> Reasons { get; set; }

        [BindProperty]
        public SignInOutEvent SignInOutEvent { get; set; }

        public List<StaffMember> StaffMembers { get; set; }

        public SignInOutModel(ILogger<SignInOutModel> logger, SignInOutBoardContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult OnGet(int? id)
        {
            if (!id.HasValue)
            {
                return Redirect("~/");
            }

            try
            {
                this.InitializePage(id.Value);
            }
            catch(Exception ex)
            {
                this.ErrorMessage = "There was an error loading your information. Please try again.";
            }

            return Page();
        }

        private void InitializePage(int id)
        {
            this.SignInOutEvent = new SignInOutEvent();
            this.StaffMembers = _dbContext.StaffMembers.OrderBy(x => x.Name).ToList();

            var staffMember = this.StaffMembers.FirstOrDefault(x => x.Id == id);
            this.SignInOutEvent.StaffMemberName = staffMember.Name;

            var lastSignIn = _dbContext.SignInOutEvents.OrderByDescending(x => x.TimeStamp).Where(x => x.StaffMemberName == staffMember.Name).FirstOrDefault();

            if (lastSignIn != null)
            {
                this.SignInOutEvent.Library = lastSignIn.Library;
                this.SignInOutEvent.Area = lastSignIn.Area;
                this.SignInOutEvent.Reason = lastSignIn.Reason;
                this.SignInOutEvent.RoomNumber = lastSignIn.RoomNumber;
                this.SignInOutEvent.SpecificLocation = lastSignIn.SpecificLocation;
            }
         
            SuccessMessage = string.Empty;
            ErrorMessage = string.Empty;
        }

        public void SaveSignInOutEvent()
        {
            try
            {
                SignInOutEvent.TimeStamp = DateTime.Now;
                _dbContext.SignInOutEvents.Add(SignInOutEvent);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Error saving event");
                this.ErrorMessage = "There was an error signing in. Please try again.";
            }
        }

        public IActionResult OnPostSignIn()
        {
            if (!ModelState.IsValid)
            {
                SuccessMessage = string.Empty;
                ErrorMessage = string.Empty;
                return Page();
            }

            SignInOutEvent.EventType = EventType.SignIn;
            this.SaveSignInOutEvent();
            return new RedirectResult("~/?message=signin");
        }

        public IActionResult OnPostSignOut()
        {
            if (!ModelState.IsValid)
            {
                SuccessMessage = string.Empty;
                ErrorMessage = string.Empty;
                return Page();
            }

            SignInOutEvent.EventType = EventType.SignOut;
            this.SaveSignInOutEvent();
            return new RedirectResult("~/?message=signout");
        }
    }
}
