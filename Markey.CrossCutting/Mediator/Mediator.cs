using Markey.CrossCutting.Mediator;
using Microsoft.Extensions.DependencyInjection;

public class Mediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        dynamic handler = _serviceProvider.GetRequiredService(handlerType); // <- lanza excepción si no encuentra

        return await handler.Handle((dynamic)request);
    }
}
