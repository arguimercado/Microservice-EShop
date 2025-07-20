using MediatR;

namespace BuildingBlocks.CQRS;

// ICommand does not require variance, so we remove the 'in' keyword from TResponse
public interface ICommand : IRequest<Unit> { }

public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}