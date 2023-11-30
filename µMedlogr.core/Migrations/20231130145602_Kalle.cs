using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class Kalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_People_PersonId",
                table: "HealthRecords");

            migrationBuilder.DropIndex(
                name: "IX_HealthRecords_PersonId",
                table: "HealthRecords");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "HealthRecords",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Allergies", "DateOfBirth", "NickName", "WeightInKg" },
                values: new object[] { 1, "[]", new DateOnly(1, 1, 1), "Kalle", 47f });

            migrationBuilder.InsertData(
                table: "HealthRecords",
                columns: new[] { "Id", "PersonId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecords_PersonId",
                table: "HealthRecords",
                column: "PersonId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_People_PersonId",
                table: "HealthRecords",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_People_PersonId",
                table: "HealthRecords");

            migrationBuilder.DropIndex(
                name: "IX_HealthRecords_PersonId",
                table: "HealthRecords");

            migrationBuilder.DeleteData(
                table: "HealthRecords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "People",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "HealthRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecords_PersonId",
                table: "HealthRecords",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_People_PersonId",
                table: "HealthRecords",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");
        }
    }
}
