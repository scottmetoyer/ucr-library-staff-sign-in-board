using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StaffSignInBoard.Models;

namespace StaffSignInBoard.Pages
{
    public class SignInOutEventModel : PageModel
    {
        private SignInOutBoardContext _dbContext;

        public SignInOutEvent Event { get; set; }

        private readonly ILogger<SignInOutEventModel> _logger;

        public SignInOutEventModel(ILogger<SignInOutEventModel> logger, SignInOutBoardContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void OnGet(int id)
        {
            Event = _dbContext.SignInOutEvents.FirstOrDefault(x => x.Id == id);
        }
    }
}
