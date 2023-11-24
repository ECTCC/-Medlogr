using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class SymptomChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectiveSeverity",
                table: "SymptomTypes");

            migrationBuilder.AddColumn<int>(
                name: "SubjectiveSeverity",
                table: "SymptomMeasurements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectiveSeverity",
                table: "SymptomMeasurements");

            migrationBuilder.AddColumn<int>(
                name: "SubjectiveSeverity",
                table: "SymptomTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
