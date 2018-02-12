using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebKantora.Data.Migrations
{
    public partial class KeywordArticleadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Keywords_KeywordId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Keywords_Articles_ArticleId",
                table: "Keywords");

            migrationBuilder.DropIndex(
                name: "IX_Keywords_ArticleId",
                table: "Keywords");

            migrationBuilder.DropIndex(
                name: "IX_Articles_KeywordId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "KeywordId",
                table: "Articles");

            migrationBuilder.CreateTable(
                name: "KeywordArticle",
                columns: table => new
                {
                    KeywordId = table.Column<Guid>(nullable: false),
                    ArticleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordArticle", x => new { x.KeywordId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_KeywordArticle_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KeywordArticle_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeywordArticle_ArticleId",
                table: "KeywordArticle",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeywordArticle");

            migrationBuilder.AddColumn<Guid>(
                name: "ArticleId",
                table: "Keywords",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "KeywordId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Keywords_ArticleId",
                table: "Keywords",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_KeywordId",
                table: "Articles",
                column: "KeywordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Keywords_KeywordId",
                table: "Articles",
                column: "KeywordId",
                principalTable: "Keywords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Keywords_Articles_ArticleId",
                table: "Keywords",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
