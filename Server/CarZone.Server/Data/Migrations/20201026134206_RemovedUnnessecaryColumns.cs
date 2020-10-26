namespace CarZone.Server.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemovedUnnessecaryColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Advertisements",
                newName: "Title");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Advertisements",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "Advertisements",
                type: "int",
                maxLength: 12,
                nullable: false,
                defaultValue: 0);
        }
    }
}
