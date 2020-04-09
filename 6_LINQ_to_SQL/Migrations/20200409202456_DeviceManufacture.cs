using Microsoft.EntityFrameworkCore.Migrations;

namespace IteaLinqToSql.Migrations
{
    public partial class DeviceManufacture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceManufacture",
                table: "Logins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceManufacture",
                table: "Logins");
        }
    }
}
