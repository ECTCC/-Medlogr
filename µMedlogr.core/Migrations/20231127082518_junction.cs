using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class junction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SymptomTypes_HealthRecords_HealthRecordId",
                table: "SymptomTypes");

            migrationBuilder.DropIndex(
                name: "IX_SymptomTypes_HealthRecordId",
                table: "SymptomTypes");

            migrationBuilder.DropColumn(
                name: "HealthRecordId",
                table: "SymptomTypes");

            migrationBuilder.CreateTable(
                name: "HealthRecordSymptomType",
                columns: table => new
                {
                    CurrentSymptomsId = table.Column<int>(type: "int", nullable: false),
                    RecordsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecordSymptomType", x => new { x.CurrentSymptomsId, x.RecordsId });
                    table.ForeignKey(
                        name: "FK_HealthRecordSymptomType_HealthRecords_RecordsId",
                        column: x => x.RecordsId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthRecordSymptomType_SymptomTypes_CurrentSymptomsId",
                        column: x => x.CurrentSymptomsId,
                        principalTable: "SymptomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecordSymptomType_RecordsId",
                table: "HealthRecordSymptomType",
                column: "RecordsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthRecordSymptomType");

            migrationBuilder.AddColumn<int>(
                name: "HealthRecordId",
                table: "SymptomTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SymptomTypes_HealthRecordId",
                table: "SymptomTypes",
                column: "HealthRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomTypes_HealthRecords_HealthRecordId",
                table: "SymptomTypes",
                column: "HealthRecordId",
                principalTable: "HealthRecords",
                principalColumn: "Id");
        }
    }
}
