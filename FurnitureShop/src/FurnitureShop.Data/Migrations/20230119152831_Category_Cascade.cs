using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.Data.Migrations
{
    public partial class Category_Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_categories_parent_id",
                table: "categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("530c4139-ef46-483c-bcd8-d57cb206429b"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "db637b7b-9240-407c-9ec2-f50a9f16fbce", "AQAAAAEAACcQAAAAEIttxX+m+Wo2/3xXoighdkCHDkC4Er4H8tpl3INVWrTY1kyjvgjmr7+pg22b/a57GQ==", "39fba202-23d1-43cf-9a0e-4117ac645fec" });

            migrationBuilder.AddForeignKey(
                name: "fk_categories_categories_parent_id",
                table: "categories",
                column: "parent_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_categories_categories_parent_id",
                table: "categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("530c4139-ef46-483c-bcd8-d57cb206429b"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "3fe49218-13a4-48fa-b67e-76a3a4844662", "AQAAAAEAACcQAAAAEAULh1F8j0EWlttNtOMmgOyw8DC/z1vVLTgvBKKdgtPo3ZKUa6SdOSDZfPQwS0ZPYw==", "41659d8f-d910-46bb-8c16-0e74884f733d" });

            migrationBuilder.AddForeignKey(
                name: "fk_categories_categories_parent_id",
                table: "categories",
                column: "parent_id",
                principalTable: "categories",
                principalColumn: "id");
        }
    }
}
