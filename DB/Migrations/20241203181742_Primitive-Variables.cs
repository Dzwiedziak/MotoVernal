using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class PrimitiveVariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleOffers_Locations_LocationId",
                table: "VehicleOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleOffers_Phones_PhoneId",
                table: "VehicleOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleOffers_Prices_PriceId",
                table: "VehicleOffers");

            migrationBuilder.DropIndex(
                name: "IX_VehicleOffers_LocationId",
                table: "VehicleOffers");

            migrationBuilder.DropIndex(
                name: "IX_VehicleOffers_PhoneId",
                table: "VehicleOffers");

            migrationBuilder.DropIndex(
                name: "IX_VehicleOffers_PriceId",
                table: "VehicleOffers");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "VehicleOffers");

            migrationBuilder.DropColumn(
                name: "PhoneId",
                table: "VehicleOffers");

            migrationBuilder.RenameColumn(
                name: "PriceId",
                table: "VehicleOffers",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "VehicleOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "VehicleOffers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "VehicleOffers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "VehicleOffers");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "VehicleOffers",
                newName: "PriceId");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "VehicleOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PhoneId",
                table: "VehicleOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOffers_LocationId",
                table: "VehicleOffers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOffers_PhoneId",
                table: "VehicleOffers",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleOffers_PriceId",
                table: "VehicleOffers",
                column: "PriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleOffers_Locations_LocationId",
                table: "VehicleOffers",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleOffers_Phones_PhoneId",
                table: "VehicleOffers",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleOffers_Prices_PriceId",
                table: "VehicleOffers",
                column: "PriceId",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
