using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortMan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddResortDbScheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccomodationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomodationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccomodationsPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccomodationTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfRoom = table.Column<int>(type: "int", nullable: false),
                    FeePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomodationsPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccomodationsPackages_AccomodationTypes_AccomodationTypeId",
                        column: x => x.AccomodationTypeId,
                        principalTable: "AccomodationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accomodations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccomodationPackageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accomodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accomodations_AccomodationsPackages_AccomodationPackageId",
                        column: x => x.AccomodationPackageId,
                        principalTable: "AccomodationsPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccomodationId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Accomodations_AccomodationId",
                        column: x => x.AccomodationId,
                        principalTable: "Accomodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accomodations_AccomodationPackageId",
                table: "Accomodations",
                column: "AccomodationPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_AccomodationsPackages_AccomodationTypeId",
                table: "AccomodationsPackages",
                column: "AccomodationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AccomodationId",
                table: "Bookings",
                column: "AccomodationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Accomodations");

            migrationBuilder.DropTable(
                name: "AccomodationsPackages");

            migrationBuilder.DropTable(
                name: "AccomodationTypes");
        }
    }
}
