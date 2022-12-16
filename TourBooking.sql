IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Companies] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Handler] nvarchar(max) NOT NULL,
    [LogoUrl] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Website] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Companies] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DataProtectionKeys] (
    [Id] int NOT NULL IDENTITY,
    [FriendlyName] nvarchar(max) NULL,
    [Xml] nvarchar(max) NULL,
    CONSTRAINT [PK_DataProtectionKeys] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Roles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [IsEmailNotificationEnabled] bit NOT NULL,
    [RefreshToken] nvarchar(max) NULL,
    [RefreshTokenExpiry] datetime2 NULL,
    [DeletionRequestedDate] datetime2 NULL,
    [RoleType] int NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Locations] (
    [Id] uniqueidentifier NOT NULL,
    [StreetName] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [ZipCode] int NOT NULL,
    [IsHeadquarter] bit NOT NULL,
    [CompanyId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Locations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Locations_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id])
);
GO

CREATE TABLE [Themes] (
    [Id] uniqueidentifier NOT NULL,
    [IsPrimary] bit NOT NULL,
    [TextColor] nvarchar(max) NOT NULL,
    [BackgroundColor] nvarchar(max) NOT NULL,
    [ContainerColor] nvarchar(max) NOT NULL,
    [ButtonColor] nvarchar(max) NOT NULL,
    [ButtonTextColor] nvarchar(max) NOT NULL,
    [NavigationBackgroundColor] nvarchar(max) NOT NULL,
    [NavigationTextColor] nvarchar(max) NOT NULL,
    [CompanyId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Themes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Themes_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RolesClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RolesClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RolesClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Admins] (
    [Id] uniqueidentifier NOT NULL,
    [ApplicationUserId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Admins] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Admins_Users_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Bookers] (
    [Id] uniqueidentifier NOT NULL,
    [Organization] nvarchar(max) NOT NULL,
    [ApplicationUserId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Bookers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookers_Users_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Employees] (
    [Id] uniqueidentifier NOT NULL,
    [CompanyId] uniqueidentifier NOT NULL,
    [ApplicationUserId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Employees_Users_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Logs] (
    [Id] uniqueidentifier NOT NULL,
    [Action] nvarchar(max) NOT NULL,
    [Page] nvarchar(max) NOT NULL,
    [ApplicationUserId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Logs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Logs_Users_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [UsersClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UsersClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UsersClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UsersLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UsersLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UsersLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UsersRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UsersRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UsersRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UsersRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UsersTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UsersTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UsersTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Packages] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [LocationId] uniqueidentifier NOT NULL,
    [CompanyId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Packages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Packages_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Packages_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Bookings] (
    [Id] uniqueidentifier NOT NULL,
    [DateTime] datetime2 NOT NULL,
    [AlternativeDateTime] datetime2 NOT NULL,
    [Attendees] int NOT NULL,
    [Remark] nvarchar(max) NULL,
    [BookingStatus] int NOT NULL,
    [PackageId] uniqueidentifier NOT NULL,
    [BookerId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookings_Bookers_BookerId] FOREIGN KEY ([BookerId]) REFERENCES [Bookers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Packages_PackageId] FOREIGN KEY ([PackageId]) REFERENCES [Packages] ([Id])
);
GO

CREATE TABLE [Materials] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CompanyId] uniqueidentifier NOT NULL,
    [BookingId] uniqueidentifier NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Materials] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Materials_Bookings_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Bookings] ([Id]),
    CONSTRAINT [FK_Materials_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Companies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Messages] (
    [Id] uniqueidentifier NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [BookingId] uniqueidentifier NOT NULL,
    [ApplicationUserId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Messages_Bookings_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Bookings] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Messages_Users_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Users] ([Id])
);
GO

CREATE INDEX [IX_Admins_ApplicationUserId] ON [Admins] ([ApplicationUserId]);
GO

CREATE INDEX [IX_Bookers_ApplicationUserId] ON [Bookers] ([ApplicationUserId]);
GO

CREATE INDEX [IX_Bookings_BookerId] ON [Bookings] ([BookerId]);
GO

CREATE INDEX [IX_Bookings_PackageId] ON [Bookings] ([PackageId]);
GO

CREATE INDEX [IX_Employees_ApplicationUserId] ON [Employees] ([ApplicationUserId]);
GO

CREATE INDEX [IX_Employees_CompanyId] ON [Employees] ([CompanyId]);
GO

CREATE INDEX [IX_Locations_CompanyId] ON [Locations] ([CompanyId]);
GO

CREATE INDEX [IX_Logs_ApplicationUserId] ON [Logs] ([ApplicationUserId]);
GO

CREATE INDEX [IX_Materials_BookingId] ON [Materials] ([BookingId]);
GO

CREATE INDEX [IX_Materials_CompanyId] ON [Materials] ([CompanyId]);
GO

CREATE INDEX [IX_Messages_ApplicationUserId] ON [Messages] ([ApplicationUserId]);
GO

CREATE INDEX [IX_Messages_BookingId] ON [Messages] ([BookingId]);
GO

CREATE INDEX [IX_Packages_CompanyId] ON [Packages] ([CompanyId]);
GO

CREATE INDEX [IX_Packages_LocationId] ON [Packages] ([LocationId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_RolesClaims_RoleId] ON [RolesClaims] ([RoleId]);
GO

CREATE INDEX [IX_Themes_CompanyId] ON [Themes] ([CompanyId]);
GO

CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_UsersClaims_UserId] ON [UsersClaims] ([UserId]);
GO

CREATE INDEX [IX_UsersLogins_UserId] ON [UsersLogins] ([UserId]);
GO

CREATE INDEX [IX_UsersRoles_RoleId] ON [UsersRoles] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221210050756_Initial', N'7.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Themes].[NavigationTextColor]', N'NavigationButtonTextColor', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221210062511_ThemePropertyNameChange', N'7.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Companies].[Handler]', N'Handle', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221213184710_HandlerToHandle', N'7.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Packages] DROP CONSTRAINT [FK_Packages_Locations_LocationId];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Packages]') AND [c].[name] = N'LocationId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Packages] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Packages] ALTER COLUMN [LocationId] uniqueidentifier NULL;
GO

