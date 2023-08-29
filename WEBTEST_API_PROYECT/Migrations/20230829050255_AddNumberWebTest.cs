using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEBTEST_API_PROYECT.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberWebTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumberTestWebs",
                columns: table => new
                {
                    TestWebNo = table.Column<int>(type: "int", nullable: false),
                    TestWebId = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatingTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberTestWebs", x => x.TestWebNo);
                    table.ForeignKey(
                        name: "FK_NumberTestWebs_TestWebs_TestWebId",
                        column: x => x.TestWebId,
                        principalTable: "TestWebs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "TestWebs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreation", "DateUpdating" },
                values: new object[] { new DateTime(2023, 8, 29, 1, 2, 55, 464, DateTimeKind.Local).AddTicks(2205), new DateTime(2023, 8, 29, 1, 2, 55, 464, DateTimeKind.Local).AddTicks(2245) });

            migrationBuilder.UpdateData(
                table: "TestWebs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreation", "DateUpdating" },
                values: new object[] { new DateTime(2023, 8, 29, 5, 2, 55, 464, DateTimeKind.Utc).AddTicks(2248), new DateTime(2023, 8, 29, 5, 2, 55, 464, DateTimeKind.Utc).AddTicks(2248) });

            migrationBuilder.CreateIndex(
                name: "IX_NumberTestWebs_TestWebId",
                table: "NumberTestWebs",
                column: "TestWebId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberTestWebs");

            migrationBuilder.UpdateData(
                table: "TestWebs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreation", "DateUpdating" },
                values: new object[] { new DateTime(2023, 8, 28, 0, 16, 44, 56, DateTimeKind.Local).AddTicks(7301), new DateTime(2023, 8, 28, 0, 16, 44, 56, DateTimeKind.Local).AddTicks(7343) });

            migrationBuilder.UpdateData(
                table: "TestWebs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreation", "DateUpdating" },
                values: new object[] { new DateTime(2023, 8, 28, 4, 16, 44, 56, DateTimeKind.Utc).AddTicks(7347), new DateTime(2023, 8, 28, 4, 16, 44, 56, DateTimeKind.Utc).AddTicks(7347) });
        }
    }
}
