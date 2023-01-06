using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.Data.Migrations
{
    public partial class prodImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("create extension hstore");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
