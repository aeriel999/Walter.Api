using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Walter.Api.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Token : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e65b0419-54c3-4a7a-ad3d-1ec55ada878b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e724ce45-bd49-4933-bfa2-427d8387ede4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2e1ee6ff-5219-4bc4-8529-348a1fd47386");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "540eba57-8427-4d46-8b26-3cd3b13085d8");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e65b0419-54c3-4a7a-ad3d-1ec55ada878b", null, "User", "USER" },
                    { "e724ce45-bd49-4933-bfa2-427d8387ede4", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2e1ee6ff-5219-4bc4-8529-348a1fd47386", 0, "294b673a-87ec-43ed-a7b0-1dfcdf68d5f7", "ApiUser", "user1@email.com", true, "Alice", "", false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "dc8a5165-b387-4fd0-88b5-44c5df83c84d", false, "user1@email.com" },
                    { "540eba57-8427-4d46-8b26-3cd3b13085d8", 0, "80f3b7a0-2335-4858-ac1d-d5105fc875b0", "ApiUser", "admin@email.com", true, "Bob", "", false, null, null, null, null, "+xx(xxx)xxx-xx-xx", true, "d47c6c8e-3b9f-4234-9b50-91ec05783d07", false, "admin@email.com" }
                });
        }
    }
}
