using System;
using CsvHelper.Configuration.Attributes;

namespace StaffSignInBoard.Models
{
    public class Demographic
    {
        [Index(0)]
        public string Id { get; set; }

        [Index(1)]
        public string Netid { get; set; }

        [Index(2)]
        public string FirstName { get; set; }

        [Index(3)]
        public string MiddleName { get; set; }

        [Index(4)]
        public string LastName { get; set; }

        [Index(5)]
        public string PreferredFirstName { get; set; }

        [Index(6)]
        public string PreferredLastName { get; set; }

        [Index(7)]
        public string Level { get; set; }

        [Index(8)]
        public string Address1 { get; set; }

        [Index(9)]
        public string Address2 { get; set; }

        [Index(10)]
        public string City { get; set; }

        [Index(11)]
        public string State { get; set; }

        [Index(12)]
        public string Zip { get; set; }

        [Index(13)]
        public string Phone1 { get; set; }

        [Index(14)]
        public string Phone2 { get; set; }

        [Index(15)]
        public string DOB { get; set; }

        [Index(16)]
        public string Email1 { get; set; }

        [Index(17)]
        public string Email2 { get; set; }

        [Index(18)]
        public string Sex { get; set; }

        [Index(19)]
        public string Department { get; set; }

        [Index(20)]
        public string Race { get; set; }

        [Index(21)]
        public string Barcode1 { get; set; }

        [Index(22)]
        public string Barcode2 { get; set; }

        public Demographic()
        {


        }
    }
}
