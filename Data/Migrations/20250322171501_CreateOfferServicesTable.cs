using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayPlus.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateOfferServicesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Offer_OfferId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_OfferId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Service");

            migrationBuilder.CreateTable(
                name: "OfferServices",
                columns: table => new
                {
                    OffersId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServicesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferServices", x => new { x.OffersId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_OfferServices_Offer_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferServices_Service_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferServices_ServicesId",
                table: "OfferServices",
                column: "ServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferServices");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Service",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_OfferId",
                table: "Service",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Offer_OfferId",
                table: "Service",
                column: "OfferId",
                principalTable: "Offer",
                principalColumn: "Id");
        }
    }
}
