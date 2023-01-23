using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShop.Data.Migrations
{
    public partial class Role_Permission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "permissions",
                table: "AspNetRoles",
                type: "integer[]",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name", "permissions" },
                values: new object[] { new Guid("3917758a-29cd-4f19-acf9-1115116ae21e"), "eaa0097a-7f7e-4c49-865b-a6d880feb3ea", "administrator", "ADMINISTRATOR", new[] { 1, 2, 3, 4 } });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "id", "access_failed_count", "avatar_url", "concurrency_stamp", "email", "email_confirmed", "first_name", "last_name", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "status", "two_factor_enabled", "user_name" },
                values: new object[] { new Guid("530c4139-ef46-483c-bcd8-d57cb206429b"), 0, null, "0f3d5638-49bb-4b99-9a1f-e97fe637c62c", null, false, null, null, false, null, null, "ADMINISTRATOR", "AQAAAAEAACcQAAAAECwvoBEH96QMLJIPhJLk+IyQvZ81fjceimYcBx1XRVEy2zAReHrhbYdpFKHdBsl5aA==", null, false, "4950e047-6a3d-4154-83d1-cc36bbc111f4", 0, false, "administrator" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { new Guid("3917758a-29cd-4f19-acf9-1115116ae21e"), new Guid("530c4139-ef46-483c-bcd8-d57cb206429b") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { new Guid("3917758a-29cd-4f19-acf9-1115116ae21e"), new Guid("530c4139-ef46-483c-bcd8-d57cb206429b") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "id",
                keyValue: new Guid("3917758a-29cd-4f19-acf9-1115116ae21e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "id",
                keyValue: new Guid("530c4139-ef46-483c-bcd8-d57cb206429b"));

            migrationBuilder.DropColumn(
                name: "permissions",
                table: "AspNetRoles");
        }
    }
}
