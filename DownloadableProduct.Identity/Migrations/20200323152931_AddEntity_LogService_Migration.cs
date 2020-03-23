using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DownloadableProduct.Identity.Migrations
{
    public partial class AddEntity_LogService_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Method = table.Column<string>(maxLength: 50, nullable: true),
                    ContentLengthRequest = table.Column<long>(nullable: true),
                    ContentLengthResponse = table.Column<long>(nullable: true),
                    RelativePath = table.Column<string>(maxLength: 250, nullable: true),
                    UserId = table.Column<string>(maxLength: 60, nullable: true),
                    ElabsedTime = table.Column<long>(nullable: true),
                    ResponseStatusCode = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Elabsed = table.Column<TimeSpan>(nullable: false),
                    IpAddress = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogServices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogServices");
        }
    }
}
