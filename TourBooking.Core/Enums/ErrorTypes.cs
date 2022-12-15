namespace TourBooking.Core.Enums;

public enum LoginErrorType
{
    CouldNotFindApplicationUser = 1,
    ApplicationUserIsLockedOut = 2,
    InvalidPassword = 3,
    CouldNotUpdateApplicationUser = 4,
}

public enum RefreshTokensErrorType
{
    AccessOrRefreshTokenIsNullOrWhitespace = 5,
    CouldNotValidateAccessToken = 6,
    InvalidRefreshToken = 7,
    CouldNotFindApplicationUser = 8,
    CouldNotUpdateApplicationUser = 9,
}

public enum RegisterAdminErrorType
{
    EmailAlreadyExists = 10,
    CouldNotCreateApplicationUser = 11,
    CouldNotFindApplicationUser = 12,
    CouldNotCreateAdmin = 13,
    InvalidUserName = 14,
}

public enum RegisterEmployeeErrorType
{
    EmailAlreadyExists = 15,
    CouldNotCreateApplicationUser = 16,
    CouldNotFindApplicationUser = 17,
    CouldNotCreateEmployee = 18,
    InvalidUserName = 19,
    CouldNotFindCompany = 20,
}

public enum GeneralErrorType
{
    OperationWasCanceled = 20,
    Unhandled = 21,
    OptimisticConcurrency = 22,
}

public enum GenericErrorType
{
    CouldNotFindEntity = 23,
    CouldNotCreateEntity = 24,
    CouldNotUpdateEntity = 25,
    CouldNotDeleteEntity = 26,
}

public enum RegisterCompanyErrorType
{
    CouldNotCreateCompany = 27,
    CouldNotCreateHeadquarter = 28,
    CouldNotCreatePrimaryTheme = 29,
    CouldNotCreatePackage = 30,
}

public enum LoadCompanyThemeErrorType
{
    CouldNotFindCompany = 30,
}

public enum CreateLocationErrorType
{
    CouldNotCreateLocation = 31,
}

public enum CreatePackageErrorType
{
    CouldNotCreatePackage = 32,
}

public enum CreateMaterialErrorType
{
    CouldNotCreateMaterial = 33,
}

public enum GetCompanyDetailsErrorType
{
    CouldNotFindCompany = 34,
}

public enum CreateBookingErrorType
{
    InvalidUserName = 35,
    CouldNotCreateApplicationUser = 36,
    CouldNotFindApplicationUser = 37,
    CouldNotCreateBookerNewUser = 38,
    CouldNotCreateBookerExistingUser = 39,
    CouldNotCreateBooking = 40,
    CouldNotUpdateMaterial = 41,
    CouldNotCreateMessage = 42,
    CouldNotSendEmail = 43,
    EmailNotConfirmed = 44,
}

public enum GetBookingDetailsErrorType
{
    CouldNotFindBooking = 42,
}

public enum ToggleBookingStatusErrorType
{
    CouldNotUpdateBooking = 43,
}

public enum CreateMessageErrorType
{
    CouldNotFindBooking = 44,
}

public enum ConfirmEmailErrorType
{
    CouldNotFindApplicationUser = 45,
    EmailAlreadyConfirmed = 46,
    CouldNotConfirmEmail = 47,
}

public enum RegisterBookerErrorType
{
    EmailAlreadyExists = 48,
    InvalidUserName = 49,
    CouldNotCreateApplicationUser = 50,
    CouldNotGetApplicationUser = 51,
    CouldNotCreateBooker = 52,
}