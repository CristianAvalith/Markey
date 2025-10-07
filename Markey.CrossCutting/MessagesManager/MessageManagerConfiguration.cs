using Microsoft.Extensions.DependencyInjection;
namespace Markey.CrossCutting.MessagesManager;
public static class MessageManagerConfiguration
{
    public static IServiceCollection ConfigureMessageManager(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            var folderPath = Path.Combine(AppContext.BaseDirectory, "ErrorCatalog");
            return new ErrorMessagesProvider(folderPath);
        });

        services.AddSingleton<MessageManager>();

        return services;
    }
}
