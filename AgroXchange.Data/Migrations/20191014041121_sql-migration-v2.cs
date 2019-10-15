using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroXchange.Data.Migrations
{
    public partial class sqlmigrationv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farms",
                schema: "dbo",
                columns: table => new
                {
                    FarmId = table.Column<Guid>(nullable: false),
                    FarmType = table.Column<string>(type: "nvarchar(9)", nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    LatitudeRad = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    LongitudeRad = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.FarmId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Farms_Latitude",
                schema: "dbo",
                table: "Farms",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Farms_LatitudeRad",
                schema: "dbo",
                table: "Farms",
                column: "LatitudeRad");

            migrationBuilder.CreateIndex(
                name: "IX_Farms_Longitude",
                schema: "dbo",
                table: "Farms",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_Farms_LongitudeRad",
                schema: "dbo",
                table: "Farms",
                column: "LongitudeRad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Farms",
                schema: "dbo");
        }
    }
}
