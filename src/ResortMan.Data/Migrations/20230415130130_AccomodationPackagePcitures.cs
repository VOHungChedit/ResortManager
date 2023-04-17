using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResortMan.Data.Migrations
{
    /// <inheritdoc />
    public partial class AccomodationPackagePcitures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccomodationPackagePictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AccomodationPackageId = table.Column<int>(type: "int", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccomodationPackagePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccomodationPackagePictures_AccomodationsPackages_AccomodationPackageId",
                        column: x => x.AccomodationPackageId,
                        principalTable: "AccomodationsPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccomodationPackagePictures_AccomodationPackageId",
                table: "AccomodationPackagePictures",
                column: "AccomodationPackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccomodationPackagePictures");
        }
    }
}
