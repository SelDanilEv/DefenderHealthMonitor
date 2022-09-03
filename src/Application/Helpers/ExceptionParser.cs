namespace Defender.HealthMonitor.Application.Helpers;

public static class ExceptionParser
{
    public static string GetValidationMessage(Exception ex)
    {
        return GetValidationMessage(ex.Message);
    }

    public static string GetValidationMessage(string message)
    {
        var messageChunks = message.Split("\"").ToList();

        var detailIndex = messageChunks.IndexOf("detail");
        if (detailIndex > -1)
        {
            return messageChunks[messageChunks.IndexOf("detail") + 2];
        }

        return "Error occurs";
    }

}
