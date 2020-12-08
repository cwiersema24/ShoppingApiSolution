using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingApi.Migrations
{
    public partial class CurbSide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurbSide",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    For = table.Column<string>(nullable: true),
                    Items = table.Column<string>(nullable: true),
                    PickupDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurbSide", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurbSide");
        }
    }
}
