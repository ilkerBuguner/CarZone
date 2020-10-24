using Microsoft.EntityFrameworkCore.Migrations;

namespace CarZone.Server.Data.Migrations
{
    public partial class FixedBrandColumnInModelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Advertisements_BrandId",
                table: "Models");

            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId1",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_BrandId1",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "BrandId1",
                table: "Models");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Brands_BrandId",
                table: "Models");

            migrationBuilder.AddColumn<string>(
                name: "BrandId1",
                table: "Models",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId1",
                table: "Models",
                column: "BrandId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Advertisements_BrandId",
                table: "Models",
                column: "BrandId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Brands_BrandId1",
                table: "Models",
                column: "BrandId1",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
