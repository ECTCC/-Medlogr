using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    WeightInKg = table.Column<float>(type: "real", nullable: true),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfMeasurement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Measurement = table.Column<float>(type: "real", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SymptomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubjectiveSeverity = table.Column<int>(type: "int", nullable: false),
                    HealthRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SymptomTypes_HealthRecords_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SymptomMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymptomId = table.Column<int>(type: "int", nullable: false),
                    TimeSymptomWasChecked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HealthRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SymptomMeasurements_HealthRecords_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SymptomMeasurements_SymptomTypes_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "SymptomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymptomMeasurements_HealthRecordId",
                table: "SymptomMeasurements",
                column: "HealthRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomMeasurements_SymptomId",
                table: "SymptomMeasurements",
                column: "SymptomId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomTypes_HealthRecordId",
                table: "SymptomTypes",
                column: "HealthRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "SymptomMeasurements");

            migrationBuilder.DropTable(
                name: "TemperatureDatas");

            migrationBuilder.DropTable(
                name: "SymptomTypes");

            migrationBuilder.DropTable(
                name: "HealthRecords");
        }
    }
}
