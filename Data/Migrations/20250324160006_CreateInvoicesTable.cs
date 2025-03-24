using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayPlus.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateInvoicesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Service",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PartnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    OfferDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Partner_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Service_InvoiceId",
                table: "Service",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_PartnerId",
                table: "Invoice",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Invoice_InvoiceId",
                table: "Service",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Invoice_InvoiceId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Service_InvoiceId",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Service");
        }
    }
}
