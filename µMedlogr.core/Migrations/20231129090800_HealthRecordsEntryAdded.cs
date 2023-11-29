using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class HealthRecordsEntryAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SymptomMeasurements_HealthRecords_HealthRecordId",
                table: "SymptomMeasurements");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "SymptomMeasurements");

            migrationBuilder.RenameColumn(
                name: "HealthRecordId",
                table: "SymptomMeasurements",
                newName: "HealthRecordEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_SymptomMeasurements_HealthRecordId",
                table: "SymptomMeasurements",
                newName: "IX_SymptomMeasurements_HealthRecordEntryId");

            migrationBuilder.CreateTable(
                name: "HealthRecordsEntrys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HealthRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecordsEntrys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthRecordsEntrys_HealthRecords_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecordsEntrys_HealthRecordId",
                table: "HealthRecordsEntrys",
                column: "HealthRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomMeasurements_HealthRecordsEntrys_HealthRecordEntryId",
                table: "SymptomMeasurements",
                column: "HealthRecordEntryId",
                principalTable: "HealthRecordsEntrys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SymptomMeasurements_HealthRecordsEntrys_HealthRecordEntryId",
                table: "SymptomMeasurements");

            migrationBuilder.DropTable(
                name: "HealthRecordsEntrys");

            migrationBuilder.RenameColumn(
                name: "HealthRecordEntryId",
                table: "SymptomMeasurements",
                newName: "HealthRecordId");

            migrationBuilder.RenameIndex(
                name: "IX_SymptomMeasurements_HealthRecordEntryId",
                table: "SymptomMeasurements",
                newName: "IX_SymptomMeasurements_HealthRecordId");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "SymptomMeasurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomMeasurements_HealthRecords_HealthRecordId",
                table: "SymptomMeasurements",
                column: "HealthRecordId",
                principalTable: "HealthRecords",
                principalColumn: "Id");
        }
    }
}
