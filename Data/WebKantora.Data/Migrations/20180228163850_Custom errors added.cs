using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebKantora.Data.Migrations
{
    public partial class Customerrorsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomErrors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomMessage = table.Column<string>(nullable: true),
                    InnerException = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Source = table.Column<string>(nullable: false),
                    StackTrace = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomErrors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomErrors");
        }
    }
}
