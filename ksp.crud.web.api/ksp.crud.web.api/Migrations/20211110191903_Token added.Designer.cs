﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ksp.crud.web.api.DataModel;

namespace ksp.crud.web.api.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211110191903_Token added")]
    partial class Tokenadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ksp.crud.web.api.DataModel.Beneficiaries", b =>
                {
                    b.Property<Guid>("BeneficiaryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateBirth")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstLastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Relationship")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("SecondLastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("BeneficiaryId");

                    b.HasIndex("EmployId");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("ksp.crud.web.api.DataModel.Employees", b =>
                {
                    b.Property<Guid>("EmployId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstLastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("HiringDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Salary")
                        .HasColumnType("float");

                    b.Property<string>("SecondLastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Status")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("EmployId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ksp.crud.web.api.DataModel.SystemUsers", b =>
                {
                    b.Property<Guid>("SystemuserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SystemuserId");

                    b.HasIndex("EmployId");

                    b.ToTable("SystemUsers");
                });

            modelBuilder.Entity("ksp.crud.web.api.DataModel.Token", b =>
                {
                    b.Property<Guid>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IsExpirated")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TokenId");

                    b.ToTable("Token");
                });

            modelBuilder.Entity("ksp.crud.web.api.DataModel.Beneficiaries", b =>
                {
                    b.HasOne("ksp.crud.web.api.DataModel.Employees", "Employ")
                        .WithMany()
                        .HasForeignKey("EmployId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employ");
                });

            modelBuilder.Entity("ksp.crud.web.api.DataModel.SystemUsers", b =>
                {
                    b.HasOne("ksp.crud.web.api.DataModel.Employees", "Employ")
                        .WithMany()
                        .HasForeignKey("EmployId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employ");
                });
#pragma warning restore 612, 618
        }
    }
}
