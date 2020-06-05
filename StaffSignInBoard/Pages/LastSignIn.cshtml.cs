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
    public class LastSignInModel : PageModel
    {
        private SignInOutBoardContext _dbContext;

        private readonly ILogger<IndexModel> _logger;

        public LastSignInModel(ILogger<IndexModel> logger, SignInOutBoardContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult OnGet(string name)
        {
            var lastSignIn = _dbContext.SignInOutEvents.OrderBy(x => x.TimeStamp).Where(x => x.StaffMemberName == name).FirstOrDefault();
            return new JsonResult(lastSignIn);
        }
    }
}
