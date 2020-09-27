using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BirrasApp.Repositories.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    MeetupDate = table.Column<DateTimeOffset>(nullable: false),
                    Temperature = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    UserRol = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMeetUps",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    MeetupId = table.Column<int>(nullable: false),
                    CheckedIn = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMeetUps", x => new { x.UserId, x.MeetupId });
                    table.ForeignKey(
                        name: "FK_UserMeetUps_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMeetUps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRequestToMeetUp",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    MeetupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequestToMeetUp", x => new { x.UserId, x.MeetupId });
                    table.ForeignKey(
                        name: "FK_UserRequestToMeetUp_Meetups_MeetupId",
                        column: x => x.MeetupId,
                        principalTable: "Meetups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRequestToMeetUp_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "UserRol", "Username" },
                values: new object[] { 1, "aosanchez@gmail.com", new byte[] { 13, 166, 151, 18, 151, 113, 126, 244, 219, 39, 249, 97, 208, 10, 97, 231, 180, 104, 108, 137, 41, 147, 161, 204, 148, 4, 218, 198, 78, 46, 197, 103, 178, 155, 13, 16, 222, 67, 189, 26, 62, 0, 200, 58, 25, 225, 46, 23, 169, 128, 167, 89, 127, 118, 5, 175, 214, 75, 173, 167, 84, 16, 13, 52 }, new byte[] { 248, 227, 237, 23, 22, 162, 181, 232, 245, 153, 51, 255, 190, 165, 205, 247, 190, 245, 151, 96, 87, 137, 253, 110, 93, 16, 158, 144, 118, 17, 231, 64, 164, 171, 49, 112, 23, 114, 108, 85, 194, 252, 109, 118, 75, 199, 142, 83, 159, 212, 19, 34, 75, 46, 239, 151, 180, 155, 118, 42, 243, 91, 235, 128, 231, 72, 155, 61, 215, 168, 28, 2, 240, 66, 201, 70, 182, 31, 23, 246, 123, 155, 147, 26, 51, 255, 2, 49, 90, 209, 232, 58, 134, 182, 151, 202, 220, 252, 97, 32, 33, 144, 112, 151, 130, 106, 245, 124, 44, 203, 192, 219, 154, 17, 182, 137, 135, 108, 169, 222, 38, 79, 176, 138, 41, 11, 163, 89 }, 0, "userAdmin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "UserRol", "Username" },
                values: new object[] { 2, "user1@santander.com", new byte[] { 107, 169, 203, 47, 101, 60, 184, 38, 178, 153, 200, 241, 247, 148, 243, 227, 234, 40, 181, 186, 55, 208, 147, 116, 152, 176, 26, 163, 17, 240, 118, 120, 33, 27, 202, 115, 67, 129, 43, 174, 63, 198, 8, 187, 164, 208, 82, 101, 211, 225, 58, 248, 132, 140, 191, 84, 54, 93, 64, 75, 243, 74, 96, 161 }, new byte[] { 125, 251, 250, 205, 96, 43, 50, 192, 252, 95, 173, 249, 230, 84, 166, 188, 192, 56, 30, 59, 144, 68, 253, 131, 27, 47, 11, 7, 177, 194, 199, 40, 228, 17, 98, 218, 10, 72, 143, 209, 222, 196, 229, 10, 51, 222, 205, 163, 85, 205, 174, 219, 112, 138, 207, 138, 17, 37, 211, 196, 120, 56, 248, 77, 203, 96, 53, 1, 135, 64, 39, 1, 36, 245, 231, 99, 188, 69, 113, 13, 213, 205, 8, 19, 129, 68, 226, 189, 48, 82, 108, 24, 131, 32, 186, 208, 137, 202, 59, 252, 120, 62, 71, 134, 236, 191, 121, 168, 254, 128, 195, 128, 205, 196, 43, 215, 222, 137, 218, 37, 123, 202, 118, 97, 125, 38, 85, 217 }, 1, "user1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "UserRol", "Username" },
                values: new object[] { 3, "user2@santander.com", new byte[] { 61, 84, 164, 106, 210, 235, 243, 75, 234, 246, 114, 144, 171, 62, 33, 54, 99, 39, 209, 218, 139, 30, 196, 194, 220, 248, 184, 217, 220, 136, 253, 193, 15, 4, 147, 51, 63, 130, 250, 54, 20, 186, 143, 193, 79, 111, 11, 230, 24, 230, 9, 232, 153, 138, 209, 98, 92, 219, 109, 187, 198, 38, 99, 228 }, new byte[] { 115, 7, 52, 76, 112, 68, 64, 182, 148, 240, 66, 39, 5, 90, 234, 8, 140, 17, 25, 141, 88, 153, 68, 179, 172, 216, 8, 210, 37, 226, 90, 238, 122, 243, 177, 6, 7, 17, 5, 188, 75, 111, 197, 98, 33, 8, 246, 33, 195, 60, 252, 157, 198, 34, 52, 117, 118, 221, 192, 155, 47, 140, 228, 41, 181, 211, 169, 173, 99, 36, 142, 160, 247, 229, 67, 161, 104, 124, 151, 102, 96, 33, 234, 116, 132, 140, 70, 154, 150, 241, 41, 208, 188, 85, 228, 81, 82, 215, 86, 36, 100, 105, 31, 7, 71, 134, 202, 119, 149, 111, 97, 214, 242, 105, 108, 124, 22, 226, 53, 131, 92, 237, 84, 48, 251, 184, 48, 57 }, 1, "user2" });

            migrationBuilder.CreateIndex(
                name: "IX_UserMeetUps_MeetupId",
                table: "UserMeetUps",
                column: "MeetupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequestToMeetUp_MeetupId",
                table: "UserRequestToMeetUp",
                column: "MeetupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMeetUps");

            migrationBuilder.DropTable(
                name: "UserRequestToMeetUp");

            migrationBuilder.DropTable(
                name: "Meetups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
