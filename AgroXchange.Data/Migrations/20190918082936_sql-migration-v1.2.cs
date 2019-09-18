using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroXchange.Data.Migrations
{
    public partial class sqlmigrationv12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_EmailId",
                schema: "dbo",
                table: "Users",
                column: "EmailId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_EmailId",
                schema: "dbo",
                table: "Users");
        }
    }
}
