using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.Data.Migrations
{
    public partial class FavouriteServiceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_favourite_products_users_user_id",
                table: "favourite_products");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "favourite_products",
                newName: "favourite_id");

            migrationBuilder.RenameIndex(
                name: "ix_favourite_products_user_id",
                table: "favourite_products",
                newName: "ix_favourite_products_favourite_id");

            migrationBuilder.CreateTable(
                name: "favourites",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_favourites", x => x.id);
                    table.ForeignKey(
                        name: "fk_favourites_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_favourites_user_id",
                table: "favourites",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_favourite_products_favourites_favourite_id",
                table: "favourite_products",
                column: "favourite_id",
                principalTable: "favourites",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_favourite_products_favourites_favourite_id",
                table: "favourite_products");

            migrationBuilder.DropTable(
                name: "favourites");

            migrationBuilder.RenameColumn(
                name: "favourite_id",
                table: "favourite_products",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_favourite_products_favourite_id",
                table: "favourite_products",
                newName: "ix_favourite_products_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_favourite_products_users_user_id",
                table: "favourite_products",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
