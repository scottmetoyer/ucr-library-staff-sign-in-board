using System;
using Microsoft.EntityFrameworkCore;

namespace StaffSignInBoard.Models
{
    public class SignInOutBoardContext : DbContext
    {
        public DbSet<SignInOutEvent> SignInOutEvents { get; set; }

        public DbSet<StaffMember> StaffMembers { get; set; }

        public SignInOutBoardContext()
        {

        }

        public SignInOutBoardContext(DbContextOptions<SignInOutBoardContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
        }
    }
}
