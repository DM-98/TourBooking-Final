namespace TourBooking.Core.Interfaces;

public interface IRepository<T> where T : class
{
	IQueryable<T> GetTable(bool isDeletedEntitiesIncluded = false);

	Task<T?> CreateAsync(T entity, CancellationToken cancellationToken = default);

	Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default);

	Task<bool> DeleteByIdAsync(Guid id, bool isSoftDelete = true, CancellationToken cancellationToken = default);
}