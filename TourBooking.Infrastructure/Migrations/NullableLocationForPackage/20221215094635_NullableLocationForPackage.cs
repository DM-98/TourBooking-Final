using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourBooking.Infrastructure.Migrations.NullableLocationForPackage
{
    /// <inheritdoc />
    public partial class NullableLocationForPackage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Locations_LocationId",
                table: "Packages");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Packages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Locations_LocationId",
                table: "Packages",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Locations_LocationId",
                table: "Packages");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Packages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Locations_LocationId",
                table: "Packages",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
