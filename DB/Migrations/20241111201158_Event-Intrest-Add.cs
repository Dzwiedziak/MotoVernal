using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class EventIntrestAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Topics",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "TopicResponses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Sections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleOfferId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "BugReports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "Bans",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventInterests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventInterests_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_ImageId",
                table: "Topics",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicResponses_ImageId",
                table: "TopicResponses",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_ImageId",
                table: "Sections",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ImageId",
                table: "Reports",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ImageId",
                table: "Posts",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_VehicleOfferId",
                table: "Files",
                column: "VehicleOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ImageId",
                table: "Events",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_BugReports_ImageId",
                table: "BugReports",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Bans_ImageId",
                table: "Bans",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId");

            migrationBuilder.CreateIndex(
                name: "IX_EventInterests_EventId",
                table: "EventInterests",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventInterests_UserId",
                table: "EventInterests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Files_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bans_Files_ImageId",
                table: "Bans",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BugReports_Files_ImageId",
                table: "BugReports",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Files_ImageId",
                table: "Events",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_VehicleOffers_VehicleOfferId",
                table: "Files",
                column: "VehicleOfferId",
                principalTable: "VehicleOffers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Files_ImageId",
                table: "Posts",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Files_ImageId",
                table: "Reports",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Files_ImageId",
                table: "Sections",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicResponses_Files_ImageId",
                table: "TopicResponses",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Files_ImageId",
                table: "Topics",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Files_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Bans_Files_ImageId",
                table: "Bans");

            migrationBuilder.DropForeignKey(
                name: "FK_BugReports_Files_ImageId",
                table: "BugReports");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Files_ImageId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_VehicleOffers_VehicleOfferId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Files_ImageId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Files_ImageId",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Files_ImageId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicResponses_Files_ImageId",
                table: "TopicResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Files_ImageId",
                table: "Topics");

            migrationBuilder.DropTable(
                name: "EventInterests");

            migrationBuilder.DropIndex(
                name: "IX_Topics_ImageId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_TopicResponses_ImageId",
                table: "TopicResponses");

            migrationBuilder.DropIndex(
                name: "IX_Sections_ImageId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Reports_ImageId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ImageId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Files_VehicleOfferId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Events_ImageId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_BugReports_ImageId",
                table: "BugReports");

            migrationBuilder.DropIndex(
                name: "IX_Bans_ImageId",
                table: "Bans");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "TopicResponses");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "VehicleOfferId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "BugReports");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Bans");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "AspNetUsers");
        }
    }
}
