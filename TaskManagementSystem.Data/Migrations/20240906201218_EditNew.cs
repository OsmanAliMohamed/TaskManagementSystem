using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1449048a-9fb4-4e15-8400-46efbfbdfbdb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2be4f3b1-bd9a-4509-8b02-30c59102ec4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a50332fb-67cd-4c82-a867-669b971886ae");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36eb7c5c-e476-46db-9b41-f4ba97154235", null, "TeamLeader", "TEAMLEADER" },
                    { "649db79e-7103-44c5-923e-cc19066df646", null, "User", "USER" },
                    { "eff9758e-924b-4f21-a856-e89d5592d16c", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36eb7c5c-e476-46db-9b41-f4ba97154235");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "649db79e-7103-44c5-923e-cc19066df646");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eff9758e-924b-4f21-a856-e89d5592d16c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1449048a-9fb4-4e15-8400-46efbfbdfbdb", null, "TeamLeader", "TEAMLEADER" },
                    { "2be4f3b1-bd9a-4509-8b02-30c59102ec4e", null, "User", "USER" },
                    { "a50332fb-67cd-4c82-a867-669b971886ae", null, "Admin", "ADMIN" }
                });
        }
    }
}
