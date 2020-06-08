using System;
using System.ComponentModel.DataAnnotations;

namespace StaffSignInBoard.Models
{
    public class StaffMember
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Magstripe { get; set; }

        public string Image { get; set; }

        public string Username { get; set; }

        public StaffMember()
        {     
        }
    }
}
