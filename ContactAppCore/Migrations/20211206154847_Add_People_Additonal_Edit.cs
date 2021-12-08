using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactAppCore.Migrations
{
    public partial class Add_People_Additonal_Edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanEditAllPeopleInUnit",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanEditAllPeopleInUnit",
                table: "People");

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
    }
}
