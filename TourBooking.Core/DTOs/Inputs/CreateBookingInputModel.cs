using System.ComponentModel.DataAnnotations;

namespace TourBooking.Core.DTOs.Inputs;

public sealed class CreateBookingInputModel
{
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, ErrorMessage = "{0} can only consist of {1} characters.")]
    [Display(Name = "First name")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, ErrorMessage = "{0} can only consist of {1} characters.")]
    [Display(Name = "Last name")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, ErrorMessage = "{0} can only consist of {1} characters.")]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [StringLength(50, ErrorMessage = "{0} can only consist of {1} characters.")]
    [Display(Name = "Phone number")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(50, ErrorMessage = "{0} can only consist of {1} characters.")]
    [Display(Name = "Organization")]
    public string Organization { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [Display(Name = "Attendees")]
    public int? Attendees { get; set; }

    [Display(Name = "Choose package / location")]
    public Guid PackageId { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [Display(Name = "Desired date and time")]
    public DateTime? DateTime { get; set; }

    [Display(Name = "Alternative date and time")]
    public DateTime? AlternativeDateTime { get; set; }

    [DataType(DataType.MultilineText)]
    [StringLength(255, ErrorMessage = "{0} can only consist of {1} characters.")]
    [Display(Name = "Remark")]
    public string? Remark { get; set; }

    [Display(Name = "Borrow materials")]
    public IEnumerable<Guid>? MaterialIds { get; set; } = new List<Guid>();
}