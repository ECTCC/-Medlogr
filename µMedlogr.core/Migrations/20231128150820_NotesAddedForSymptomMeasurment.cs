using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class NotesAddedForSymptomMeasurment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "SymptomMeasurements",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "SymptomMeasurements");
        }
    }
}
