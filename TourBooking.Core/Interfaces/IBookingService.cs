using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Inputs;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;

namespace TourBooking.Core.Interfaces;

public interface IBookingService : IBaseService<Booking>
{
    Task<ResponseDTO<Booking>> CreateBookingAsync(string handle, CreateBookingInputModel createBookingInputModel, string confirmUrl, CancellationToken cancellationToken = default);

    Task<ResponseDTO<IEnumerable<BookingListDTO>>> GetBookingListAsync(string handle, Guid? userId = null, CancellationToken cancellationToken = default);

    Task<ResponseDTO<BookingDetailsDTO>> GetBookingDetailsAsync(Guid id, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<ResponseDTO<BookingDetailsDTO>> ToggleBookingStatusAsync(Guid id, BookingStatus bookingStatus, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<ResponseDTO<Message>> CreateMessageAsync(CreateMessageInputModel createMessageInputModel, Guid? userId = default, CancellationToken cancellationToken = default);
}