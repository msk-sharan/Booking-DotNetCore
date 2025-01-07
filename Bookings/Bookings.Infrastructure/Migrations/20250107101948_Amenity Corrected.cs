using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AmenityCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Villas_VIllaId",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "VIllaId",
                table: "Amenities",
                newName: "VillaId");

            migrationBuilder.RenameIndex(
                name: "IX_Amenities_VIllaId",
                table: "Amenities",
                newName: "IX_Amenities_VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Villas_VillaId",
                table: "Amenities",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Villas_VillaId",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "VillaId",
                table: "Amenities",
                newName: "VIllaId");

            migrationBuilder.RenameIndex(
                name: "IX_Amenities_VillaId",
                table: "Amenities",
                newName: "IX_Amenities_VIllaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Villas_VIllaId",
                table: "Amenities",
                column: "VIllaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
