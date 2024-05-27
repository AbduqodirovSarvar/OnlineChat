using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineChat.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a888bbd0-7059-4a5b-b057-1069246ef7f8"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "PhotoName", "Role" },
                values: new object[,]
                {
                    { new Guid("6ac565d4-b195-4914-89b4-c5e66250e216"), new DateTime(2024, 5, 27, 5, 35, 0, 649, DateTimeKind.Utc).AddTicks(3984), "user2@gmail.com", "User2 firstname", false, "user2 lastname", "1tKDv2JuGuEDLVjB8GcCuDSTiym1EA2kwsfkahclfUhvO8FxnhtaqXHRozOrngn6ayXJQai7y0nRGaPpThND2A==", null, 1 },
                    { new Guid("c6d6e9df-a709-404c-977c-bffca10922d4"), new DateTime(2024, 5, 27, 5, 35, 0, 642, DateTimeKind.Utc).AddTicks(151), "abduqodirovsarvar.2002@gmail.com", "SuperAdmin", false, "SuperAdmin", "tyoclraLokrpjuQVLrZ0XVEwyxX/TlT7hVah4sGdM1m3eJgEO2qqj/qa7O9ayPHz1/qgUmfjfYIhLTVCRWVfpQ==", null, 3 },
                    { new Guid("f4770fa9-9fab-46b7-adcf-3e6dd964f7ff"), new DateTime(2024, 5, 27, 5, 35, 0, 645, DateTimeKind.Utc).AddTicks(7252), "user1@gmail.com", "User1 firstname", false, "User1 lastname", "iXAw+vYbv5JgAJUS6jyaEVj/1cwgkS364EWBMV/6ENGBcDQfdQG2fpW3/nqYFXenz9gVAAdNC5hm8d2l/zhOjw==", null, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6ac565d4-b195-4914-89b4-c5e66250e216"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c6d6e9df-a709-404c-977c-bffca10922d4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f4770fa9-9fab-46b7-adcf-3e6dd964f7ff"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsDeleted", "LastName", "PasswordHash", "PhotoName", "Role" },
                values: new object[] { new Guid("a888bbd0-7059-4a5b-b057-1069246ef7f8"), new DateTime(2024, 5, 23, 14, 3, 32, 731, DateTimeKind.Utc).AddTicks(28), "abduqodirovsarvar.2002@gmail.com", "SuperAdmin", false, "SuperAdmin", "mgub682wLvVoFShP2yuP72G7qYggK/T52lmH7WZKRK2kC7pKVdSQUZIEZXeSlho8S92YgMSFQlRyia0UTYJ/Kg==", null, 3 });
        }
    }
}
