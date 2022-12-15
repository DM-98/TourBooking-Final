using TourBooking.Core.Domain;
using TourBooking.Core.Enums;

namespace TourBooking.Core.DTOs.Outputs;

public sealed record BookingDetailsDTO
{
    public Guid Id { get; set; }

    public BookingStatus BookingStatus { get; set; }

    public required string Organization { get; set; }

    public DateTime DateTime { get; set; }

    public DateTime? AlternativeDateTime { get; set; }

    public required string PackageName { get; set; }

    public required string PackageDescription { get; set; }

    public string? StreetName { get; set; }

    public string? City { get; set; }

    public int? ZipCode { get; set; }

    public int Attendees { get; set; }

    public required string BookersName { get; set; }

    public required string BookersEmail { get; set; }

    public string? BookersPhoneNumber { get; set; }

    public string? Remark { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public IEnumerable<Material>? Materials { get; set; }

    public IEnumerable<Message>? Messages { get; set; }
}