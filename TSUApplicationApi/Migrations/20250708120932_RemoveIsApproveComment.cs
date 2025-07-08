using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSUApplicationApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsApproveComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "FeedComments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "FeedComments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
