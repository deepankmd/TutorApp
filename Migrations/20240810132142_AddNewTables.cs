using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TutoringPreferencesID",
                table: "TutorSubject",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TutoringPreferencesID",
                table: "TutorLocations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TutoringPreferencesID",
                table: "TutorLevel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationAndQualificationsID",
                table: "TutorGradeValues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationAndQualificationsID",
                table: "TutorGradesSubject",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationAndQualificationsID",
                table: "TutorGrade",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountInfo",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordConfirm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NRIC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AgencyManagers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyManagers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EducationAndQualifications",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TutorCategories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicQualifications = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAndQualifications", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProfilesAndExperinces",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadOfCertificates = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilesAndExperinces", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TutoringPreferences",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecialNeedsExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialNeedsExperienceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredLocations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedSubjects = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutoringPreferences", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubject_TutoringPreferencesID",
                table: "TutorSubject",
                column: "TutoringPreferencesID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorLocations_TutoringPreferencesID",
                table: "TutorLocations",
                column: "TutoringPreferencesID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorLevel_TutoringPreferencesID",
                table: "TutorLevel",
                column: "TutoringPreferencesID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorGradeValues_EducationAndQualificationsID",
                table: "TutorGradeValues",
                column: "EducationAndQualificationsID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorGradesSubject_EducationAndQualificationsID",
                table: "TutorGradesSubject",
                column: "EducationAndQualificationsID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorGrade_EducationAndQualificationsID",
                table: "TutorGrade",
                column: "EducationAndQualificationsID");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorGrade_EducationAndQualifications_EducationAndQualificationsID",
                table: "TutorGrade",
                column: "EducationAndQualificationsID",
                principalTable: "EducationAndQualifications",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorGradesSubject_EducationAndQualifications_EducationAndQualificationsID",
                table: "TutorGradesSubject",
                column: "EducationAndQualificationsID",
                principalTable: "EducationAndQualifications",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorGradeValues_EducationAndQualifications_EducationAndQualificationsID",
                table: "TutorGradeValues",
                column: "EducationAndQualificationsID",
                principalTable: "EducationAndQualifications",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorLevel_TutoringPreferences_TutoringPreferencesID",
                table: "TutorLevel",
                column: "TutoringPreferencesID",
                principalTable: "TutoringPreferences",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorLocations_TutoringPreferences_TutoringPreferencesID",
                table: "TutorLocations",
                column: "TutoringPreferencesID",
                principalTable: "TutoringPreferences",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TutorSubject_TutoringPreferences_TutoringPreferencesID",
                table: "TutorSubject",
                column: "TutoringPreferencesID",
                principalTable: "TutoringPreferences",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TutorGrade_EducationAndQualifications_EducationAndQualificationsID",
                table: "TutorGrade");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorGradesSubject_EducationAndQualifications_EducationAndQualificationsID",
                table: "TutorGradesSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorGradeValues_EducationAndQualifications_EducationAndQualificationsID",
                table: "TutorGradeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorLevel_TutoringPreferences_TutoringPreferencesID",
                table: "TutorLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorLocations_TutoringPreferences_TutoringPreferencesID",
                table: "TutorLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_TutorSubject_TutoringPreferences_TutoringPreferencesID",
                table: "TutorSubject");

            migrationBuilder.DropTable(
                name: "AccountInfo");

            migrationBuilder.DropTable(
                name: "AgencyManagers");

            migrationBuilder.DropTable(
                name: "EducationAndQualifications");

            migrationBuilder.DropTable(
                name: "ProfilesAndExperinces");

            migrationBuilder.DropTable(
                name: "TutoringPreferences");

            migrationBuilder.DropIndex(
                name: "IX_TutorSubject_TutoringPreferencesID",
                table: "TutorSubject");

            migrationBuilder.DropIndex(
                name: "IX_TutorLocations_TutoringPreferencesID",
                table: "TutorLocations");

            migrationBuilder.DropIndex(
                name: "IX_TutorLevel_TutoringPreferencesID",
                table: "TutorLevel");

            migrationBuilder.DropIndex(
                name: "IX_TutorGradeValues_EducationAndQualificationsID",
                table: "TutorGradeValues");

            migrationBuilder.DropIndex(
                name: "IX_TutorGradesSubject_EducationAndQualificationsID",
                table: "TutorGradesSubject");

            migrationBuilder.DropIndex(
                name: "IX_TutorGrade_EducationAndQualificationsID",
                table: "TutorGrade");

            migrationBuilder.DropColumn(
                name: "TutoringPreferencesID",
                table: "TutorSubject");

            migrationBuilder.DropColumn(
                name: "TutoringPreferencesID",
                table: "TutorLocations");

            migrationBuilder.DropColumn(
                name: "TutoringPreferencesID",
                table: "TutorLevel");

            migrationBuilder.DropColumn(
                name: "EducationAndQualificationsID",
                table: "TutorGradeValues");

            migrationBuilder.DropColumn(
                name: "EducationAndQualificationsID",
                table: "TutorGradesSubject");

            migrationBuilder.DropColumn(
                name: "EducationAndQualificationsID",
                table: "TutorGrade");
        }
    }
}
