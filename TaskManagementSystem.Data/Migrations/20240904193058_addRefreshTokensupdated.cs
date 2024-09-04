using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addRefreshTokensupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "305e461a-f377-47c5-8f3d-28e3d8b57817", null, "User", "USER" },
                    { "a271770a-c57b-4ca0-9d51-a5ff78e7ee0b", null, "Admin", "ADMIN" },
                    { "b05a03af-28b6-4efb-9c34-2dbc6aa31283", null, "TeamLeader", "TEAMLEADER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "305e461a-f377-47c5-8f3d-28e3d8b57817");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a271770a-c57b-4ca0-9d51-a5ff78e7ee0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b05a03af-28b6-4efb-9c34-2dbc6aa31283");
        }
    }
}
