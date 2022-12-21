using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.Data.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_contracts_products_product_id",
                table: "contracts");

            migrationBuilder.DropIndex(
                name: "ix_contracts_product_id",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "product_id",
                table: "contracts");

            migrationBuilder.DropColumn(
                name: "product_properties",
                table: "contracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "product_id",
                table: "contracts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Dictionary<string, string>>(
                name: "product_properties",
                table: "contracts",
                type: "hstore",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_contracts_product_id",
                table: "contracts",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "fk_contracts_products_product_id",
                table: "contracts",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
