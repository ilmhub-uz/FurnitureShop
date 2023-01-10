using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.Data.Migrations
{
    public partial class int2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_orders_contracts_contract_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "ix_orders_contract_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "contract_id",
                table: "orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "contract_id",
                table: "orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_orders_contract_id",
                table: "orders",
                column: "contract_id");

            migrationBuilder.AddForeignKey(
                name: "fk_orders_contracts_contract_id",
                table: "orders",
                column: "contract_id",
                principalTable: "contracts",
                principalColumn: "id");
        }
    }
}
