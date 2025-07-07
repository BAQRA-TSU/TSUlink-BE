using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TSUApplicationApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIsApprovedToFeedPostAndComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "FeedPosts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "FeedComments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "FeedPosts");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "FeedComments");
        }
    }
}
