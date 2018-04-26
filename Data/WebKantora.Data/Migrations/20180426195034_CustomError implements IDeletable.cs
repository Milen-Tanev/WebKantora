using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebKantora.Data.Migrations
{
    public partial class CustomErrorimplementsIDeletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThrowTIme",
                table: "CustomErrors",
                newName: "ThrowTime");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CustomErrors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CustomErrors");

            migrationBuilder.RenameColumn(
                name: "ThrowTime",
                table: "CustomErrors",
                newName: "ThrowTIme");
        }
    }
}
