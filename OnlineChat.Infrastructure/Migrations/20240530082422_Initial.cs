using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    PhotoName = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
                    Msg = table.Column<string>(type: "text", nullable: false),
                    IsSeen = table.Column<bool>(type: "boolean", nullable: false),
                    SeenAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "PhotoName", "Role" },
                values: new object[,]
                {
                    { new Guid("05d862aa-0fb2-493e-9a75-63823a5f7426"), new DateTime(2024, 5, 30, 8, 24, 21, 886, DateTimeKind.Utc).AddTicks(6525), "abduqodirovsarvar.2002@gmail.com", "SuperAdmin", false, "SuperAdmin", "JB+FNBSsH2Il6OsezHksIwj+E0d5332YJ47UV+nlGeCyl6JVj0mC29bs+IK1SP5y+K9uztUNK/BoqxTTVBxpZg==", null, 3 },
                    { new Guid("6e39c6f8-e2a3-49ab-88b4-a51f9af457c0"), new DateTime(2024, 5, 30, 8, 24, 21, 890, DateTimeKind.Utc).AddTicks(8460), "user1@gmail.com", "User1 firstname", false, "User1 lastname", "90sdOz/PPC1VoeD/CNNZmEYaPrBnUgIKcB53LUhwZvZeSwfonRjJz9tcD0zaCda57LtkfGFhhjbqHpzJ5C7+lA==", null, 1 },
                    { new Guid("92b939ac-dcab-416e-bb78-7080cba8afa2"), new DateTime(2024, 5, 30, 8, 24, 21, 894, DateTimeKind.Utc).AddTicks(7942), "user2@gmail.com", "User2 firstname", false, "user2 lastname", "4no4wEFWsh8VJfZwHGysXLcnEluJQoLJbOeRGTvfbiBdIbJyrCmGIY+/wuWldfDPFRPxfEyFi0h0h8W4s3ooiA==", null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
