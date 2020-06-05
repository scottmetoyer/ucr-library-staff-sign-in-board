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
    public class ReportsModel : PageModel
    {
        private SignInOutBoardContext _dbContext;

        private readonly ILogger<ReportsModel> _logger;

        [Required]
        [BindProperty]
        public DateTime StartDate { get; set; }

        [Required]
        [BindProperty]
        public DateTime EndDate { get; set; }

        public List<SignInOutEvent> SearchResults { get; set; }

        public string ErrorMessage { get; set; }

        public ReportsModel(ILogger<ReportsModel> logger, SignInOutBoardContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public void OnGet()
        {
            ErrorMessage = string.Empty;
            StartDate = new DateTime(2020, 1, 1, 0, 0, 0);
            EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            SearchResults = new List<SignInOutEvent>();
        }

        public PageResult OnPost()
        {
            ErrorMessage = string.Empty;
            SearchResults = new List<SignInOutEvent>();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                SearchResults = _dbContext.SignInOutEvents.Where(x => x.TimeStamp >= StartDate).Where(x => x.TimeStamp <= EndDate).OrderByDescending(x => x.TimeStamp).ToList();

                if (SearchResults.Count == 0)
                {
                    this.ErrorMessage = "There were no results. Try another date range.";
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Error running report");
                this.ErrorMessage = "There was an error running the report. Please try again.";
            }

            return Page();
        }
    }
}
