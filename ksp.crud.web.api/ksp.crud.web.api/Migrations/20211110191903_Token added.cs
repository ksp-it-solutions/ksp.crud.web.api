using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ksp.crud.web.api.Migrations
{
    public partial class Tokenadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    TokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsExpirated = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.TokenId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token");
        }
    }
}
