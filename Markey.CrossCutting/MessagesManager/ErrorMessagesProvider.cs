using System.Text.Json;
namespace Markey.CrossCutting.MessagesManager;
public class ErrorMessagesProvider
{
    public Dictionary<string, ErrorMessage> ErrorMessages { get; private set; }

    public ErrorMessagesProvider(string folderPath)
    {
        ErrorMessages = new Dictionary<string, ErrorMessage>();
        LoadErrorMessages(folderPath);
    }

    private void LoadErrorMessages(string folderPath)
    {
        var files = Directory.GetFiles(folderPath, "*.json");

        foreach (var file in files)
        {
            var json = File.ReadAllText(file);
            var data = JsonSerializer.Deserialize<JsonRoot>(json);

            if (data != null)
            {
                foreach (var message in data.Messages)
                {
                    ErrorMessages[message.Key] = message;
                }
            }
        }
    }
}
