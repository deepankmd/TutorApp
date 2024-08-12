using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTutorLocationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectsToBeTutored = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FrequencyOfLessons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YourTuitionBudget = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TuitionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LengthOfCommitment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableTimings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredTutorGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionOfNeeds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorAvailability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    AssignmentStatus = table.Column<int>(type: "int", nullable: false),
                    AppliedCount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentApplied",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TutorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignmentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YourRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllAvailableTimings = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenCanYouStartEarliest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhyShouldYouBeChosen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentApplied", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScreenType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParentDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<long>(type: "bigint", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordConfirm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationShip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutorCategory",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutorGrade",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorGrade", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutorGradesSubject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorGradesSubject", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutorGradeValues",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorGradeSubjectID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorGradeValues", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutorLevel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorLevel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutorLocations",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Zone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorLocations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tutors",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordConfirm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NRIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredSelectedSubjects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextNeeds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredSpecialNeedsExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredLocations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationLevelSelected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorCategorySelected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradesAndQualifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorSchools = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertFileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertFileID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutorSchools",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSchools", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutorSubject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorLevelID1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TutorLevelID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSubject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TutorSubject_TutorLevel_TutorLevelID1",
                        column: x => x.TutorLevelID1,
                        principalTable: "TutorLevel",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubject_TutorLevelID1",
                table: "TutorSubject",
                column: "TutorLevelID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "AssignmentApplied");

            migrationBuilder.DropTable(
                name: "EducationLevel");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ParentDetails");

            migrationBuilder.DropTable(
                name: "TutorCategory");

            migrationBuilder.DropTable(
                name: "TutorGrade");

            migrationBuilder.DropTable(
                name: "TutorGradesSubject");

            migrationBuilder.DropTable(
                name: "TutorGradeValues");

            migrationBuilder.DropTable(
                name: "TutorLocations");

            migrationBuilder.DropTable(
                name: "Tutors");

            migrationBuilder.DropTable(
                name: "TutorSchools");

            migrationBuilder.DropTable(
                name: "TutorSubject");

            migrationBuilder.DropTable(
                name: "TutorLevel");
        }
    }
}
