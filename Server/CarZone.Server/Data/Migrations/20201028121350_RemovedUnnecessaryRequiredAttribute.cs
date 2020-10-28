namespace CarZone.Server.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemovedUnnecessaryRequiredAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Advertisements_BrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId1",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_AdvertisementId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BrandId1",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "AdvertisementId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_AdvertisementId",
                table: "Cars",
                column: "AdvertisementId",
                unique: true,
                filter: "[AdvertisementId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Brands_BrandId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_AdvertisementId",
                table: "Cars");

            migrationBuilder.AlterColumn<string>(
                name: "AdvertisementId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandId1",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_AdvertisementId",
                table: "Cars",
                column: "AdvertisementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BrandId1",
                table: "Cars",
                column: "BrandId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Advertisements_BrandId",
                table: "Cars",
                column: "BrandId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Brands_BrandId1",
                table: "Cars",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
