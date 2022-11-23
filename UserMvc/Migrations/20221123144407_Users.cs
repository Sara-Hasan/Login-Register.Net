using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserMvc.Migrations
{
    public partial class Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "users",
                type: "varbinary(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "users",
                type: "varbinary(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "users");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "users");
        }
    }
}
