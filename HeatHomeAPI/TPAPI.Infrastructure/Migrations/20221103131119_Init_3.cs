using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPAPI.Infrastructure.Migrations
{
    public partial class Init_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Temperature_HardwareUnits_HeatingUnitId",
                table: "Temperature");

            migrationBuilder.AlterColumn<Guid>(
                name: "HeatingUnitId",
                table: "Temperature",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperature_HardwareUnits_HeatingUnitId",
                table: "Temperature",
                column: "HeatingUnitId",
                principalTable: "HardwareUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Temperature_HardwareUnits_HeatingUnitId",
                table: "Temperature");

            migrationBuilder.AlterColumn<Guid>(
                name: "HeatingUnitId",
                table: "Temperature",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Temperature_HardwareUnits_HeatingUnitId",
                table: "Temperature",
                column: "HeatingUnitId",
                principalTable: "HardwareUnits",
                principalColumn: "Id");
        }
    }
}
