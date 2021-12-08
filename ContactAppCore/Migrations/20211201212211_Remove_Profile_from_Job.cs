using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactAppCore.Migrations
{
    public partial class Remove_Profile_from_Job : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "JobProfileTags");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2021, 12, 1, 15, 22, 11, 53, DateTimeKind.Local).AddTicks(69));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2021, 12, 1, 15, 22, 11, 50, DateTimeKind.Local).AddTicks(245));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "JobProfileTags",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
