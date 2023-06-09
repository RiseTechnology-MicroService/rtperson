namespace rtperson.Services
{
    public interface ILogService
    {
        Task LogAsync(string message);
        Task LogAsync(Exception ex);
    }
}
