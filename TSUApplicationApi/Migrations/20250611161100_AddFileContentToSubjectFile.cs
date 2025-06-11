using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSUApplicationApi.Migrations
{
    /// <inheritdoc />
    public partial class AddFileContentToSubjectFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "SubjectFile",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "SubjectFile",
                type: "longblob",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "SubjectFile");

            migrationBuilder.DropColumn(
                name: "FileContent",
                table: "SubjectFile");
        }
    }
}
