using System;
using System.ComponentModel.DataAnnotations;

namespace StaffSignInBoard.Models
{
    public enum EventType
    {
        SignIn,
        SignOut
    }

    public class SignInOutEvent
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Staff Member")]
        public string StaffMemberName { get; set; }

        [Required]
        [Display(Name = "Library")]
        public string Library { get; set; }

        [Required]
        [Display(Name = "Area")]
        public string Area { get; set; }

        [Required]
        [Display(Name = "Room Number/Area")]
        public string RoomNumber { get; set; }

        [Display(Name = "Specific Location")]
        public string SpecificLocation { get; set; }

        [Display(Name = "Timestamp")]
        public DateTime TimeStamp { get; set; }

        [Required]
        [Display(Name = "Visit Reason")]
        public string Reason { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        public EventType EventType { get; set; }

        public SignInOutEvent()
        {
        }

    }
}
