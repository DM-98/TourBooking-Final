using Microsoft.EntityFrameworkCore;
using TourBooking.Core.Domain.Nomi4s;
using TourBooking.Core.DTOs.Inputs.Nomi4s;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;
using TourBooking.Core.Interfaces.Nomi4s;

namespace TourBooking.Infrastructure.Services.Nomi4s;

public sealed class Nomi4sBookingService : INomi4sBookingService
{
    private readonly IRepository<Nomi4sBooking> nomi4sBookingRepository;

    public Nomi4sBookingService(IRepository<Nomi4sBooking> nomi4sBookingRepository)
    {
        this.nomi4sBookingRepository = nomi4sBookingRepository;
    }

    public async Task<ResponseDTO<Nomi4sBooking>> CreateNomi4sBookingAsync(CreateNomi4sBookingInputModel createNomi4sBookingInputModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var nomi4sBookingToCreate = new Nomi4sBooking
            {
                BookingId = createNomi4sBookingInputModel.BookingId,
                AgeGroup = createNomi4sBookingInputModel.AgeGroup!.Value,
                SchoolGrade = createNomi4sBookingInputModel.SchoolGrade,
                IsTransportPaymentRequested = createNomi4sBookingInputModel.IsTransportPaymentRequested!.Value
            };

            var createdNomi4sBooking = await nomi4sBookingRepository.CreateAsync(nomi4sBookingToCreate, cancellationToken);

            if (createdNomi4sBooking is null)
            {
                return new ResponseDTO<Nomi4sBooking>(false, "Failed to create Nomi4s booking.", CreateNomi4sBookingErrorType.CouldNotCreateNomi4sBooking);
            }

            return new ResponseDTO<Nomi4sBooking>(true, content: createdNomi4sBooking);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Nomi4sBooking>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Nomi4sBooking>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }

    public async Task<ResponseDTO<Nomi4sBooking>> GetNomi4sDetailsAsync(Guid bookingId, CancellationToken cancellationToken = default)
    {
        try
        {
            var nomi4sBooking = await nomi4sBookingRepository.GetTable().FirstOrDefaultAsync(x => x.BookingId == bookingId, cancellationToken);

            if (nomi4sBooking is null)
            {
                return new ResponseDTO<Nomi4sBooking>(false, "Failed to find Nomi4s booking.", GetNomi4sDetailsErrorType.CouldNotFindNomi4sBooking);
            }

            return new ResponseDTO<Nomi4sBooking>(true, content: nomi4sBooking);
        }
        catch (OperationCanceledException ex)
        {
            return new ResponseDTO<Nomi4sBooking>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
        }
        catch (Exception ex)
        {
            return new ResponseDTO<Nomi4sBooking>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
        }
    }
}