using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSUApplicationApi.Migrations
{
    /// <inheritdoc />
    public partial class AddApproveAndDeleteReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "SubjectReviews",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "LecturerReviews",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "SubjectReviews");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "LecturerReviews");
        }
    }
}
