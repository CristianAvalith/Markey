namespace Markey.CrossCutting.MessagesManager;
public class MessageManager
{
    private readonly ErrorMessagesProvider _errorMessagesProvider;

    public MessageManager(ErrorMessagesProvider errorMessagesProvider)
    {
        _errorMessagesProvider = errorMessagesProvider;
    }

    public string GetNotification(string errorKey, params object[] formatArgs)
    {
        if (_errorMessagesProvider.ErrorMessages.TryGetValue(errorKey, out var errorMessage))
        {
            return string.Format(errorMessage.Value, formatArgs);
        }
        return "Unknown error.";
    }
}
