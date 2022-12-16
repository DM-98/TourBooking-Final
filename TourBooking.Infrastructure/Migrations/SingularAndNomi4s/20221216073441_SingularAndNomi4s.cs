using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourBooking.Infrastructure.Migrations.SingularAndNomi4s
{
    /// <inheritdoc />
    public partial class SingularAndNomi4s : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Users_ApplicationUserId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookers_Users_ApplicationUserId",
                table: "Bookers");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Bookers_BookerId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Packages_PackageId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Companies_CompanyId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_ApplicationUserId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Bookings_BookingId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Companies_CompanyId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Bookings_BookingId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ApplicationUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Companies_CompanyId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Locations_LocationId",
                table: "Packages");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesClaims_Roles_RoleId",
                table: "RolesClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_Themes_Companies_CompanyId",
                table: "Themes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersClaims_Users_UserId",
                table: "UsersClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersLogins_Users_UserId",
                table: "UsersLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoles_Roles_RoleId",
                table: "UsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoles_Users_UserId",
                table: "UsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTokens_Users_UserId",
                table: "UsersTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Themes",
                table: "Themes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packages",
                table: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Materials",
                table: "Materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataProtectionKeys",
                table: "DataProtectionKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookers",
                table: "Bookers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Themes",
                newName: "Theme");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Packages",
                newName: "Package");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameTable(
                name: "Materials",
                newName: "Material");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "Log");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "DataProtectionKeys",
                newName: "DataProtectionKey");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Booking");

            migrationBuilder.RenameTable(
                name: "Bookers",
                newName: "Booker");

            migrationBuilder.RenameTable(
                name: "Admins",
                newName: "Admin");

            migrationBuilder.RenameIndex(
                name: "IX_Themes_CompanyId",
                table: "Theme",
                newName: "IX_Theme_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_LocationId",
                table: "Package",
                newName: "IX_Package_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_CompanyId",
                table: "Package",
                newName: "IX_Package_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_BookingId",
                table: "Message",
                newName: "IX_Message_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ApplicationUserId",
                table: "Message",
                newName: "IX_Message_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_CompanyId",
                table: "Material",
                newName: "IX_Material_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_BookingId",
                table: "Material",
                newName: "IX_Material_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_ApplicationUserId",
                table: "Log",
                newName: "IX_Log_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_CompanyId",
                table: "Location",
                newName: "IX_Location_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_CompanyId",
                table: "Employee",
                newName: "IX_Employee_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employee",
                newName: "IX_Employee_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_PackageId",
                table: "Booking",
                newName: "IX_Booking_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_BookerId",
                table: "Booking",
                newName: "IX_Booking_BookerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookers_ApplicationUserId",
                table: "Booker",
                newName: "IX_Booker_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Admins_ApplicationUserId",
                table: "Admin",
                newName: "IX_Admin_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Theme",
                table: "Theme",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Package",
                table: "Package",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Log",
                table: "Log",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataProtectionKey",
                table: "DataProtectionKey",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booking",
                table: "Booking",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Booker",
                table: "Booker",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admin",
                table: "Admin",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Nomi4sBooking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgeGroup = table.Column<int>(type: "int", nullable: false),
                    SchoolGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTransportPaymentRequested = table.Column<bool>(type: "bit", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nomi4sBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nomi4sBooking_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nomi4sBooking_BookingId",
                table: "Nomi4sBooking",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_User_ApplicationUserId",
                table: "Admin",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booker_User_ApplicationUserId",
                table: "Booker",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Booker_BookerId",
                table: "Booking",
                column: "BookerId",
                principalTable: "Booker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Package_PackageId",
                table: "Booking",
                column: "PackageId",
                principalTable: "Package",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Company_CompanyId",
                table: "Employee",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_User_ApplicationUserId",
                table: "Employee",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Company_CompanyId",
                table: "Location",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Log_User_ApplicationUserId",
                table: "Log",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Booking_BookingId",
                table: "Material",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Company_CompanyId",
                table: "Material",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Booking_BookingId",
                table: "Message",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_ApplicationUserId",
                table: "Message",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Company_CompanyId",
                table: "Package",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Location_LocationId",
                table: "Package",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesClaims_Role_RoleId",
                table: "RolesClaims",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Company_CompanyId",
                table: "Theme",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersClaims_User_UserId",
                table: "UsersClaims",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersLogins_User_UserId",
                table: "UsersLogins",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_Role_RoleId",
                table: "UsersRoles",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_User_UserId",
                table: "UsersRoles",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTokens_User_UserId",
                table: "UsersTokens",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_User_ApplicationUserId",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Booker_User_ApplicationUserId",
                table: "Booker");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Booker_BookerId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Package_PackageId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Company_CompanyId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_User_ApplicationUserId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Company_CompanyId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_Log_User_ApplicationUserId",
                table: "Log");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Booking_BookingId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Material_Company_CompanyId",
                table: "Material");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Booking_BookingId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_ApplicationUserId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Company_CompanyId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Location_LocationId",
                table: "Package");

            migrationBuilder.DropForeignKey(
                name: "FK_RolesClaims_Role_RoleId",
                table: "RolesClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Company_CompanyId",
                table: "Theme");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersClaims_User_UserId",
                table: "UsersClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersLogins_User_UserId",
                table: "UsersLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoles_Role_RoleId",
                table: "UsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoles_User_UserId",
                table: "UsersRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersTokens_User_UserId",
                table: "UsersTokens");

            migrationBuilder.DropTable(
                name: "Nomi4sBooking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Theme",
                table: "Theme");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Package",
                table: "Package");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Log",
                table: "Log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataProtectionKey",
                table: "DataProtectionKey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booking",
                table: "Booking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Booker",
                table: "Booker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admin",
                table: "Admin");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Theme",
                newName: "Themes");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Package",
                newName: "Packages");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "Materials");

            migrationBuilder.RenameTable(
                name: "Log",
                newName: "Logs");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "DataProtectionKey",
                newName: "DataProtectionKeys");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.RenameTable(
                name: "Booking",
                newName: "Bookings");

            migrationBuilder.RenameTable(
                name: "Booker",
                newName: "Bookers");

            migrationBuilder.RenameTable(
                name: "Admin",
                newName: "Admins");

            migrationBuilder.RenameIndex(
                name: "IX_Theme_CompanyId",
                table: "Themes",
                newName: "IX_Themes_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_LocationId",
                table: "Packages",
                newName: "IX_Packages_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Package_CompanyId",
                table: "Packages",
                newName: "IX_Packages_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_BookingId",
                table: "Messages",
                newName: "IX_Messages_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ApplicationUserId",
                table: "Messages",
                newName: "IX_Messages_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_CompanyId",
                table: "Materials",
                newName: "IX_Materials_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_BookingId",
                table: "Materials",
                newName: "IX_Materials_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Log_ApplicationUserId",
                table: "Logs",
                newName: "IX_Logs_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Location_CompanyId",
                table: "Locations",
                newName: "IX_Locations_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_CompanyId",
                table: "Employees",
                newName: "IX_Employees_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_ApplicationUserId",
                table: "Employees",
                newName: "IX_Employees_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_PackageId",
                table: "Bookings",
                newName: "IX_Bookings_PackageId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_BookerId",
                table: "Bookings",
                newName: "IX_Bookings_BookerId");

            migrationBuilder.RenameIndex(
                name: "IX_Booker_ApplicationUserId",
                table: "Bookers",
                newName: "IX_Bookers_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Admin_ApplicationUserId",
                table: "Admins",
                newName: "IX_Admins_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Themes",
                table: "Themes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packages",
                table: "Packages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Materials",
                table: "Materials",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataProtectionKeys",
                table: "DataProtectionKeys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookers",
                table: "Bookers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Users_ApplicationUserId",
                table: "Admins",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookers_Users_ApplicationUserId",
                table: "Bookers",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Bookers_BookerId",
                table: "Bookings",
                column: "BookerId",
                principalTable: "Bookers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Packages_PackageId",
                table: "Bookings",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_CompanyId",
                table: "Employees",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Companies_CompanyId",
                table: "Locations",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_ApplicationUserId",
                table: "Logs",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Bookings_BookingId",
                table: "Materials",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Companies_CompanyId",
                table: "Materials",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Bookings_BookingId",
                table: "Messages",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ApplicationUserId",
                table: "Messages",
                column: "ApplicationUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Companies_CompanyId",
                table: "Packages",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Locations_LocationId",
                table: "Packages",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolesClaims_Roles_RoleId",
                table: "RolesClaims",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Themes_Companies_CompanyId",
                table: "Themes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersClaims_Users_UserId",
                table: "UsersClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersLogins_Users_UserId",
                table: "UsersLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_Roles_RoleId",
                table: "UsersRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoles_Users_UserId",
                table: "UsersRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersTokens_Users_UserId",
                table: "UsersTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
