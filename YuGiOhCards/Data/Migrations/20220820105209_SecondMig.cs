using Microsoft.EntityFrameworkCore.Migrations;

namespace YuGiOhCards.Data.Migrations
{
    public partial class SecondMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birim",
                table: "Urun");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Birim",
                table: "Urun",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
