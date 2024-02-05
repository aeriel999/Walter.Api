using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Walter.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c599d2d-6515-414a-ae6a-a86b0c841b3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e60cb252-8154-4523-a9fb-2c1d76cf92a6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c2ffefc-b3ff-41bf-8a2c-5d7ac5a94591");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c65b27fe-83a0-466a-a4e1-42c248bdd64e");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c599d2d-6515-414a-ae6a-a86b0c841b3b", null, "User", "USER" },
                    { "e60cb252-8154-4523-a9fb-2c1d76cf92a6", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7c2ffefc-b3ff-41bf-8a2c-5d7ac5a94591", 0, "c374c24a-3c9c-4f63-9eea-015360e785f9", "ApiUser", "user1@email.com", true, "Alice", "", false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "8549ce65-3659-4a89-ac07-30bc7ddd7958", false, "user1@email.com" },
                    { "c65b27fe-83a0-466a-a4e1-42c248bdd64e", 0, "8138cbb8-e714-4c29-8a32-ebe48011a293", "ApiUser", "admin@email.com", true, "Bob", "", false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "6663d0af-c9cb-482d-b9f2-47b5f316f4a6", false, "admin@email.com" }
                });
        }
    }
}
