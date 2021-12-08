using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactAppCore.Migrations
{
    public partial class Adding_People : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryProfile = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalOrder = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearEnded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearStarted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeProfileId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeActivities_EmployeeProfiles_EmployeeProfileId",
                        column: x => x.EmployeeProfileId,
                        principalTable: "EmployeeProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalOrder = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeProfileId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLinks_EmployeeProfiles_EmployeeProfileId",
                        column: x => x.EmployeeProfileId,
                        principalTable: "EmployeeProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeProfileId = table.Column<int>(type: "int", nullable: true),
                    InternalOrder = table.Column<int>(type: "int", nullable: false),
                    OfficeInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobProfiles_EmployeeProfiles_EmployeeProfileId",
                        column: x => x.EmployeeProfileId,
                        principalTable: "EmployeeProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobProfileTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<int>(type: "int", nullable: false),
                    JobProfileId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobProfileTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobProfileTags_JobProfiles_JobProfileId",
                        column: x => x.JobProfileId,
                        principalTable: "JobProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2021, 11, 30, 10, 28, 19, 281, DateTimeKind.Local).AddTicks(582));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2021, 11, 30, 10, 28, 19, 277, DateTimeKind.Local).AddTicks(4272));

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeActivities_EmployeeProfileId",
                table: "EmployeeActivities",
                column: "EmployeeProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLinks_EmployeeProfileId",
                table: "EmployeeLinks",
                column: "EmployeeProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_JobProfiles_EmployeeProfileId",
                table: "JobProfiles",
                column: "EmployeeProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_JobProfileTags_JobProfileId",
                table: "JobProfileTags",
                column: "JobProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeActivities");

            migrationBuilder.DropTable(
                name: "EmployeeLinks");

            migrationBuilder.DropTable(
                name: "JobProfileTags");

            migrationBuilder.DropTable(
                name: "JobProfiles");

            migrationBuilder.DropTable(
                name: "EmployeeProfiles");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2021, 11, 12, 13, 29, 34, 587, DateTimeKind.Local).AddTicks(8756));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2021, 11, 12, 13, 29, 34, 584, DateTimeKind.Local).AddTicks(8582));
        }
    }
}
