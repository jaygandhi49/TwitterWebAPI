using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwitterWebAPI.Migrations
{
    public partial class TweeterFinalChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TweetMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoginId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SaltPassword = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TweetComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentCommentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    TweetMasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetComments_TweetMasters_TweetMasterId",
                        column: x => x.TweetMasterId,
                        principalTable: "TweetMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TweetComments_Users_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TweetLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    UserDetailsId = table.Column<int>(type: "int", nullable: false),
                    TweetMasterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetLikes_TweetMasters_TweetMasterId",
                        column: x => x.TweetMasterId,
                        principalTable: "TweetMasters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TweetLikes_Users_UserDetailsId",
                        column: x => x.UserDetailsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TweetComments_TweetMasterId",
                table: "TweetComments",
                column: "TweetMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetComments_UserDetailsId",
                table: "TweetComments",
                column: "UserDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetLikes_TweetMasterId",
                table: "TweetLikes",
                column: "TweetMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetLikes_UserDetailsId",
                table: "TweetLikes",
                column: "UserDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TweetComments");

            migrationBuilder.DropTable(
                name: "TweetLikes");

            migrationBuilder.DropTable(
                name: "TweetMasters");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
