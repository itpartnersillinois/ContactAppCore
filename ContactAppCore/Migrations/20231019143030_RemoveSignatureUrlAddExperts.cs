using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactAppCore.Migrations
{
    public partial class RemoveSignatureUrlAddExperts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignatureUrl",
                table: "Areas");

            migrationBuilder.AddColumn<bool>(
                name: "IsInExperts",
                table: "EmployeeProfiles",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 19, 9, 30, 30, 35, DateTimeKind.Local).AddTicks(1398));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 19, 9, 30, 30, 30, DateTimeKind.Local).AddTicks(1133));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInExperts",
                table: "EmployeeProfiles");

            migrationBuilder.AddColumn<string>(
                name: "SignatureUrl",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 18, 11, 20, 27, 141, DateTimeKind.Local).AddTicks(6662));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2023, 10, 18, 11, 20, 27, 137, DateTimeKind.Local).AddTicks(488));
        }
    }
}
