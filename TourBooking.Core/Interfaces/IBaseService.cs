using TourBooking.Core.DTOs.Outputs;

namespace TourBooking.Core.Interfaces;

public interface IBaseService<T> where T : class
{
	Task<ResponseDTO<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken = default);

	Task<ResponseDTO<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	Task<ResponseDTO<T>> CreateAsync(T entity, CancellationToken cancellationToken = default);

	Task<ResponseDTO<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default);

	Task<ResponseDTO<T>> DeleteByIdAsync(Guid id, bool isSoftDelete = true, CancellationToken cancellationToken = default);
}