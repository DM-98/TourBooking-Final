using System.ComponentModel.DataAnnotations;

namespace TourBooking.Core.DTOs.Inputs.Nomi4s;

public sealed class CreateNomi4sBookingInputModel
{
    [Required(ErrorMessage = "This field is required.")]
    [Display(Name = "Age group")]
    public int? AgeGroup { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [StringLength(15, ErrorMessage = "{0} can only consist of {1} characters.")]
    [Display(Name = "School grade")]
    public string SchoolGrade { get; set; } = null!;

    [Required(ErrorMessage = "This field is required.")]
    [Display(Name = "Paid student transport for primary and independent schools in Holstebro, Lemvig, Skive and Struer municipalities (applies ONLY to information center Holstebro and Kåstrup)")]
    public bool? IsTransportPaymentRequested { get; set; }

    public Guid BookingId { get; set; }
}