ALTER TABLE [Packages] ADD CONSTRAINT [FK_Packages_Locations_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Locations] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221215094635_NullableLocationForPackage', N'7.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bookings]') AND [c].[name] = N'AlternativeDateTime');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Bookings] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Bookings] ALTER COLUMN [AlternativeDateTime] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221215133647_AddNullableLocation', N'7.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Admins] DROP CONSTRAINT [FK_Admins_Users_ApplicationUserId];
GO

ALTER TABLE [Bookers] DROP CONSTRAINT [FK_Bookers_Users_ApplicationUserId];
GO

ALTER TABLE [Bookings] DROP CONSTRAINT [FK_Bookings_Bookers_BookerId];
GO

ALTER TABLE [Bookings] DROP CONSTRAINT [FK_Bookings_Packages_PackageId];
GO

ALTER TABLE [Employees] DROP CONSTRAINT [FK_Employees_Companies_CompanyId];
GO

ALTER TABLE [Employees] DROP CONSTRAINT [FK_Employees_Users_ApplicationUserId];
GO

ALTER TABLE [Locations] DROP CONSTRAINT [FK_Locations_Companies_CompanyId];
GO

ALTER TABLE [Logs] DROP CONSTRAINT [FK_Logs_Users_ApplicationUserId];
GO

ALTER TABLE [Materials] DROP CONSTRAINT [FK_Materials_Bookings_BookingId];
GO

ALTER TABLE [Materials] DROP CONSTRAINT [FK_Materials_Companies_CompanyId];
GO

ALTER TABLE [Messages] DROP CONSTRAINT [FK_Messages_Bookings_BookingId];
GO

ALTER TABLE [Messages] DROP CONSTRAINT [FK_Messages_Users_ApplicationUserId];
GO

ALTER TABLE [Packages] DROP CONSTRAINT [FK_Packages_Companies_CompanyId];
GO

ALTER TABLE [Packages] DROP CONSTRAINT [FK_Packages_Locations_LocationId];
GO

ALTER TABLE [RolesClaims] DROP CONSTRAINT [FK_RolesClaims_Roles_RoleId];
GO

ALTER TABLE [Themes] DROP CONSTRAINT [FK_Themes_Companies_CompanyId];
GO

