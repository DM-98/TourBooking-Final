using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourBooking.Infrastructure.Migrations.Initial
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Companies",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Handler = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Companies", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "DataProtectionKeys",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Xml = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Roles",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Roles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					IsEmailNotificationEnabled = table.Column<bool>(type: "bit", nullable: false),
					RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
					RefreshTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
					DeletionRequestedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RoleType = table.Column<int>(type: "int", nullable: false),
					UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
					PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
					SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
					TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
					LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
					AccessFailedCount = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Locations",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					StreetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					City = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ZipCode = table.Column<int>(type: "int", nullable: false),
					IsHeadquarter = table.Column<bool>(type: "bit", nullable: false),
					CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Locations", x => x.Id);
					table.ForeignKey(
						name: "FK_Locations_Companies_CompanyId",
						column: x => x.CompanyId,
						principalTable: "Companies",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "Themes",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					IsPrimary = table.Column<bool>(type: "bit", nullable: false),
					TextColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
					BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ContainerColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ButtonColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ButtonTextColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
					NavigationBackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
					NavigationTextColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Themes", x => x.Id);
					table.ForeignKey(
						name: "FK_Themes_Companies_CompanyId",
						column: x => x.CompanyId,
						principalTable: "Companies",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "RolesClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RolesClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_RolesClaims_Roles_RoleId",
						column: x => x.RoleId,
						principalTable: "Roles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Admins",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Admins", x => x.Id);
					table.ForeignKey(
						name: "FK_Admins_Users_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Bookers",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Bookers", x => x.Id);
					table.ForeignKey(
						name: "FK_Bookers_Users_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Employees",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Employees", x => x.Id);
					table.ForeignKey(
						name: "FK_Employees_Companies_CompanyId",
						column: x => x.CompanyId,
						principalTable: "Companies",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Employees_Users_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Logs",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Page = table.Column<string>(type: "nvarchar(max)", nullable: false),
					ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Logs", x => x.Id);
					table.ForeignKey(
						name: "FK_Logs_Users_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "UsersClaims",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UsersClaims", x => x.Id);
					table.ForeignKey(
						name: "FK_UsersClaims_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UsersLogins",
				columns: table => new
				{
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
					ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UsersLogins", x => new { x.LoginProvider, x.ProviderKey });
					table.ForeignKey(
						name: "FK_UsersLogins_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UsersRoles",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UsersRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_UsersRoles_Roles_RoleId",
						column: x => x.RoleId,
						principalTable: "Roles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_UsersRoles_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UsersTokens",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UsersTokens", x => new { x.UserId, x.LoginProvider, x.Name });
					table.ForeignKey(
						name: "FK_UsersTokens_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Packages",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Packages", x => x.Id);
					table.ForeignKey(
						name: "FK_Packages_Companies_CompanyId",
						column: x => x.CompanyId,
						principalTable: "Companies",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Packages_Locations_LocationId",
						column: x => x.LocationId,
						principalTable: "Locations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Bookings",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
					AlternativeDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
					Attendees = table.Column<int>(type: "int", nullable: false),
					Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
					BookingStatus = table.Column<int>(type: "int", nullable: false),
					PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					BookerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Bookings", x => x.Id);
					table.ForeignKey(
						name: "FK_Bookings_Bookers_BookerId",
						column: x => x.BookerId,
						principalTable: "Bookers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Bookings_Packages_PackageId",
						column: x => x.PackageId,
						principalTable: "Packages",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "Materials",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Materials", x => x.Id);
					table.ForeignKey(
						name: "FK_Materials_Bookings_BookingId",
						column: x => x.BookingId,
						principalTable: "Bookings",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Materials_Companies_CompanyId",
						column: x => x.CompanyId,
						principalTable: "Companies",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Messages",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
					BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Messages", x => x.Id);
					table.ForeignKey(
						name: "FK_Messages_Bookings_BookingId",
						column: x => x.BookingId,
						principalTable: "Bookings",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Messages_Users_ApplicationUserId",
						column: x => x.ApplicationUserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Admins_ApplicationUserId",
				table: "Admins",
				column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Bookers_ApplicationUserId",
				table: "Bookers",
				column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Bookings_BookerId",
				table: "Bookings",
				column: "BookerId");

			migrationBuilder.CreateIndex(
				name: "IX_Bookings_PackageId",
				table: "Bookings",
				column: "PackageId");

			migrationBuilder.CreateIndex(
				name: "IX_Employees_ApplicationUserId",
				table: "Employees",
				column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Employees_CompanyId",
				table: "Employees",
				column: "CompanyId");

			migrationBuilder.CreateIndex(
				name: "IX_Locations_CompanyId",
				table: "Locations",
				column: "CompanyId");

			migrationBuilder.CreateIndex(
				name: "IX_Logs_ApplicationUserId",
				table: "Logs",
				column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Materials_BookingId",
				table: "Materials",
				column: "BookingId");

			migrationBuilder.CreateIndex(
				name: "IX_Materials_CompanyId",
				table: "Materials",
				column: "CompanyId");

			migrationBuilder.CreateIndex(
				name: "IX_Messages_ApplicationUserId",
				table: "Messages",
				column: "ApplicationUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Messages_BookingId",
				table: "Messages",
				column: "BookingId");

			migrationBuilder.CreateIndex(
				name: "IX_Packages_CompanyId",
				table: "Packages",
				column: "CompanyId");

			migrationBuilder.CreateIndex(
				name: "IX_Packages_LocationId",
				table: "Packages",
				column: "LocationId");

			migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				table: "Roles",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_RolesClaims_RoleId",
				table: "RolesClaims",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "IX_Themes_CompanyId",
				table: "Themes",
				column: "CompanyId");

			migrationBuilder.CreateIndex(
				name: "EmailIndex",
				table: "Users",
				column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				table: "Users",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
				name: "IX_UsersClaims_UserId",
				table: "UsersClaims",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_UsersLogins_UserId",
				table: "UsersLogins",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_UsersRoles_RoleId",
				table: "UsersRoles",
				column: "RoleId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Admins");

			migrationBuilder.DropTable(
				name: "DataProtectionKeys");

			migrationBuilder.DropTable(
				name: "Employees");

			migrationBuilder.DropTable(
				name: "Logs");

			migrationBuilder.DropTable(
				name: "Materials");

			migrationBuilder.DropTable(
				name: "Messages");

			migrationBuilder.DropTable(
				name: "RolesClaims");

			migrationBuilder.DropTable(
				name: "Themes");

			migrationBuilder.DropTable(
				name: "UsersClaims");

			migrationBuilder.DropTable(
				name: "UsersLogins");

			migrationBuilder.DropTable(
				name: "UsersRoles");

			migrationBuilder.DropTable(
				name: "UsersTokens");

			migrationBuilder.DropTable(
				name: "Bookings");

			migrationBuilder.DropTable(
				name: "Roles");

			migrationBuilder.DropTable(
				name: "Bookers");

			migrationBuilder.DropTable(
				name: "Packages");

			migrationBuilder.DropTable(
				name: "Users");

			migrationBuilder.DropTable(
				name: "Locations");

			migrationBuilder.DropTable(
				name: "Companies");
		}
	}
}