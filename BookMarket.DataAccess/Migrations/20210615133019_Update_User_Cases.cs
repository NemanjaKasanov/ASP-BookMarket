using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMarket.DataAccess.Migrations
{
    public partial class Update_User_Cases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 15, 15, 30, 19, 114, DateTimeKind.Local).AddTicks(589),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 13, 17, 11, 30, 763, DateTimeKind.Local).AddTicks(1536));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 6, 13, 17, 11, 30, 763, DateTimeKind.Local).AddTicks(1536),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 6, 15, 15, 30, 19, 114, DateTimeKind.Local).AddTicks(589));
        }
    }
}
