using System.ComponentModel.DataAnnotations;

namespace TourBooking.Core.DTOs.Inputs;

public sealed class RegisterCompanyInputModel
{
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Handle (example.com/handle)")]
    public string Handle { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Logo URL")]
    public string LogoUrl { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(100, MinimumLength = 3, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Website")]
    public string? Website { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Street name and building number")]
    public string StreetName { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "City")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [Display(Name = "ZIP Code")]
    public int? ZipCode { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(75, MinimumLength = 3, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Headquarter package name / duration")]
    public string PackageName { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Headquarter package description")]
    public string PackageDescription { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Primary text color")]
    public string PrimaryTextColor { get; set; } = "#000000";

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Primary background color")]
    public string PrimaryBackgroundColor { get; set; } = "#F2F2F2";

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Primary container color")]
    public string PrimaryContainerColor { get; set; } = "#D3D3D3";

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Primary button color")]
    public string PrimaryButtonColor { get; set; } = "#0F52BA";

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Primary button text color")]
    public string PrimaryButtonTextColor { get; set; } = "#FFFFFF";

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Primary navigation menu background color")]
    public string PrimaryNavigationBackgroundColor { get; set; } = "#0F52BA";

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(8, MinimumLength = 4, ErrorMessage = "{0} must be between {2}-{1} characters long.")]
    [Display(Name = "Primary navigation menu button text color")]
    public string PrimaryNavigationButtonTextColor { get; set; } = "#FFFFFF";
}