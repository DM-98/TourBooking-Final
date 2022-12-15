using Microsoft.EntityFrameworkCore;
using TourBooking.Core.Domain;
using TourBooking.Core.Interfaces;

namespace TourBooking.Infrastructure.Repositories;

public sealed class EFRepository<T, TDbContext> : IRepository<T> where T : BaseEntity where TDbContext : DbContext
{
	private readonly TDbContext context;
	private readonly DbSet<T> table;

	public EFRepository(TDbContext context)
	{
		this.context = context;
		table = this.context.Set<T>();
	}

	public IQueryable<T> GetTable(bool isDeletedEntitiesIncluded = false)
	{
		return isDeletedEntitiesIncluded ? table : table.Where(x => !x.IsDeleted);
	}

	public async Task<T?> CreateAsync(T entity, CancellationToken cancellationToken = default)
	{
		var createdEntityEntry = await table.AddAsync(entity, cancellationToken);

		if (createdEntityEntry is null)
		{
			return null;
		}

		var rowsAffected = await context.SaveChangesAsync(cancellationToken);

		return rowsAffected > 0 ? createdEntityEntry.Entity : null;
	}

	public async Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default)
	{
		context.Entry(entity).Property(x => x.UpdatedDate).CurrentValue = DateTime.UtcNow;
		context.Entry(entity).State = EntityState.Modified;

		var rowsAffected = await context.SaveChangesAsync(cancellationToken);

		return rowsAffected <= 0 ? null : entity;
	}

	public async Task<bool> DeleteByIdAsync(Guid id, bool isSoftDelete = true, CancellationToken cancellationToken = default)
	{
		var rowsAffected = isSoftDelete
			? await table.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(x => x.IsDeleted, true), cancellationToken)
			: await table.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);

		return rowsAffected > 0;
	}
}