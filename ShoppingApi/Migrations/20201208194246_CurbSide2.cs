using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApi.Migrations
{
    public partial class CurbSide2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PickupDate",
                table: "CurbSide",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "CurbSide",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "CurbSide");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PickupDate",
                table: "CurbSide",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
