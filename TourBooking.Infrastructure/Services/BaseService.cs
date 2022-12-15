using Microsoft.EntityFrameworkCore;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Infrastructure.Services;

public abstract class BaseService<T> : IBaseService<T> where T : BaseEntity, IAggregateRoot
{
	private readonly IRepository<T> repository;

	public BaseService(IRepository<T> repository)
	{
		this.repository = repository;
	}

	public async Task<ResponseDTO<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			var entities = await repository.GetTable().ToListAsync(cancellationToken);

			return new ResponseDTO<IEnumerable<T>>(true, content: entities);
		}
		catch (OperationCanceledException ex)
		{
			return new ResponseDTO<IEnumerable<T>>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
		}
		catch (Exception ex)
		{
			return new ResponseDTO<IEnumerable<T>>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
		}
	}

	public async Task<ResponseDTO<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		try
		{
			var entity = await repository.GetTable().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

			if (entity is null)
			{
				return new ResponseDTO<T>(false, "Your request was not found.", GenericErrorType.CouldNotFindEntity);
			}

			return new ResponseDTO<T>(true, content: entity);
		}
		catch (OperationCanceledException ex)
		{
			return new ResponseDTO<T>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
		}
		catch (Exception ex)
		{
			return new ResponseDTO<T>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
		}
	}

	public async Task<ResponseDTO<T>> CreateAsync(T entity, CancellationToken cancellationToken = default)
	{
		try
		{
			var createdEntity = await repository.CreateAsync(entity, cancellationToken);

			if (createdEntity is null)
			{
				return new ResponseDTO<T>(false, "Your request could not be created.", GenericErrorType.CouldNotCreateEntity);
			}

			return new ResponseDTO<T>(true, content: createdEntity);
		}
		catch (OperationCanceledException ex)
		{
			return new ResponseDTO<T>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
		}
		catch (Exception ex)
		{
			return new ResponseDTO<T>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
		}
	}

	public async Task<ResponseDTO<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default)
	{
		try
		{
			var updatedEntity = await repository.UpdateAsync(entity, cancellationToken);

			if (updatedEntity is null)
			{
				return new ResponseDTO<T>(false, "A server error has occurred while updating the entity - contact a server administrator.", GenericErrorType.CouldNotUpdateEntity);
			}

			return new ResponseDTO<T>(true, content: updatedEntity);
		}
		catch (OperationCanceledException ex)
		{
			return new ResponseDTO<T>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			var dbEntityEntry = ex.Entries[0];

			if (dbEntityEntry is null)
			{
				return new ResponseDTO<T>(false, "Someone has deleted the entry before your request.", GeneralErrorType.OptimisticConcurrency);
			}

			var dbEntityPropertyValues = await dbEntityEntry.GetDatabaseValuesAsync(cancellationToken);

			if (dbEntityPropertyValues is null)
			{
				return new ResponseDTO<T>(false, "Someone has deleted the entry before your request.", GeneralErrorType.OptimisticConcurrency);
			}

			if (dbEntityPropertyValues.ToObject() is not T dbEntity)
			{
				return new ResponseDTO<T>(false, "Someone has deleted the entry before your request.", GeneralErrorType.OptimisticConcurrency);
			}

			return new ResponseDTO<T>(false, "Someone else has edited the entry before your request, check the new values below the fields and correct them if necessary, then re-submit.", GeneralErrorType.OptimisticConcurrency, content: dbEntity);
		}
		catch (Exception ex)
		{
			return new ResponseDTO<T>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
		}
	}

	public async Task<ResponseDTO<T>> DeleteByIdAsync(Guid id, bool isSoftDelete = true, CancellationToken cancellationToken = default)
	{
		try
		{
			var isDeleted = await repository.DeleteByIdAsync(id, isSoftDelete, cancellationToken);

			if (!isDeleted)
			{
				return new ResponseDTO<T>(false, "Your request could not be deleted.", GenericErrorType.CouldNotDeleteEntity);
			}

			return new ResponseDTO<T>(true);
		}
		catch (OperationCanceledException ex)
		{
			return new ResponseDTO<T>(false, "Your request was canceled.", GeneralErrorType.OperationWasCanceled, ex.Message, ex.InnerException?.Message);
		}
		catch (Exception ex)
		{
			return new ResponseDTO<T>(false, "A server error has occurred - contact a server administrator.", GeneralErrorType.Unhandled, ex.Message, ex.InnerException?.Message);
		}
	}
}