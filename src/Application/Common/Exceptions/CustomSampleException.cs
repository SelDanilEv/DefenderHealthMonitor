namespace Defender.HealthMonitor.Application.Common.Exceptions;

public class CustomSampleException : Exception
{
    public CustomSampleException()
        : base("Something goes wrong during a request to google.")
    {
    }
}
