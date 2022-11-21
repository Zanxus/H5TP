using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPAPI.Infrastructure.Migrations
{
    public partial class Init_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeatingUnits_Coordinates_CoordinateId",
                table: "HeatingUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_HeatingUnits_HardwareType_HardwareTypeId",
                table: "HeatingUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_Temperature_HeatingUnits_HeatingUnitId",
                table: "Temperature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HardwareType",
                table: "HardwareType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeatingUnits",
                table: "HeatingUnits");

            migrationBuilder.RenameTable(
                name: "HardwareType",
                newName: "HardwareTypes");

            migrationBuilder.RenameTable(
                name: "HeatingUnits",
                newName: "HardwareUnits");

            migrationBuilder.RenameIndex(
                name: "IX_HeatingUnits_HardwareTypeId",
                table: "HardwareUnits",
                newName: "IX_HardwareUnits_HardwareTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HeatingUnits_CoordinateId",
                table: "HardwareUnits",
                newName: "IX_HardwareUnits_CoordinateId");

            migrationBuilder.AlterColumn<float>(
                name: "TargetTemperature",
                table: "HardwareUnits",
                type: "real",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "HardwareUnits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HardwareTypes",
                table: "HardwareTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HardwareUnits",
                table: "HardwareUnits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareUnits_Coordinates_CoordinateId",
                table: "HardwareUnits",
                column: "CoordinateId",
                principalTable: "Coordinates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareUnits_HardwareTypes_HardwareTypeId",
                table: "HardwareUnits",
                column: "HardwareTypeId",
                principalTable: "HardwareTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperature_HardwareUnits_HeatingUnitId",
                table: "Temperature",
                column: "HeatingUnitId",
                principalTable: "HardwareUnits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareUnits_Coordinates_CoordinateId",
                table: "HardwareUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareUnits_HardwareTypes_HardwareTypeId",
                table: "HardwareUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_Temperature_HardwareUnits_HeatingUnitId",
                table: "Temperature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HardwareTypes",
                table: "HardwareTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HardwareUnits",
                table: "HardwareUnits");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "HardwareUnits");

            migrationBuilder.RenameTable(
                name: "HardwareTypes",
                newName: "HardwareType");

            migrationBuilder.RenameTable(
                name: "HardwareUnits",
                newName: "HeatingUnits");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareUnits_HardwareTypeId",
                table: "HeatingUnits",
                newName: "IX_HeatingUnits_HardwareTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareUnits_CoordinateId",
                table: "HeatingUnits",
                newName: "IX_HeatingUnits_CoordinateId");

            migrationBuilder.AlterColumn<float>(
                name: "TargetTemperature",
                table: "HeatingUnits",
                type: "real",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HardwareType",
                table: "HardwareType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeatingUnits",
                table: "HeatingUnits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeatingUnits_Coordinates_CoordinateId",
                table: "HeatingUnits",
                column: "CoordinateId",
                principalTable: "Coordinates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeatingUnits_HardwareType_HardwareTypeId",
                table: "HeatingUnits",
                column: "HardwareTypeId",
                principalTable: "HardwareType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Temperature_HeatingUnits_HeatingUnitId",
                table: "Temperature",
                column: "HeatingUnitId",
                principalTable: "HeatingUnits",
                principalColumn: "Id");
        }
    }
}
