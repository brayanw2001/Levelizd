using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APILevelizd.Migrations
{
    /// <inheritdoc />
    public partial class RenameImagesColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserImage",
                table: "Users",
                newName: "UserImageUrl");

            migrationBuilder.RenameColumn(
                name: "GameImage",
                table: "Games",
                newName: "GameImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserImageUrl",
                table: "Users",
                newName: "UserImage");

            migrationBuilder.RenameColumn(
                name: "GameImageUrl",
                table: "Games",
                newName: "GameImage");
        }
    }
}
