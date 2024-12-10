using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class PostCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComment_Posts_PostId",
                table: "PostComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment");

            migrationBuilder.RenameTable(
                name: "PostComment",
                newName: "PostComments");

            migrationBuilder.RenameIndex(
                name: "IX_PostComment_PostId",
                table: "PostComments",
                newName: "IX_PostComments_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.RenameTable(
                name: "PostComments",
                newName: "PostComment");

            migrationBuilder.RenameIndex(
                name: "IX_PostComments_PostId",
                table: "PostComment",
                newName: "IX_PostComment_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComment_Posts_PostId",
                table: "PostComment",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
