using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WEBTEST_API_PROYECT.Migrations
{
    /// <inheritdoc />
    public partial class feedTableWebTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TestWebs",
                columns: new[] { "Id", "Amenity", "DateCreation", "DateUpdating", "Detail", "Fee", "ImageUrl", "Name", "Pages", "SquareMeters" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 8, 28, 0, 16, 44, 56, DateTimeKind.Local).AddTicks(7301), new DateTime(2023, 8, 28, 0, 16, 44, 56, DateTimeKind.Local).AddTicks(7343), "Detalle de la villa", 500.0, "", "Villa real", 5, 200 },
                    { 2, "Swimming pool, beach access", new DateTime(2023, 8, 28, 4, 16, 44, 56, DateTimeKind.Utc).AddTicks(7347), new DateTime(2023, 8, 28, 4, 16, 44, 56, DateTimeKind.Utc).AddTicks(7347), "A sunny vacation spot by the beach", 800.0, "https://example.com/sunny-shores.jpg", "Sunny Shores", 10, 400 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TestWebs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TestWebs",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
