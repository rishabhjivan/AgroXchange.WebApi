﻿// <auto-generated />
using System;
using AgroXchange.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgroXchange.Data.Migrations
{
    [DbContext(typeof(EFDataContext))]
    [Migration("20190918082936_sql-migration-v1.2")]
    partial class sqlmigrationv12
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AgroXchange.Data.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles","dbo");
                });

            modelBuilder.Entity("AgroXchange.Data.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activated")
                        .HasColumnType("bit");

                    b.Property<string>("ActivationKey")
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockedOut")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordResetKey")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId");

                    b.HasIndex("EmailId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users","dbo");
                });

            modelBuilder.Entity("AgroXchange.Data.Models.User", b =>
                {
                    b.HasOne("AgroXchange.Data.Models.Role", "UserRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
