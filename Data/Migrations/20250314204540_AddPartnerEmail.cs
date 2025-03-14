using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayPlus.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPartnerEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Partners",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Partners",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Partners");
        }
    }
}
