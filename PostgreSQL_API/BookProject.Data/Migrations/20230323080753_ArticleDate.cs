using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookProject.Data.Migrations
{
    public partial class ArticleDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Articles",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_DATE");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Articles");
        }
    }
}
    