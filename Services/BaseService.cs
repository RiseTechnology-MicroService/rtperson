namespace rtperson.Services
{
    public abstract class BaseService
    {
        private readonly ILogService _logService;

        public BaseService(ILogService logService)
        {
            _logService = logService;
        }

        public void WriteLog(string message) => _logService.LogAsync(message);
        public void WriteException(Exception ex) => _logService.LogAsync(ex);
    }
}
