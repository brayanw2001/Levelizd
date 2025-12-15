using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILevelizd.Migrations
{
    /// <inheritdoc />
    public partial class SegundaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Reviews",
                type: "decimal(2,1)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Reviews",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,1)");
        }
    }
}
