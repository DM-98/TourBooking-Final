using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourBooking.Core.Domain;
using TourBooking.Core.DTOs.Outputs;
using TourBooking.Core.Enums;
using TourBooking.Core.Interfaces;

namespace TourBooking.Web.Controllers;

[Authorize(Roles = "Admin")]
public abstract class EFBaseController<T> : ControllerBase where T : BaseEntity
{
	private readonly IBaseService<T> baseService;

	public EFBaseController(IBaseService<T> baseService)
	{
		this.baseService = baseService;
	}

	[HttpGet("base/GetAll")]
	public async Task<ActionResult<ResponseDTO<IEnumerable<T>>>> GetAllAsync(CancellationToken cancellationToken)
	{
		var result = await baseService.GetAllAsync(cancellationToken);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is GeneralErrorType.OperationWasCanceled)
			{
				return StatusCode(499, result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpGet("base/GetById/{id}")]
	public async Task<ActionResult<ResponseDTO<T>>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var result = await baseService.GetByIdAsync(id, cancellationToken);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is GeneralErrorType.OperationWasCanceled)
			{
				return StatusCode(499, result);
			}
			else if (result.ErrorType is GenericErrorType.CouldNotFindEntity)
			{
				return NotFound(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpPost("base/Create")]
	public async Task<ActionResult<ResponseDTO<T>>> CreateAsync(T entity, CancellationToken cancellationToken)
	{
		var result = await baseService.CreateAsync(entity, cancellationToken);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is GeneralErrorType.OperationWasCanceled)
			{
				return StatusCode(499, result);
			}
			else if (result.ErrorType is GenericErrorType.CouldNotCreateEntity)
			{
				return UnprocessableEntity(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpPut("base/Update")]
	public async Task<ActionResult<ResponseDTO<T>>> UpdateAsync(T entity, CancellationToken cancellationToken)
	{
		var result = await baseService.UpdateAsync(entity, cancellationToken);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is GeneralErrorType.OperationWasCanceled)
			{
				return StatusCode(499, result);
			}
			else if (result.ErrorType is GenericErrorType.CouldNotUpdateEntity)
			{
				return UnprocessableEntity(result);
			}
			else if (result.ErrorType is GeneralErrorType.OptimisticConcurrency)
			{
				return Conflict(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}

	[HttpDelete("base/DeleteById/{id}/{isSoftDelete?}")]
	public async Task<ActionResult<ResponseDTO<T>>> DeleteByIdAsync(Guid id, CancellationToken cancellationToken, bool isSoftDelete = true)
	{
		var result = await baseService.DeleteByIdAsync(id, isSoftDelete, cancellationToken);

		if (result.IsSuccess)
		{
			return Ok(result);
		}
		else
		{
			if (result.ErrorType is GeneralErrorType.OperationWasCanceled)
			{
				return StatusCode(499, result);
			}
			else if (result.ErrorType is GenericErrorType.CouldNotDeleteEntity)
			{
				return UnprocessableEntity(result);
			}
			else
			{
				return StatusCode(StatusCodes.Status500InternalServerError, result);
			}
		}
	}
}