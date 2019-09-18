using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroXchange.Data.Migrations
{
    public partial class sqlmigrationv11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 1, "Farmer" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 2, "Aggregator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 3, "Wholesaler" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 4, "Processor" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 5, "Offtaker" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 6, "Supplier" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Roles", "RoleId", 1);
            migrationBuilder.DeleteData("Roles", "RoleId", 2);
            migrationBuilder.DeleteData("Roles", "RoleId", 3);
            migrationBuilder.DeleteData("Roles", "RoleId", 4);
            migrationBuilder.DeleteData("Roles", "RoleId", 5);
            migrationBuilder.DeleteData("Roles", "RoleId", 6);
        }
    }
}
