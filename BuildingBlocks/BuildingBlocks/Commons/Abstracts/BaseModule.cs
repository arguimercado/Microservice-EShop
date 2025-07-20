using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BuildingBlocks.Commons.Abstracts;

public abstract class BaseModule : ICarterModule
{
    public IResult HandleResults<T>(T data)
    {
        return TypedResults.Ok(data);
    }
    public virtual void AddRoutes(IEndpointRouteBuilder app)
    {
    }
}
