using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroXchange.Data.Migrations
{
    public partial class sqlmigrationv13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActivationMailDate",
                schema: "dbo",
                table: "Users",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetExpiry",
                schema: "dbo",
                table: "Users",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationMailDate",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetExpiry",
                schema: "dbo",
                table: "Users");
        }
    }
}
