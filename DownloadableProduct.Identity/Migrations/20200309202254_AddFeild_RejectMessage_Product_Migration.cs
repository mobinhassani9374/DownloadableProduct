using Microsoft.EntityFrameworkCore.Migrations;

namespace DownloadableProduct.Identity.Migrations
{
    public partial class AddFeild_RejectMessage_Product_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectMessage",
                table: "Products",
                maxLength: 550,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectMessage",
                table: "Products");
        }
    }
}
