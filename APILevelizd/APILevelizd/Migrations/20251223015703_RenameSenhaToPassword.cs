using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILevelizd.Migrations
{
    /// <inheritdoc />
    public partial class RenameSenhaToPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Users",
                newName: "Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Senha");
        }
    }
}
