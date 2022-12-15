using System.ComponentModel.DataAnnotations;

namespace TourBooking.Core.Domain;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [Timestamp]
    public byte[]? RowVersion { get; set; }

    public bool IsDeleted { get; set; }

    public BaseEntity()
    {
        CreatedDate = DateTime.UtcNow;
    }
}