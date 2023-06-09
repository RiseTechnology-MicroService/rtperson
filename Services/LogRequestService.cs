using System.Text;
using System.Text.Json;

namespace rtperson.Services
{
    public class LogRequestService
    {
        private readonly HttpClient _httpClient;
        public LogRequestService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("Services:LoggerURL")!);
        }
        public async Task SendLogAsync(SendLogModel data)
        {
            var jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("api/logs", content);
        }
    }

    public class SendLogModel
    {
        public string Application { get; set; }
        public string Version { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }
        public string Type { get; set; }

        public SendLogModel(string application, string version, string type, string message, string? stackTrace)
        {
            Application = application;
            Version = version;
            Type = type;
            Message = message;
            StackTrace = stackTrace;
        }
    }
}
