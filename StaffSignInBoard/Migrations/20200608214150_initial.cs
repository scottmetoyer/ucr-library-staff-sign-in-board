using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StaffSignInBoard.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SignInOutEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StaffMemberName = table.Column<string>(nullable: false),
                    Library = table.Column<string>(nullable: false),
                    Area = table.Column<string>(nullable: false),
                    RoomNumber = table.Column<string>(nullable: true),
                    SpecificLocation = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    EventType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignInOutEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Magstripe = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffMembers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SignInOutEvents");

            migrationBuilder.DropTable(
                name: "StaffMembers");
        }
    }
}
