using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StaffSignInBoard.Models;

namespace StaffSignInBoard.Pages
{
    public class StaffLookupModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Username { get; set; }

        private SignInOutBoardContext _dbContext;

        private readonly ILogger<StaffLookupModel> _logger;

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        public StaffLookupModel(ILogger<StaffLookupModel> logger, SignInOutBoardContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        private void Initialize()
        {
            this.SuccessMessage = string.Empty;
            this.ErrorMessage = string.Empty;
            this.Username = string.Empty;
        }

        public void OnGet()
        {
            this.Initialize();
        }

        public IActionResult OnPost()
        {
            var staffMember = _dbContext.StaffMembers.FirstOrDefault(x => x.Username.ToLower() == this.Username.ToLower());

            if (staffMember == null)
            {
                this.ErrorMessage = "User not found. Please check your entries and try again";
                this.SuccessMessage = string.Empty;
                return Page();
            }

            return new RedirectResult("SignInOut?id=" + staffMember.Id);
        }
    }
}
