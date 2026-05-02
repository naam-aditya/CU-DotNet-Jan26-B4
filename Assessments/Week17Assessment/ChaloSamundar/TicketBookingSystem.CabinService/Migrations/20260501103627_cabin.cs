using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBookingSystem.CabinService.Migrations
{
    /// <inheritdoc />
    public partial class cabin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalCabins",
                table: "CabinTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BookingReference",
                table: "Cabins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReservedAt",
                table: "Cabins",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Cabins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Cabins",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCabins",
                table: "CabinTypes");

            migrationBuilder.DropColumn(
                name: "BookingReference",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "ReservedAt",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cabins");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Cabins");
        }
    }
}
