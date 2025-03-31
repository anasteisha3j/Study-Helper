using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace studyhelper.Migrations
{
    /// <inheritdoc />
    public partial class AddFilePathToStudy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Studies",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Studies");
        }
    }
}
