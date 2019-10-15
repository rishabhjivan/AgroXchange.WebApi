using AgroXchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroXchange.Data
{
    //Tutorial from https://www.c-sharpcorner.com/article/entity-framework-core-a-code-first-approach/
    public class EFDataContext : DbContext
    {
        public DbSet<Farm> Farms { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.EmailId)
                .IsUnique();
            builder.Entity<Farm>()
                .HasIndex(f => f.Latitude);
            builder.Entity<Farm>()
                .HasIndex(f => f.LatitudeRad);
            builder.Entity<Farm>()
                .HasIndex(f => f.Longitude);
            builder.Entity<Farm>()
                .HasIndex(f => f.LongitudeRad);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=agroxchangestaging.database.windows.net;Initial Catalog=AgroXchangeDB;User ID=agroadmin;Password=Admin@123;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MRVVT46\SQLEXPRESS2014;Initial Catalog=AgroXchangeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public IEnumerable<Farm> GetFarmsWithinRadius(double latRad, double lonRad, double radius, double minLatRad, double maxLatRad, double minLonRad, double maxLonRad)
        {
            try
            {
                // Processing.  
                //string sqlQuery = $"EXEC [dbo].[GetFarmsInRadius] {latRad}, {lonRad}, {radius}, {minLatRad}, {maxLatRad}, {minLonRad}, {maxLonRad}";
                string sqlQuery = "EXEC [dbo].[GetFarmsInRadius] @latRad, @lonRad, @radius, @minLatRad, @minLonRad, @maxLatRad, @maxLonRad";

                return this.Set<Farm>().FromSql(sqlQuery
                    , new SqlParameter("@latRad", latRad)
                    , new SqlParameter("@lonRad", lonRad)
                    , new SqlParameter("@radius", radius)
                    , new SqlParameter("@minLatRad", minLatRad)
                    , new SqlParameter("@minLonRad", minLonRad)
                    , new SqlParameter("@maxLatRad", maxLatRad)
                    , new SqlParameter("@maxLonRad", maxLonRad)).AsEnumerable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
