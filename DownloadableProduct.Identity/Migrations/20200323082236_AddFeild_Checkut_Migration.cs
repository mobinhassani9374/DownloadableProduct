using Microsoft.EntityFrameworkCore.Migrations;

namespace DownloadableProduct.Identity.Migrations
{
    public partial class AddFeild_Checkut_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Checkouts",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CartNumber",
                table: "Checkouts",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "CartNumber",
                table: "Checkouts");
        }
    }
}
