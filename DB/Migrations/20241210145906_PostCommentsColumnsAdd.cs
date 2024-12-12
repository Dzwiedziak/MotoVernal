using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class PostCommentsColumnsAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "PostComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PublisherId",
                table: "PostComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PublisherId",
                table: "PostComments",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_AspNetUsers_PublisherId",
                table: "PostComments",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_AspNetUsers_PublisherId",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_PublisherId",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "PostComments");
        }
    }
}
