using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StaffSignInBoard.Models;

namespace StaffSignInBoard.Pages
{
    public class RefreshDatabaseModel : PageModel
    {
        private SignInOutBoardContext _dbContext;

        private readonly ILogger<RefreshDatabaseModel> _logger;

        private readonly IConfiguration _configuration;

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }

        public RefreshDatabaseModel(IConfiguration configuration, ILogger<RefreshDatabaseModel> logger, SignInOutBoardContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _logger = logger;
        }

        public void OnGet()
        {
            // Load CSV file

            using (var reader = new StreamReader(_configuration["DemographicsFileLocation"]))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                var records = csv.GetRecords<Demographic>().Where(x => x.Department == "University Library").ToList();
                var recordCount = records.Count;

                // Purge old records
                _dbContext.StaffMembers.RemoveRange(_dbContext.StaffMembers);
                _dbContext.SaveChanges();

                int index = 1;

                foreach (var record in records)
                {
                    var staff = new StaffMember()
                    {
                        Id = index,
                        Magstripe = record.Barcode1,
                        Username = record.Netid,
                        Name = record.FirstName + " " + record.LastName,
                        Image = (record.FirstName + "-" + record.LastName + ".jpg").Replace(" ", "-")
                    };

                    _dbContext.StaffMembers.Add(staff);
                    index++;
                }

                _dbContext.SaveChanges();

                this.SuccessMessage = "Database has been refreshed with " + index + " records";
            }
        }
    }
}
