﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolProject.Infrastructure.Context;

#nullable disable

namespace SchoolProject.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241108160946_AddInstructor_Supervisor_Relationship")]
    partial class AddInstructor_Supervisor_Relationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SchoolProject.Domain.Entities.Department", b =>
                {
                    b.Property<int>("DID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DID"));

                    b.Property<string>("DName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("InsManager")
                        .HasColumnType("int");

                    b.HasKey("DID");

                    b.HasIndex("InsManager")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.DepartmentSubject", b =>
                {
                    b.Property<int>("DID")
                        .HasColumnType("int");

                    b.Property<int>("SubID")
                        .HasColumnType("int");

                    b.HasKey("DID", "SubID");

                    b.HasIndex("SubID");

                    b.ToTable("DepartmentSubjects");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Ins_Subject", b =>
                {
                    b.Property<int>("InsId")
                        .HasColumnType("int");

                    b.Property<int>("SubID")
                        .HasColumnType("int");

                    b.HasKey("InsId", "SubID");

                    b.HasIndex("SubID");

                    b.ToTable("Ins_Subject");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Instructor", b =>
                {
                    b.Property<int>("InsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DID")
                        .HasColumnType("int");

                    b.Property<string>("InstructorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SupervisorId")
                        .HasColumnType("int");

                    b.HasKey("InsId");

                    b.HasIndex("DID");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Student", b =>
                {
                    b.Property<int>("StudID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudID"));

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("DID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Phone")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("StudID");

                    b.HasIndex("DID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.StudentSubject", b =>
                {
                    b.Property<int>("StudID")
                        .HasColumnType("int");

                    b.Property<int>("SubID")
                        .HasColumnType("int");

                    b.HasKey("StudID", "SubID");

                    b.HasIndex("SubID");

                    b.ToTable("StudentSubjects");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Subjects", b =>
                {
                    b.Property<int>("SubID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubID"));

                    b.Property<DateTime>("Period")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("SubID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Department", b =>
                {
                    b.HasOne("SchoolProject.Domain.Entities.Instructor", "Instructor")
                        .WithOne("DepartmentManager")
                        .HasForeignKey("SchoolProject.Domain.Entities.Department", "InsManager")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.DepartmentSubject", b =>
                {
                    b.HasOne("SchoolProject.Domain.Entities.Department", "Department")
                        .WithMany("DepartmentSubjects")
                        .HasForeignKey("DID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolProject.Domain.Entities.Subjects", "Subjects")
                        .WithMany("DepartmentSubjects")
                        .HasForeignKey("SubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Ins_Subject", b =>
                {
                    b.HasOne("SchoolProject.Domain.Entities.Instructor", "Instructor")
                        .WithMany("Ins_Subjects")
                        .HasForeignKey("InsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolProject.Domain.Entities.Subjects", "Subjects")
                        .WithMany("Ins_Subjects")
                        .HasForeignKey("SubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Instructor", b =>
                {
                    b.HasOne("SchoolProject.Domain.Entities.Department", "Department")
                        .WithMany("Instructors")
                        .HasForeignKey("DID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolProject.Domain.Entities.Instructor", "Supervisor")
                        .WithMany("Instructors")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Student", b =>
                {
                    b.HasOne("SchoolProject.Domain.Entities.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DID");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.StudentSubject", b =>
                {
                    b.HasOne("SchoolProject.Domain.Entities.Student", "Student")
                        .WithMany("StudentSubject")
                        .HasForeignKey("StudID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolProject.Domain.Entities.Subjects", "Subject")
                        .WithMany("StudentsSubjects")
                        .HasForeignKey("SubID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Department", b =>
                {
                    b.Navigation("DepartmentSubjects");

                    b.Navigation("Instructors");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Instructor", b =>
                {
                    b.Navigation("DepartmentManager")
                        .IsRequired();

                    b.Navigation("Ins_Subjects");

                    b.Navigation("Instructors");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Student", b =>
                {
                    b.Navigation("StudentSubject");
                });

            modelBuilder.Entity("SchoolProject.Domain.Entities.Subjects", b =>
                {
                    b.Navigation("DepartmentSubjects");

                    b.Navigation("Ins_Subjects");

                    b.Navigation("StudentsSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}
