using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loaf.EntityFrameworkCore.Tests.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bus_student",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false, comment: "name"),
                    NickName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false, comment: "nickname"),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: false, comment: "birthday")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bus_student", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bus_student");
        }
    }
}