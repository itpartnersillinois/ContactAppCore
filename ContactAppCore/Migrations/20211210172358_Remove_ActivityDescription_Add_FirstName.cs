using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactAppCore.Migrations
{
    public partial class Remove_ActivityDescription_Add_FirstName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobProfileTags_JobProfiles_JobProfileId",
                table: "JobProfileTags");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "EmployeeActivities");

            migrationBuilder.AlterColumn<int>(
                name: "JobProfileId",
                table: "JobProfileTags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreferredNameLast",
                table: "EmployeeProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2021, 12, 10, 11, 23, 57, 260, DateTimeKind.Local).AddTicks(1394));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2021, 12, 10, 11, 23, 57, 255, DateTimeKind.Local).AddTicks(3640));

            migrationBuilder.AddForeignKey(
                name: "FK_JobProfileTags_JobProfiles_JobProfileId",
                table: "JobProfileTags",
                column: "JobProfileId",
                principalTable: "JobProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobProfileTags_JobProfiles_JobProfileId",
                table: "JobProfileTags");

            migrationBuilder.DropColumn(
                name: "PreferredNameLast",
                table: "EmployeeProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "JobProfileId",
                table: "JobProfileTags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "EmployeeActivities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2021, 12, 6, 9, 48, 47, 367, DateTimeKind.Local).AddTicks(7692));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2021, 12, 6, 9, 48, 47, 363, DateTimeKind.Local).AddTicks(9245));

            migrationBuilder.AddForeignKey(
                name: "FK_JobProfileTags_JobProfiles_JobProfileId",
                table: "JobProfileTags",
                column: "JobProfileId",
                principalTable: "JobProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
