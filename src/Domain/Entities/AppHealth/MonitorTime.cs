namespace Defender.HealthMonitor.Domain.Entities.AppHealth;

public class MonitorTime
{
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }

    public bool IsTimeToMonitor()
    {
        if (StartTime == null || EndTime == null)
        {
            return false;
        }

        var startTimeArray = StartTime.Split(':').Select(t => Double.Parse(t)).ToList();
        var endTimeArray = EndTime.Split(':').Select(t => Double.Parse(t)).ToList();

        var startDateTime = DateTime.UtcNow.Date.AddHours(startTimeArray[0]).AddMinutes(startTimeArray[1]);
        var endDateTime = DateTime.UtcNow.Date.AddHours(endTimeArray[0]).AddMinutes(endTimeArray[1]);
        
        if(startTimeArray[0] == endTimeArray[0] && startTimeArray[1] == endTimeArray[1])
        {
            return true;
        }

        if (startTimeArray[0] >= endTimeArray[0]
            && (startTimeArray[0] != endTimeArray[0] || startTimeArray[1] >= endTimeArray[1]))
        {
            endDateTime = endDateTime.AddDays(1);
        }

        var currentDate = DateTime.UtcNow;

        return startDateTime <= currentDate && currentDate <= endDateTime;
    }
}
