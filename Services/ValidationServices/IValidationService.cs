namespace rtperson.Services.ValidationServices
{
    public interface IValidationService<T> where T : class
    {
        Task<ValidationResponse> ValidateCreateAsync(T entity);
        Task<ValidationResponse> ValidateUpdateAsync(T entity);
        Task<ValidationResponse> ValidateDeleteAsync(Guid id);
    }

    public class ValidationResponse
    {
        public bool Success => Errors.Count == 0;
        public List<string> Errors { get; private set; }

        public ValidationResponse()
        {
            Errors = new List<string>();
        }

        public ValidationResponse AddError(string error)
        {
            Errors.Add(error);
            return this;
        }
    }
}
