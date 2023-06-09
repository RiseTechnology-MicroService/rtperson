namespace rtperson.Services.Responses
{
    public class ServiceResponse
    {
        public bool Success => Errors.Count == 0;
        public List<ErrorModel> Errors { get; private set; }
        public ServiceResponse()
        {
            Errors = new List<ErrorModel>();
        }

        public ServiceResponse AddError(string error, ErrorTypeEnum et = ErrorTypeEnum.Validation)
        {
            Errors.Add(new ErrorModel(error, et));
            return this;
        }

        public ServiceResponse AddError(List<string> errors, ErrorTypeEnum et = ErrorTypeEnum.Validation)
        {
            Errors.AddRange(errors.Select(error => new ErrorModel(error, et)));
            return this;
        }

        public List<string> GetErrors()
        {
            return Errors.Select(e => e.Error).ToList();
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T? Data { get; private set; }

        public void SetData(T data) => Data = data;
    }

    public class ErrorModel
    {
        public string Error { get; private set; }
        public string ErrorType { get; private set; }

        public ErrorModel(string error, ErrorTypeEnum errorType)
        {
            Error = error;
            ErrorType = errorType.ToString();
        }

    }

    public enum ErrorTypeEnum
    {
        InternalError,
        Validation
    }
}