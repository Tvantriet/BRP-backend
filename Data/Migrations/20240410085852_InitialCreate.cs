using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startLat = table.Column<double>(type: "float", nullable: false),
                    startLong = table.Column<double>(type: "float", nullable: false),
                    startCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endLat = table.Column<double>(type: "float", nullable: false),
                    endLong = table.Column<double>(type: "float", nullable: false),
                    endCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    distance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
