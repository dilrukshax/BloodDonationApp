using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateeventreport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    ParticipatedPeopleCount = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    APositive = table.Column<int>(type: "int", nullable: false),
                    BPositive = table.Column<int>(type: "int", nullable: false),
                    ABPositive = table.Column<int>(type: "int", nullable: false),
                    OPositive = table.Column<int>(type: "int", nullable: false),
                    ANegative = table.Column<int>(type: "int", nullable: false),
                    BNegative = table.Column<int>(type: "int", nullable: false),
                    ABNegative = table.Column<int>(type: "int", nullable: false),
                    ONegative = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_EventReports_EventId",
                table: "EventReports",
                column: "EventId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventReports");
        }
    }
}