ALTER TABLE [UsersClaims] DROP CONSTRAINT [FK_UsersClaims_Users_UserId];
GO

ALTER TABLE [UsersLogins] DROP CONSTRAINT [FK_UsersLogins_Users_UserId];
GO

ALTER TABLE [UsersRoles] DROP CONSTRAINT [FK_UsersRoles_Roles_RoleId];
GO

ALTER TABLE [UsersRoles] DROP CONSTRAINT [FK_UsersRoles_Users_UserId];
GO

ALTER TABLE [UsersTokens] DROP CONSTRAINT [FK_UsersTokens_Users_UserId];
GO

ALTER TABLE [Users] DROP CONSTRAINT [PK_Users];
GO

ALTER TABLE [Themes] DROP CONSTRAINT [PK_Themes];
GO

ALTER TABLE [Roles] DROP CONSTRAINT [PK_Roles];
GO

ALTER TABLE [Packages] DROP CONSTRAINT [PK_Packages];
GO

ALTER TABLE [Messages] DROP CONSTRAINT [PK_Messages];
GO

ALTER TABLE [Materials] DROP CONSTRAINT [PK_Materials];
GO

ALTER TABLE [Logs] DROP CONSTRAINT [PK_Logs];
GO

ALTER TABLE [Locations] DROP CONSTRAINT [PK_Locations];
GO

ALTER TABLE [Employees] DROP CONSTRAINT [PK_Employees];
GO

ALTER TABLE [DataProtectionKeys] DROP CONSTRAINT [PK_DataProtectionKeys];
GO

ALTER TABLE [Companies] DROP CONSTRAINT [PK_Companies];
GO

ALTER TABLE [Bookings] DROP CONSTRAINT [PK_Bookings];
GO

ALTER TABLE [Bookers] DROP CONSTRAINT [PK_Bookers];
GO

ALTER TABLE [Admins] DROP CONSTRAINT [PK_Admins];
GO

EXEC sp_rename N'[Users]', N'User';
GO

EXEC sp_rename N'[Themes]', N'Theme';
GO

EXEC sp_rename N'[Roles]', N'Role';
GO

EXEC sp_rename N'[Packages]', N'Package';
GO

EXEC sp_rename N'[Messages]', N'Message';
GO

EXEC sp_rename N'[Materials]', N'Material';
GO

EXEC sp_rename N'[Logs]', N'Log';
GO

EXEC sp_rename N'[Locations]', N'Location';
GO

EXEC sp_rename N'[Employees]', N'Employee';
GO

EXEC sp_rename N'[DataProtectionKeys]', N'DataProtectionKey';
GO

EXEC sp_rename N'[Companies]', N'Company';
GO

EXEC sp_rename N'[Bookings]', N'Booking';
GO

EXEC sp_rename N'[Bookers]', N'Booker';
GO

EXEC sp_rename N'[Admins]', N'Admin';
GO

EXEC sp_rename N'[Theme].[IX_Themes_CompanyId]', N'IX_Theme_CompanyId', N'INDEX';
GO

EXEC sp_rename N'[Package].[IX_Packages_LocationId]', N'IX_Package_LocationId', N'INDEX';
GO

EXEC sp_rename N'[Package].[IX_Packages_CompanyId]', N'IX_Package_CompanyId', N'INDEX';
GO

EXEC sp_rename N'[Message].[IX_Messages_BookingId]', N'IX_Message_BookingId', N'INDEX';
GO

EXEC sp_rename N'[Message].[IX_Messages_ApplicationUserId]', N'IX_Message_ApplicationUserId', N'INDEX';
GO

EXEC sp_rename N'[Material].[IX_Materials_CompanyId]', N'IX_Material_CompanyId', N'INDEX';
GO

EXEC sp_rename N'[Material].[IX_Materials_BookingId]', N'IX_Material_BookingId', N'INDEX';
GO

EXEC sp_rename N'[Log].[IX_Logs_ApplicationUserId]', N'IX_Log_ApplicationUserId', N'INDEX';
GO

EXEC sp_rename N'[Location].[IX_Locations_CompanyId]', N'IX_Location_CompanyId', N'INDEX';
GO

