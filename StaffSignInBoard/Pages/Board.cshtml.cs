using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StaffSignInBoard.Models;

namespace StaffSignInBoard.Pages
{
    public class BoardModel : PageModel
    {
        private SignInOutBoardContext _dbContext;

        private readonly ILogger<BoardModel> _logger;

        [BindProperty]
        public List<SignInOutEvent> Events { get; set; }

        public List<StaffMember> StaffMembers { get; set; }

        public BoardModel(ILogger<BoardModel> logger, SignInOutBoardContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void OnGet()
        {
            StaffMembers = _dbContext.StaffMembers.ToList();
            Events = _dbContext.SignInOutEvents.FromSqlRaw(@"
                select Id, Library, RoomNumber, SpecificLocation, StaffMemberName, max([TimeStamp]) as [TimeStamp], Notes, EventType, Area, Reason from SignInOutEvents
group by StaffMemberName").ToList();
        }
    }
}
