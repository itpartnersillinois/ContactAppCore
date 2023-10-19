using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactAppCore.Migrations
{
    public partial class ImageAndSignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureHeightMinimum",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PictureWidthMinimum",
                table: "Areas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SignatureExtension",
                table: "Areas",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureHeightMinimum",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "PictureWidthMinimum",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "SignatureExtension",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "SignatureUrl",
                table: "Areas");

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -2,
                column: "LastUpdated",
                value: new DateTime(2023, 9, 11, 12, 4, 4, 39, DateTimeKind.Local).AddTicks(9706));

            migrationBuilder.UpdateData(
                table: "People",
                keyColumn: "Id",
                keyValue: -1,
                column: "LastUpdated",
                value: new DateTime(2023, 9, 11, 12, 4, 4, 23, DateTimeKind.Local).AddTicks(9229));
        }
    }
}
