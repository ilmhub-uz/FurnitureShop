using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.Data.Migrations
{
    public partial class addauthorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "author_id",
                table: "products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("530c4139-ef46-483c-bcd8-d57cb206429b"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "3fe49218-13a4-48fa-b67e-76a3a4844662", "AQAAAAEAACcQAAAAEAULh1F8j0EWlttNtOMmgOyw8DC/z1vVLTgvBKKdgtPo3ZKUa6SdOSDZfPQwS0ZPYw==", "41659d8f-d910-46bb-8c16-0e74884f733d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "author_id",
                table: "products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("530c4139-ef46-483c-bcd8-d57cb206429b"),
                columns: new[] { "concurrency_stamp", "password_hash", "security_stamp" },
                values: new object[] { "0f3d5638-49bb-4b99-9a1f-e97fe637c62c", "AQAAAAEAACcQAAAAECwvoBEH96QMLJIPhJLk+IyQvZ81fjceimYcBx1XRVEy2zAReHrhbYdpFKHdBsl5aA==", "4950e047-6a3d-4154-83d1-cc36bbc111f4" });
        }
    }
}
