using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.Migrations
{
    /// <inheritdoc />
    public partial class updatedtableassignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Assignments");

            migrationBuilder.AddColumn<string>(
                name: "Subjectid",
                table: "Assignments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ProffesorMail = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    SubjectModelid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.id);
                    table.ForeignKey(
                        name: "FK_Grade_Subjects_SubjectModelid",
                        column: x => x.SubjectModelid,
                        principalTable: "Subjects",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SubjectModelTestModel",
                columns: table => new
                {
                    Subjectsid = table.Column<string>(type: "text", nullable: false),
                    Testsid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectModelTestModel", x => new { x.Subjectsid, x.Testsid });
                    table.ForeignKey(
                        name: "FK_SubjectModelTestModel_Subjects_Subjectsid",
                        column: x => x.Subjectsid,
                        principalTable: "Subjects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectModelTestModel_Tests_Testsid",
                        column: x => x.Testsid,
                        principalTable: "Tests",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_Subjectid",
                table: "Assignments",
                column: "Subjectid");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_SubjectModelid",
                table: "Grade",
                column: "SubjectModelid");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectModelTestModel_Testsid",
                table: "SubjectModelTestModel",
                column: "Testsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Subjects_Subjectid",
                table: "Assignments",
                column: "Subjectid",
                principalTable: "Subjects",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Subjects_Subjectid",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "SubjectModelTestModel");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_Subjectid",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Subjectid",
                table: "Assignments");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Assignments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
