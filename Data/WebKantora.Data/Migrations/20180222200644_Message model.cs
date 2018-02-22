using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebKantora.Data.Migrations
{
    public partial class Messagemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Messages");
        }
    }
}
