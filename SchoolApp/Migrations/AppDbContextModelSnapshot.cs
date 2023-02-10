﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SchoolApp.Database;

#nullable disable

namespace SchoolApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SchoolApp.Base.Grade", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<string>("SubjectModelid")
                        .HasColumnType("text");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.HasKey("id");

                    b.HasIndex("SubjectModelid");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("SchoolApp.Features.Assignments.Models.AssignmentModel", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Subjectid")
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.HasIndex("Subjectid");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("SchoolApp.Features.Assignments.Models.SubjectModel", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProffesorMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SchoolApp.Features.Assignments.Models.TestModel", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TestDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("SubjectModelTestModel", b =>
                {
                    b.Property<string>("Subjectsid")
                        .HasColumnType("text");

                    b.Property<string>("Testsid")
                        .HasColumnType("text");

                    b.HasKey("Subjectsid", "Testsid");

                    b.HasIndex("Testsid");

                    b.ToTable("SubjectModelTestModel");
                });

            modelBuilder.Entity("SchoolApp.Base.Grade", b =>
                {
                    b.HasOne("SchoolApp.Features.Assignments.Models.SubjectModel", null)
                        .WithMany("Grades")
                        .HasForeignKey("SubjectModelid");
                });

            modelBuilder.Entity("SchoolApp.Features.Assignments.Models.AssignmentModel", b =>
                {
                    b.HasOne("SchoolApp.Features.Assignments.Models.SubjectModel", "Subject")
                        .WithMany("Assignment")
                        .HasForeignKey("Subjectid");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SubjectModelTestModel", b =>
                {
                    b.HasOne("SchoolApp.Features.Assignments.Models.SubjectModel", null)
                        .WithMany()
                        .HasForeignKey("Subjectsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolApp.Features.Assignments.Models.TestModel", null)
                        .WithMany()
                        .HasForeignKey("Testsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolApp.Features.Assignments.Models.SubjectModel", b =>
                {
                    b.Navigation("Assignment");

                    b.Navigation("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}
