using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactAppCore.Migrations
{
    public partial class Add_JobProfile_Office_Link : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeActivities_EmployeeProfiles_EmployeeProfileId",
                table: "EmployeeActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_JobProfiles_EmployeeProfiles_EmployeeProfileId",
                table: "JobProfiles");

            migrationBuilder.DropColumn(
                name: "OfficeInformation",
                table: "JobProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeProfileId",
                table: "JobProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "JobProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeProfileId",
                table: "EmployeeActivities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2021, 12, 1, 13, 37, 48, 678, DateTimeKind.Local).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2021, 12, 1, 13, 37, 48, 676, DateTimeKind.Local).AddTicks(1817));

            migrationBuilder.CreateIndex(
                name: "IX_JobProfiles_OfficeId",
                table: "JobProfiles",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeActivities_EmployeeProfiles_EmployeeProfileId",
                table: "EmployeeActivities",
                column: "EmployeeProfileId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobProfiles_EmployeeProfiles_EmployeeProfileId",
                table: "JobProfiles",
                column: "EmployeeProfileId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobProfiles_Offices_OfficeId",
                table: "JobProfiles",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeActivities_EmployeeProfiles_EmployeeProfileId",
                table: "EmployeeActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_JobProfiles_EmployeeProfiles_EmployeeProfileId",
                table: "JobProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_JobProfiles_Offices_OfficeId",
                table: "JobProfiles");

            migrationBuilder.DropIndex(
                name: "IX_JobProfiles_OfficeId",
                table: "JobProfiles");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "JobProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeProfileId",
                table: "JobProfiles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "OfficeInformation",
                table: "JobProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeProfileId",
                table: "EmployeeActivities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2021, 11, 30, 10, 56, 1, 463, DateTimeKind.Local).AddTicks(2887));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2021, 11, 30, 10, 56, 1, 460, DateTimeKind.Local).AddTicks(3047));

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeActivities_EmployeeProfiles_EmployeeProfileId",
                table: "EmployeeActivities",
                column: "EmployeeProfileId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobProfiles_EmployeeProfiles_EmployeeProfileId",
                table: "JobProfiles",
                column: "EmployeeProfileId",
                principalTable: "EmployeeProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
