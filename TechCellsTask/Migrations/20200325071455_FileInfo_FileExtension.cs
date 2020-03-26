using Microsoft.EntityFrameworkCore.Migrations;

namespace TechCellsTaskApi.Migrations
{
    public partial class FileInfo_FileExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "FileInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "FileInfos");
        }
    }
}