EXEC sp_rename N'[Employee].[IX_Employees_CompanyId]', N'IX_Employee_CompanyId', N'INDEX';
GO

EXEC sp_rename N'[Employee].[IX_Employees_ApplicationUserId]', N'IX_Employee_ApplicationUserId', N'INDEX';
GO

EXEC sp_rename N'[Booking].[IX_Bookings_PackageId]', N'IX_Booking_PackageId', N'INDEX';
GO

EXEC sp_rename N'[Booking].[IX_Bookings_BookerId]', N'IX_Booking_BookerId', N'INDEX';
GO

EXEC sp_rename N'[Booker].[IX_Bookers_ApplicationUserId]', N'IX_Booker_ApplicationUserId', N'INDEX';
GO

EXEC sp_rename N'[Admin].[IX_Admins_ApplicationUserId]', N'IX_Admin_ApplicationUserId', N'INDEX';
GO

ALTER TABLE [User] ADD CONSTRAINT [PK_User] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Theme] ADD CONSTRAINT [PK_Theme] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Role] ADD CONSTRAINT [PK_Role] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Package] ADD CONSTRAINT [PK_Package] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Message] ADD CONSTRAINT [PK_Message] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Material] ADD CONSTRAINT [PK_Material] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Log] ADD CONSTRAINT [PK_Log] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Location] ADD CONSTRAINT [PK_Location] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Employee] ADD CONSTRAINT [PK_Employee] PRIMARY KEY ([Id]);
GO

ALTER TABLE [DataProtectionKey] ADD CONSTRAINT [PK_DataProtectionKey] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Company] ADD CONSTRAINT [PK_Company] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Booking] ADD CONSTRAINT [PK_Booking] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Booker] ADD CONSTRAINT [PK_Booker] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Admin] ADD CONSTRAINT [PK_Admin] PRIMARY KEY ([Id]);
GO

CREATE TABLE [Nomi4sBooking] (
    [Id] uniqueidentifier NOT NULL,
    [AgeGroup] int NOT NULL,
    [SchoolGrade] nvarchar(max) NOT NULL,
    [IsTransportPaymentRequested] bit NOT NULL,
    [BookingId] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NULL,
    [RowVersion] rowversion NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Nomi4sBooking] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Nomi4sBooking_Booking_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Booking] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Nomi4sBooking_BookingId] ON [Nomi4sBooking] ([BookingId]);
GO

ALTER TABLE [Admin] ADD CONSTRAINT [FK_Admin_User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Booker] ADD CONSTRAINT [FK_Booker_User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Booking] ADD CONSTRAINT [FK_Booking_Booker_BookerId] FOREIGN KEY ([BookerId]) REFERENCES [Booker] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Booking] ADD CONSTRAINT [FK_Booking_Package_PackageId] FOREIGN KEY ([PackageId]) REFERENCES [Package] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Employee] ADD CONSTRAINT [FK_Employee_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Employee] ADD CONSTRAINT [FK_Employee_User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Location] ADD CONSTRAINT [FK_Location_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Log] ADD CONSTRAINT [FK_Log_User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Material] ADD CONSTRAINT [FK_Material_Booking_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Booking] ([Id]);
GO

ALTER TABLE [Material] ADD CONSTRAINT [FK_Material_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Message] ADD CONSTRAINT [FK_Message_Booking_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Booking] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Message] ADD CONSTRAINT [FK_Message_User_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [User] ([Id]);
GO

ALTER TABLE [Package] ADD CONSTRAINT [FK_Package_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Package] ADD CONSTRAINT [FK_Package_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id]);
GO

ALTER TABLE [RolesClaims] ADD CONSTRAINT [FK_RolesClaims_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Theme] ADD CONSTRAINT [FK_Theme_Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [Company] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [UsersClaims] ADD CONSTRAINT [FK_UsersClaims_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [UsersLogins] ADD CONSTRAINT [FK_UsersLogins_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [UsersRoles] ADD CONSTRAINT [FK_UsersRoles_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [UsersRoles] ADD CONSTRAINT [FK_UsersRoles_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [UsersTokens] ADD CONSTRAINT [FK_UsersTokens_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221216073441_SingularAndNomi4s', N'7.0.1');
GO

COMMIT;
GO

