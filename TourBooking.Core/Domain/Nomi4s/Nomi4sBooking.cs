using System.ComponentModel.DataAnnotations.Schema;

namespace TourBooking.Core.Domain.Nomi4s;

public sealed class Nomi4sBooking : BaseEntity
{
    public int AgeGroup { get; set; }

    public required string SchoolGrade { get; set; }

    public bool IsTransportPaymentRequested { get; set; }

    public Guid BookingId { get; set; }

    [ForeignKey(nameof(BookingId))]
    public Booking Booking { get; set; } = null!;
}