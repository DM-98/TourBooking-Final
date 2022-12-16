using TourBooking.Core.Domain.Nomi4s;
using TourBooking.Core.DTOs.Inputs.Nomi4s;
using TourBooking.Core.DTOs.Outputs;

namespace TourBooking.Core.Interfaces.Nomi4s;

public interface INomi4sBookingService
{
    Task<ResponseDTO<Nomi4sBooking>> CreateNomi4sBookingAsync(CreateNomi4sBookingInputModel createNomi4sBookingInputModel, CancellationToken cancellationToken = default);

    Task<ResponseDTO<Nomi4sBooking>> GetNomi4sDetailsAsync(Guid bookingId, CancellationToken cancellationToken = default);
}