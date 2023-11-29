using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                name: "SymptomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomTypes", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    WeightInKg = table.Column<float>(type: "real", nullable: true),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_HealthRecords_Id",
                        column: x => x.Id,
                        principalTable: "HealthRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfMeasurement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Measurement = table.Column<float>(type: "real", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HealthRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureDatas_HealthRecords_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateTable(
                name: "SymptomMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymptomId = table.Column<int>(type: "int", nullable: false),
                    SubjectiveSeverity = table.Column<int>(type: "int", nullable: false),
                    TimeSymptomWasChecked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HealthRecordEntryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SymptomMeasurements_HealthRecordsEntrys_HealthRecordEntryId",
                        column: x => x.HealthRecordEntryId,
                        principalTable: "HealthRecordsEntrys",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SymptomMeasurements_SymptomTypes_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "SymptomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_People_MeId",
                        column: x => x.MeId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserPerson",
                columns: table => new
                {
                    CareGiversId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PeopleInCareOfId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserPerson", x => new { x.CareGiversId, x.PeopleInCareOfId });
                    table.ForeignKey(
                        name: "FK_AppUserPerson_AppUsers_CareGiversId",
                        column: x => x.CareGiversId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserPerson_People_PeopleInCareOfId",
                        column: x => x.PeopleInCareOfId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserPerson_PeopleInCareOfId",
                table: "AppUserPerson",
                column: "PeopleInCareOfId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_MeId",
                table: "AppUsers",
                column: "MeId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecordsEntrys_HealthRecordId",
                table: "HealthRecordsEntrys",
                column: "HealthRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecordSymptomType_RecordsId",
                table: "HealthRecordSymptomType",
                column: "RecordsId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomMeasurements_HealthRecordEntryId",
                table: "SymptomMeasurements",
                column: "HealthRecordEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_SymptomMeasurements_SymptomId",
                table: "SymptomMeasurements",
                column: "SymptomId");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureDatas_HealthRecordId",
                table: "TemperatureDatas",
                column: "HealthRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserPerson");

            migrationBuilder.DropTable(
                name: "HealthRecordSymptomType");

            migrationBuilder.DropTable(
                name: "SymptomMeasurements");

            migrationBuilder.DropTable(
                name: "TemperatureDatas");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "HealthRecordsEntrys");

            migrationBuilder.DropTable(
                name: "SymptomTypes");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "HealthRecords");
        }
    }
}
