using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactAppCore.Migrations
{
    public partial class AddPictureDimensions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureHeight",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PictureWidth",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2022, 5, 3, 9, 26, 18, 579, DateTimeKind.Local).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2022, 5, 3, 9, 26, 18, 572, DateTimeKind.Local).AddTicks(8949));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureHeight",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "PictureWidth",
                table: "Areas");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2022, 4, 12, 7, 41, 22, 911, DateTimeKind.Local).AddTicks(761));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2022, 4, 12, 7, 41, 22, 907, DateTimeKind.Local).AddTicks(5137));
        }
    }
}
