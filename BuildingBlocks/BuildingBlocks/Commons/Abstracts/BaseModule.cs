using BuildingBlocks.Commons.Errors;
using Carter;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BuildingBlocks.Commons.Abstracts;

public abstract class BaseModule : ICarterModule
{
    public IResult HandleResults<T>(Result<T> data)
    {
        if (data.IsFailed)
        {
            // Check if any error is a ValidationError
            bool hasValidationError = data.Errors.Any(e => e is ValidationErrorResult);
            if (hasValidationError)
            {
                // Handle validation error (e.g., return BadRequest)
                return TypedResults.BadRequest(data.Errors);
            }
            // Handle other errors as needed
        }

        return TypedResults.Ok(data.Value);
    }
    public virtual void AddRoutes(IEndpointRouteBuilder app)
    {
    }
}
