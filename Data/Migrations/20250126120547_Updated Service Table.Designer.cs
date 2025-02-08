﻿// <auto-generated />
using System;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250126120547_Updated Service Table")]
    partial class UpdatedServiceTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data.Entities.CostumerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CostumerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Costumers");
                });

            modelBuilder.Entity("Data.Entities.ProjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CostumerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("ProjectManagerId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("StatusTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("CostumerId");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("StatusTypeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Data.Entities.ProjectManagerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("ProjectManagers");
                });

            modelBuilder.Entity("Data.Entities.ServiceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Data.Entities.StatusTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("StatusTypes");
                });

            modelBuilder.Entity("Data.Entities.ProjectEntity", b =>
                {
                    b.HasOne("Data.Entities.CostumerEntity", "Costumer")
                        .WithMany("Projects")
                        .HasForeignKey("CostumerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.ProjectManagerEntity", "ProjectManager")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.ServiceEntity", "Service")
                        .WithMany("Projects")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.StatusTypeEntity", "StatusType")
                        .WithMany("Projects")
                        .HasForeignKey("StatusTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Costumer");

                    b.Navigation("ProjectManager");

                    b.Navigation("Service");

                    b.Navigation("StatusType");
                });

            modelBuilder.Entity("Data.Entities.CostumerEntity", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Data.Entities.ProjectManagerEntity", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Data.Entities.ServiceEntity", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Data.Entities.StatusTypeEntity", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
