using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store99.Migrations
{
    public partial class AddingShoeFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ShoeFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Secure_Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Public_Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoeFiles_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoeFiles_ShoeId",
                table: "ShoeFiles",
                column: "ShoeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoeFiles");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Brands",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
