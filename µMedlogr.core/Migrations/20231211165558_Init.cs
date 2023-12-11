using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace µMedlogr.core.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
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
                name: "SymptomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasureInterval = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MeId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_People_MeId",
                        column: x => x.MeId,
                        principalTable: "People",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HealthRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthRecords_People_PersonId",
                        column: x => x.PersonId,
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
                        name: "FK_AppUserPerson_AspNetUsers_CareGiversId",
                        column: x => x.CareGiversId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserPerson_People_PeopleInCareOfId",
                        column: x => x.PeopleInCareOfId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HealthRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_HealthRecords_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthRecordsEntrys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeSymptomWasChecked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HealthRecordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthRecordsEntrys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthRecordsEntrys_HealthRecords_HealthRecordId",
                        column: x => x.HealthRecordId,
                        principalTable: "HealthRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveSubstance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Form = table.Column<int>(type: "int", nullable: false),
                    Effects = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drugs_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SymptomMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SymptomId = table.Column<int>(type: "int", nullable: false),
                    SubjectiveSeverity = table.Column<int>(type: "int", nullable: false),
                    HealthRecordEntryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymptomMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SymptomMeasurements_HealthRecordsEntrys_HealthRecordEntryId",
                        column: x => x.HealthRecordEntryId,
                        principalTable: "HealthRecordsEntrys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SymptomMeasurements_SymptomTypes_SymptomId",
                        column: x => x.SymptomId,
                        principalTable: "SymptomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "MeId", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "37290e33-eedf-4d2a-ae40-544c5da72297", 0, "fa38e27a-a23f-4977-a902-0e199d0e79da", "Test@Test.com", true, false, null, null, "TEST@TEST.COM", "TEST", "AQAAAAIAAYagAAAAEHuFGihfSqzz4qFhPBf/apjBQy4yILVsnNJku4i5JvYbtOLHz2gK3+nzBoNUloRLoA==", null, false, "1514c361-7826-40cc-88a3-82c57b159dbe", false, "Test" });

            migrationBuilder.InsertData(
                table: "Drugs",
                columns: new[] { "Id", "ActiveSubstance", "Effects", "EventId", "Form", "Name" },
                values: new object[,]
                {
                    { 1, "Kokain", "[4]", null, 2, "Ipren" },
                    { 2, "MDMA", "[4,3,1]", null, 2, "Treo" },
                    { 3, "Secret", "[0]", null, 2, "Viagra" },
                    { 4, "Kokain", "[2]", null, 1, "Amoxicillin" },
                    { 5, "alpha-methylphenethylamine", "[4,0]", null, 7, "Thomas Energy Supplement" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Allergies", "DateOfBirth", "NickName", "WeightInKg" },
                values: new object[] { 1, "[]", new DateOnly(2023, 12, 11), "Totte", 55f });

            migrationBuilder.InsertData(
                table: "SymptomTypes",
                columns: new[] { "Id", "MeasureInterval", "Name" },
                values: new object[,]
                {
                    { 1, new TimeSpan(0, 0, 0, 0, 0), "Snuva" },
                    { 2, new TimeSpan(0, 0, 0, 0, 0), "Hosta" },
                    { 3, new TimeSpan(0, 0, 0, 0, 0), "Feber" },
                    { 4, new TimeSpan(0, 0, 0, 0, 0), "Huvudvärk" },
                    { 5, new TimeSpan(0, 0, 0, 0, 0), "Låg Energi" },
                    { 6, new TimeSpan(0, 0, 0, 0, 0), "Nedsatt prestationsförmåga" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserPerson_PeopleInCareOfId",
                table: "AppUserPerson",
                column: "PeopleInCareOfId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MeId",
                table: "AspNetUsers",
                column: "MeId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_EventId",
                table: "Drugs",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_HealthRecordId",
                table: "Events",
                column: "HealthRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthRecords_PersonId",
                table: "HealthRecords",
                column: "PersonId",
                unique: true);

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "HealthRecordSymptomType");

            migrationBuilder.DropTable(
                name: "SymptomMeasurements");

            migrationBuilder.DropTable(
                name: "TemperatureDatas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "HealthRecordsEntrys");

            migrationBuilder.DropTable(
                name: "SymptomTypes");

            migrationBuilder.DropTable(
                name: "HealthRecords");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
