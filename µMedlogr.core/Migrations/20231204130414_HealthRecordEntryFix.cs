using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class HealthRecordEntryFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSymptomWasChecked",
                table: "SymptomMeasurements");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeSymptomWasChecked",
                table: "HealthRecordsEntrys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSymptomWasChecked",
                table: "HealthRecordsEntrys");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeSymptomWasChecked",
                table: "SymptomMeasurements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
