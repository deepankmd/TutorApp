﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TutorAppAPI.Services;

#nullable disable

namespace TutorAppAPI.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20240810131245_AddTutorLocationsTable")]
    partial class AddTutorLocationsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TutorAppAPI.Models.Admins", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("TutorAppAPI.Models.Assignment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AppliedCount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssignmentStatus")
                        .HasColumnType("int");

                    b.Property<string>("AvailableTimings")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescriptionOfNeeds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FrequencyOfLessons")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<string>("LengthOfCommitment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredTutorGender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectsToBeTutored")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TuitionStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TutorAvailability")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YourTuitionBudget")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("TutorAppAPI.Models.AssignmentApplied", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AllAvailableTimings")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssignmentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhenCanYouStartEarliest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WhyShouldYouBeChosen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YourRate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AssignmentApplied");
                });

            modelBuilder.Entity("TutorAppAPI.Models.EducationLevel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("EducationLevel");
                });

            modelBuilder.Entity("TutorAppAPI.Models.Notification", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NotificationType")
                        .HasColumnType("int");

                    b.Property<int>("ScreenType")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("TutorAppAPI.Models.ParentDetails", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Mobile")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordConfirm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RelationShip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ParentDetails");
                });

            modelBuilder.Entity("TutorAppAPI.Models.TutorCategory", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TutorCategory");
                });

            modelBuilder.Entity("TutorAppAPI.Models.TutorGrade", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TutorGrade");
                });

            modelBuilder.Entity("TutorAppAPI.Models.TutorGradeValues", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorGradeSubjectID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TutorGradeValues");
                });

            modelBuilder.Entity("TutorAppAPI.Models.TutorGradesSubject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GradeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TutorGradesSubject");
                });

            modelBuilder.Entity("TutorAppAPI.Models.TutorLevel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LevelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LevelShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TutorLevel");
                });

            modelBuilder.Entity("TutorAppAPI.Models.TutorLocations", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TutorLocations");
                });

            modelBuilder.Entity("TutorAppAPI.Models.TutorSchools", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TutorSchools");
                });

            modelBuilder.Entity("TutorAppAPI.Models.TutorSubject", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorLevelID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TutorLevelID1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("TutorLevelID1");

                    b.ToTable("TutorSubject");
                });

            modelBuilder.Entity("TutorAppAPI.Models.Tutors", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CertFileID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CertFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CertFileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Citizenship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateofBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EducationLevelSelected")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GradesAndQualifications")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("NRIC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordConfirm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredLocations")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredSelectedSubjects")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreferredSpecialNeedsExperience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Race")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TextNeeds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorCategorySelected")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorSchools")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TutorTo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Tutors");
                });

            modelBuilder.Entity("TutorAppAPI.Models.TutorSubject", b =>
                {
                    b.HasOne("TutorAppAPI.Models.TutorLevel", "TutorLevel")
                        .WithMany()
                        .HasForeignKey("TutorLevelID1");

                    b.Navigation("TutorLevel");
                });
#pragma warning restore 612, 618
        }
    }
}
