using Defender.HealthMonitor.Application.Helpers;
using Defender.HealthMonitor.Infrastructure.Clients.Monitor;
using Newtonsoft.Json;

namespace Defender.HealthMonitor.Infrastructure.Clients;

public partial class MonitorClient : IMonitorClient
{
    private readonly HttpClient _httpClient;

    public MonitorClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HealthModel> CheckHealthAsync(string url, HttpMethod method)
    {
        if (url == null && method == null)
            throw new ArgumentNullException(nameof(url));

        try
        {
            using (var request = new HttpRequestMessage())
            {
                request.Method = method;
                request.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

                var response = await _httpClient.SendAsync(request);

                var responseText = await response.Content.ReadAsStringAsync();

                switch ((int)response.StatusCode)
                {
                    case 200:
                        try
                        {
                            if (responseText == null)
                            {
                                return new HealthModel { Status = "Unhealthy" };
                            }
                            if (responseText.Contains("<!doctype html>"))
                            {
                                return new HealthModel { Status = "NotFound" };
                            }

                            return JsonConvert.DeserializeObject<HealthModel>(responseText) ?? new HealthModel { Status = "Unhealthy" };
                        }
                        catch (JsonSerializationException)
                        {
                            var message = "Could not deserialize the response body string as " + typeof(HealthModel).FullName + ".";
                            throw new InvalidCastException(message);
                        }
                    case 404:
                        return new HealthModel { Status = "NotFound" };
                    default:
                        return new HealthModel { Status = "Unhealthy" };

                }
            }
        }
        catch (Exception ex)
        {
            SimpleLogger.Log(ex);
            return new HealthModel { Status = "Unhealthy" };
        }
    }
}

public class HealthModel
{
    public string? Status { get; set; }
}