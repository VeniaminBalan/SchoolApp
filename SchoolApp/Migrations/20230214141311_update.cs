using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolApp.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "SubjectModelTestModel");

            migrationBuilder.DropColumn(
                name: "TestDate",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tests",
                newName: "Subjectid");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Grade",
                table: "Tests",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Grade",
                table: "Assignments",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_Subjectid",
                table: "Tests",
                column: "Subjectid");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Subjects_Subjectid",
                table: "Tests",
                column: "Subjectid",
                principalTable: "Subjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Subjects_Subjectid",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_Subjectid",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Subjectid",
                table: "Tests",
                newName: "Title");

            migrationBuilder.AddColumn<DateTime>(
                name: "TestDate",
                table: "Tests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    SubjectModelid = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<double>(type: "double precision", nullable: false)
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
                name: "IX_Grade_SubjectModelid",
                table: "Grade",
                column: "SubjectModelid");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectModelTestModel_Testsid",
                table: "SubjectModelTestModel",
                column: "Testsid");
        }
    }
}
