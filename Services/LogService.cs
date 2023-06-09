namespace rtperson.Services
{
    public class LogService : ILogService
    {
        private string _version = string.Empty;
        private string _application = string.Empty;

        private const string ErrorLogType = "error";
        private const string InfoLogType = "info";

        private readonly LogRequestService _logRequestService;

        public LogService(IConfiguration configuration, LogRequestService logRequestService)
        {
            var version = configuration.GetSection("Application:Version").Value;
            if (version == null)
                throw new Exception("Application version is not defined in appsettings.json");

            var application = configuration.GetSection("Application:Name").Value;
            if (application == null)
                throw new Exception("Application name is not defined in appsettings.json");

            _version = version;
            _application = application;
            _logRequestService = logRequestService;
        }
        public async Task LogAsync(string message)
        {
            await _logRequestService
                .SendLogAsync(new SendLogModel(_application, _version, InfoLogType, message, string.Empty))
                .ConfigureAwait(false);
        }

        public async Task LogAsync(Exception ex)
        {
            await _logRequestService
               .SendLogAsync(new SendLogModel(_application, _version, ErrorLogType, ex.Message, ex.StackTrace))
               .ConfigureAwait(false);
        }
    }
}
