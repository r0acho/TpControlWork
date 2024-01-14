﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TpControlWork.DataAccess;

#nullable disable

namespace TpControlWork.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TpControlWork.DataAccess.Entities.Earning", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("BonusAmount")
                        .HasColumnType("numeric");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("FkEmployeeId")
                        .HasColumnType("integer");

                    b.Property<int?>("OvertimeHours")
                        .HasColumnType("integer");

                    b.Property<decimal?>("OvertimeRate")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("FkEmployeeId");

                    b.ToTable("Earnings");
                });

            modelBuilder.Entity("TpControlWork.DataAccess.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FkEmployeeTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("FkPaymentTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FkEmployeeTypeId");

                    b.HasIndex("FkPaymentTypeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TpControlWork.DataAccess.Entities.EmployeeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmployeeTypes");
                });

            modelBuilder.Entity("TpControlWork.DataAccess.Entities.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("HourlyRate")
                        .HasColumnType("numeric");

                    b.Property<int?>("HoursWorked")
                        .HasColumnType("integer");

                    b.Property<decimal?>("MonthlySalary")
                        .HasColumnType("numeric");

                    b.Property<int?>("NumberOfPieces")
                        .HasColumnType("integer");

                    b.Property<decimal?>("RatePerPiece")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("TpControlWork.DataAccess.Entities.Earning", b =>
                {
                    b.HasOne("TpControlWork.DataAccess.Entities.Employee", null)
                        .WithMany("Earnings")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("TpControlWork.DataAccess.Entities.Employee", "FkEmployee")
                        .WithMany()
                        .HasForeignKey("FkEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FkEmployee");
                });

            modelBuilder.Entity("TpControlWork.DataAccess.Entities.Employee", b =>
                {
                    b.HasOne("TpControlWork.DataAccess.Entities.EmployeeType", "FkEmployeeType")
                        .WithMany()
                        .HasForeignKey("FkEmployeeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TpControlWork.DataAccess.Entities.PaymentType", "FkPaymentType")
                        .WithMany()
                        .HasForeignKey("FkPaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FkEmployeeType");

                    b.Navigation("FkPaymentType");
                });

            modelBuilder.Entity("TpControlWork.DataAccess.Entities.Employee", b =>
                {
                    b.Navigation("Earnings");
                });
#pragma warning restore 612, 618
        }
    }
}
