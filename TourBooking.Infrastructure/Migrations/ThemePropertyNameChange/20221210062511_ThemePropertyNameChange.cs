using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourBooking.Infrastructure.Migrations.ThemePropertyNameChange
{
	/// <inheritdoc />
	public partial class ThemePropertyNameChange : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "NavigationTextColor",
				table: "Themes",
				newName: "NavigationButtonTextColor");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "NavigationButtonTextColor",
				table: "Themes",
				newName: "NavigationTextColor");
		}
	}
}