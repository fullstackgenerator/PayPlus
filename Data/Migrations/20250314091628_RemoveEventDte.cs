using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayPlus.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEventDte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventDte",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "EventLocation",
                table: "Service");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EventDte",
                table: "Service",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EventLocation",
                table: "Service",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
