using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.Data.Migrations
{
    public partial class Category_Table_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_category_images_category_image_id",
                table: "categories");

            migrationBuilder.AlterColumn<Guid>(
                name: "category_image_id",
                table: "categories",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_categories_category_images_category_image_id",
                table: "categories",
                column: "category_image_id",
                principalTable: "category_images",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_category_images_category_image_id",
                table: "categories");

            migrationBuilder.AlterColumn<Guid>(
                name: "category_image_id",
                table: "categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_categories_category_images_category_image_id",
                table: "categories",
                column: "category_image_id",
                principalTable: "category_images",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
