using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayPlus.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsAndRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Invoice_InvoiceId",
                table: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Service_InvoiceId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "OfferDate",
                table: "Offer",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "OfferDate",
                table: "Invoice",
                newName: "Date");

            migrationBuilder.CreateTable(
                name: "InvoiceServices",
                columns: table => new
                {
                    InvoicesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ServicesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceServices", x => new { x.InvoicesId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_InvoiceServices_Invoice_InvoicesId",
                        column: x => x.InvoicesId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceServices_Service_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceServices_ServicesId",
                table: "InvoiceServices",
                column: "ServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceServices");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Offer",
                newName: "OfferDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Invoice",
                newName: "OfferDate");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Service",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Service_InvoiceId",
                table: "Service",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Invoice_InvoiceId",
                table: "Service",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");
        }
    }
}
