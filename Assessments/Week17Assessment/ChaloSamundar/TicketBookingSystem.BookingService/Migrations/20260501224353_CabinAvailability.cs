using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketBookingSystem.BookingService.Migrations
{
    /// <inheritdoc />
    public partial class CabinAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CabinAvailabilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    CabinTypeId = table.Column<int>(type: "int", nullable: false),
                    CabinTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCabins = table.Column<int>(type: "int", nullable: false),
                    BookedCabins = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinAvailabilities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CabinAvailabilities",
                columns: new[] { "Id", "BookedCabins", "CabinTypeId", "CabinTypeName", "TotalCabins", "TripId" },
                values: new object[,]
                {
                    { 1, 0, 1, "Interior", 20, 1 },
                    { 2, 0, 2, "Ocean View", 15, 1 },
                    { 3, 0, 3, "Balcony", 10, 1 },
                    { 4, 0, 4, "Suite", 5, 1 },
                    { 5, 0, 1, "Interior", 20, 2 },
                    { 6, 0, 2, "Ocean View", 15, 2 },
                    { 7, 0, 3, "Balcony", 10, 2 },
                    { 8, 0, 4, "Suite", 5, 2 },
                    { 9, 0, 1, "Interior", 20, 3 },
                    { 10, 0, 2, "Ocean View", 15, 3 },
                    { 11, 0, 3, "Balcony", 10, 3 },
                    { 12, 0, 4, "Suite", 5, 3 },
                    { 13, 0, 1, "Interior", 20, 4 },
                    { 14, 0, 2, "Ocean View", 15, 4 },
                    { 15, 0, 3, "Balcony", 10, 4 },
                    { 16, 0, 4, "Suite", 5, 4 },
                    { 17, 0, 1, "Interior", 20, 5 },
                    { 18, 0, 2, "Ocean View", 15, 5 },
                    { 19, 0, 3, "Balcony", 10, 5 },
                    { 20, 0, 4, "Suite", 5, 5 },
                    { 21, 0, 1, "Interior", 20, 6 },
                    { 22, 0, 2, "Ocean View", 15, 6 },
                    { 23, 0, 3, "Balcony", 10, 6 },
                    { 24, 0, 4, "Suite", 5, 6 },
                    { 25, 0, 1, "Interior", 20, 7 },
                    { 26, 0, 2, "Ocean View", 15, 7 },
                    { 27, 0, 3, "Balcony", 10, 7 },
                    { 28, 0, 4, "Suite", 5, 7 },
                    { 29, 0, 1, "Interior", 20, 8 },
                    { 30, 0, 2, "Ocean View", 15, 8 },
                    { 31, 0, 3, "Balcony", 10, 8 },
                    { 32, 0, 4, "Suite", 5, 8 },
                    { 33, 0, 1, "Interior", 20, 9 },
                    { 34, 0, 2, "Ocean View", 15, 9 },
                    { 35, 0, 3, "Balcony", 10, 9 },
                    { 36, 0, 4, "Suite", 5, 9 },
                    { 37, 0, 1, "Interior", 20, 10 },
                    { 38, 0, 2, "Ocean View", 15, 10 },
                    { 39, 0, 3, "Balcony", 10, 10 },
                    { 40, 0, 4, "Suite", 5, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabinAvailabilities");
        }
    }
}
