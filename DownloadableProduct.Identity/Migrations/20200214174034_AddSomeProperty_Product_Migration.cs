using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DownloadableProduct.Identity.Migrations
{
    public partial class AddSomeProperty_Product_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountBuy",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadFileDate",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UserUpoadImageDate",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountBuy",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UploadFileDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserUpoadImageDate",
                table: "Products");
        }
    }
}
