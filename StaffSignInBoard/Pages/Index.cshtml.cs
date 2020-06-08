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
        public string SwipeInput { get; set; }

        public IndexModel(ILogger<IndexModel> logger, SignInOutBoardContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void OnGet(string message)
        {
            this.InitializePage();

            if (!string.IsNullOrEmpty(message))
            {
                switch (message)
                {
                    case "signin":
                        this.SuccessMessage = "You have been successfully signed in.";
                        break;

                    case "signout":
                        this.SuccessMessage = "You have been successfully signed out";
                        break;
                }
            }
        }

        private void InitializePage()
        {
            SuccessMessage = string.Empty;
            ErrorMessage = string.Empty;
        }

        public IActionResult OnPost()
        {
            this.InitializePage();

            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                this.SwipeInput = string.Empty;
                this.ErrorMessage = "Swipe not registered. Please try again.";
                return Page();
            }

            var staffMember = _dbContext.StaffMembers.FirstOrDefault(x => x.Magstripe == this.SwipeInput.Trim());

            if(staffMember != null)
            {
                return new RedirectResult("~/SignInOut?id=" + staffMember.Id);
            } else
            {
                ModelState.Clear();
                this.SwipeInput = string.Empty;
                this.ErrorMessage = "User not found. Please login with your username and password by clicking the link below.";
            }

            return Page();
        }
    }
}
