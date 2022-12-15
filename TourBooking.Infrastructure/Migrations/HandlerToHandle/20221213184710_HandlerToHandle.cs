using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourBooking.Infrastructure.Migrations.HandlerToHandle
{
    /// <inheritdoc />
    public partial class HandlerToHandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Handler",
                table: "Companies",
                newName: "Handle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Handle",
                table: "Companies",
                newName: "Handler");
        }
    }
}
