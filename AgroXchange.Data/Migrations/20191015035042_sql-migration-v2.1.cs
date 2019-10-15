using Microsoft.EntityFrameworkCore.Migrations;

namespace AgroXchange.Data.Migrations
{
    public partial class sqlmigrationv21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE GetFarmsInRadius 
	            @Lat float, @Lon float, @Dist float, @minLat float, @minLon float, @maxLat float, @maxLon float
            AS
            BEGIN
	            SET NOCOUNT ON;
	            SELECT * FROM dbo.Farms WHERE (LatitudeRad >= @minLat AND LatitudeRad <= @maxLat) AND (LongitudeRad >= @minLon AND LongitudeRad <= @maxLon)
                AND acos(sin(@Lat) * sin(LatitudeRad) + cos(@Lat) * cos(LatitudeRad) * cos(@Lon - (LongitudeRad))) * 6371.01 <= @Dist;
            END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE GetFarmsInRadius;";
            migrationBuilder.Sql(sp);
        }
    }
}
