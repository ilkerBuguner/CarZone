namespace CarZone.Server.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixedCarSafetiesTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carSafeties_Cars_CarId",
                table: "carSafeties");

            migrationBuilder.DropForeignKey(
                name: "FK_carSafeties_Safeties_SafetyId",
                table: "carSafeties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_carSafeties",
                table: "carSafeties");

            migrationBuilder.RenameTable(
                name: "carSafeties",
                newName: "CarSafeties");

            migrationBuilder.RenameIndex(
                name: "IX_carSafeties_SafetyId",
                table: "CarSafeties",
                newName: "IX_CarSafeties_SafetyId");

            migrationBuilder.RenameIndex(
                name: "IX_carSafeties_IsDeleted",
                table: "CarSafeties",
                newName: "IX_CarSafeties_IsDeleted");

            migrationBuilder.AddColumn<int>(
                name: "DoorsCount",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarSafeties",
                table: "CarSafeties",
                columns: new[] { "CarId", "SafetyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarSafeties_Cars_CarId",
                table: "CarSafeties",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarSafeties_Safeties_SafetyId",
                table: "CarSafeties",
                column: "SafetyId",
                principalTable: "Safeties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarSafeties_Cars_CarId",
                table: "CarSafeties");

            migrationBuilder.DropForeignKey(
                name: "FK_CarSafeties_Safeties_SafetyId",
                table: "CarSafeties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarSafeties",
                table: "CarSafeties");

            migrationBuilder.DropColumn(
                name: "DoorsCount",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "CarSafeties",
                newName: "carSafeties");

            migrationBuilder.RenameIndex(
                name: "IX_CarSafeties_SafetyId",
                table: "carSafeties",
                newName: "IX_carSafeties_SafetyId");

            migrationBuilder.RenameIndex(
                name: "IX_CarSafeties_IsDeleted",
                table: "carSafeties",
                newName: "IX_carSafeties_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_carSafeties",
                table: "carSafeties",
                columns: new[] { "CarId", "SafetyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_carSafeties_Cars_CarId",
                table: "carSafeties",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_carSafeties_Safeties_SafetyId",
                table: "carSafeties",
                column: "SafetyId",
                principalTable: "Safeties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
