using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class PublicProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HealthRecordId",
                table: "TemperatureDatas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HealthRecordId",
                table: "SymptomTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SymptomTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HealthRecordId",
                table: "SymptomMeasurements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectiveSeverity",
                table: "SymptomMeasurements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SymptomId",
                table: "SymptomMeasurements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeSymptomWasChecked",
                table: "SymptomMeasurements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "People",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "People",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "WeightInKg",
                table: "People",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "HealthRecords",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureDatas_HealthRecordId",
                table: "TemperatureDatas",
                column: "HealthRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomTypes_HealthRecordId",
                table: "SymptomTypes",
                column: "HealthRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomMeasurements_HealthRecordId",
                table: "SymptomMeasurements",
                column: "HealthRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomMeasurements_SymptomId",
                table: "SymptomMeasurements",
                column: "SymptomId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecords_RecordId",
                table: "HealthRecords",
                column: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_People_RecordId",
                table: "HealthRecords",
                column: "RecordId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomMeasurements_HealthRecords_HealthRecordId",
                table: "SymptomMeasurements",
                column: "HealthRecordId",
                principalTable: "HealthRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomMeasurements_SymptomTypes_SymptomId",
                table: "SymptomMeasurements",
                column: "SymptomId",
                principalTable: "SymptomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SymptomTypes_HealthRecords_HealthRecordId",
                table: "SymptomTypes",
                column: "HealthRecordId",
                principalTable: "HealthRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TemperatureDatas_HealthRecords_HealthRecordId",
                table: "TemperatureDatas",
                column: "HealthRecordId",
                principalTable: "HealthRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_People_RecordId",
                table: "HealthRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_SymptomMeasurements_HealthRecords_HealthRecordId",
                table: "SymptomMeasurements");

            migrationBuilder.DropForeignKey(
                name: "FK_SymptomMeasurements_SymptomTypes_SymptomId",
                table: "SymptomMeasurements");

            migrationBuilder.DropForeignKey(
                name: "FK_SymptomTypes_HealthRecords_HealthRecordId",
                table: "SymptomTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TemperatureDatas_HealthRecords_HealthRecordId",
                table: "TemperatureDatas");

            migrationBuilder.DropIndex(
                name: "IX_TemperatureDatas_HealthRecordId",
                table: "TemperatureDatas");

            migrationBuilder.DropIndex(
                name: "IX_SymptomTypes_HealthRecordId",
                table: "SymptomTypes");

            migrationBuilder.DropIndex(
                name: "IX_SymptomMeasurements_HealthRecordId",
                table: "SymptomMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_SymptomMeasurements_SymptomId",
                table: "SymptomMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_HealthRecords_RecordId",
                table: "HealthRecords");

            migrationBuilder.DropColumn(
                name: "HealthRecordId",
                table: "TemperatureDatas");

            migrationBuilder.DropColumn(
                name: "HealthRecordId",
                table: "SymptomTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SymptomTypes");

            migrationBuilder.DropColumn(
                name: "HealthRecordId",
                table: "SymptomMeasurements");

            migrationBuilder.DropColumn(
                name: "SubjectiveSeverity",
                table: "SymptomMeasurements");

            migrationBuilder.DropColumn(
                name: "SymptomId",
                table: "SymptomMeasurements");

            migrationBuilder.DropColumn(
                name: "TimeSymptomWasChecked",
                table: "SymptomMeasurements");

            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "People");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "People");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "People");

            migrationBuilder.DropColumn(
                name: "WeightInKg",
                table: "People");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "HealthRecords");
        }
    }
}
