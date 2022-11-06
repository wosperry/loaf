using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loaf.Admin.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "姓名"),
                    Account = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "账号"),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "密码"),
                    Salt = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "盐")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_Account",
                table: "user",
                column: "Account",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
