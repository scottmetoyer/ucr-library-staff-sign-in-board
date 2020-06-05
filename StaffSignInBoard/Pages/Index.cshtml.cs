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
    public class IndexModel : PageModel
    {
        private SignInOutBoardContext _dbContext;

        private readonly ILogger<IndexModel> _logger;

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        [BindProperty]
        public SignInOutEvent SignInOutEvent { get; set; }

        public List<StaffMember> StaffMembers { get; set; }

        public List<SelectListItem> Libraries { get; set; }

        public IndexModel(ILogger<IndexModel> logger, SignInOutBoardContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void OnGet()
        {
            this.InitializePage();
        }

        private void InitializePage()
        {
            this.SignInOutEvent = new SignInOutEvent();
            this.StaffMembers = _dbContext.StaffMembers.ToList();

            // this.StaffMembers = new SelectList(_dbContext.StaffMembers, nameof(StaffMember.Id), nameof(StaffMember.Name));
            this.Libraries = new List<SelectListItem>
            {
                new SelectListItem("Orbach", "Orbach"),
                new SelectListItem("Rivera", "Rivera")
            };

            SuccessMessage = string.Empty;
            ErrorMessage = string.Empty;
        }

        public async Task SaveSignInOutEvent()
        { 
            try
            {
                SignInOutEvent.TimeStamp = DateTime.Now;
                _dbContext.SignInOutEvents.Add(SignInOutEvent);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Error saving event");
                this.ErrorMessage = "There was an error signing in. Please try again.";
            }
           
            InitializePage();
            ModelState.Clear();
        }

        public async Task<IActionResult> OnPostSignInAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SignInOutEvent.EventType = EventType.SignIn;
            await this.SaveSignInOutEvent();
            this.SuccessMessage = "You have been signed in.";
            return Page();
        }

        public async Task<IActionResult> OnPostSignOutAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SignInOutEvent.EventType = EventType.SignOut;
            await this.SaveSignInOutEvent();
            this.SuccessMessage = "You have been signed out.";
            return Page();
        }
    }
}
