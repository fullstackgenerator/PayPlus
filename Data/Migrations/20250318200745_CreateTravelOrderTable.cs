using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayPlus.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateTravelOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TravelOrder",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    order_date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    date_start = table.Column<DateTime>(type: "TEXT", nullable: false),
                    date_end = table.Column<DateTime>(type: "TEXT", nullable: false),
                    location_start = table.Column<string>(type: "TEXT", nullable: false),
                    location_end = table.Column<string>(type: "TEXT", nullable: false),
                    full_name_driver = table.Column<string>(type: "TEXT", nullable: false),
                    car_brand_and_model = table.Column<string>(type: "TEXT", nullable: false),
                    car_type = table.Column<int>(type: "INTEGER", nullable: false),
                    trip_reason = table.Column<string>(type: "TEXT", nullable: false),
                    full_name_organizer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelOrder", x => x.order_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelOrder");
        }
    }
}
