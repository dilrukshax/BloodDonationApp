using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateevent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodVariantReports");

            migrationBuilder.DropTable(
                name: "EventReports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NumberOfDonors = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventReports_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BloodVariantReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventReportId = table.Column<int>(type: "int", nullable: false),
                    BloodType = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    UnitsCollected = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodVariantReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodVariantReports_EventReports_EventReportId",
                        column: x => x.EventReportId,
                        principalTable: "EventReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodVariantReports_EventReportId",
                table: "BloodVariantReports",
                column: "EventReportId");

            migrationBuilder.CreateIndex(
                name: "IX_EventReports_EventId",
                table: "EventReports",
                column: "EventId",
                unique: true);
        }
    }
}
