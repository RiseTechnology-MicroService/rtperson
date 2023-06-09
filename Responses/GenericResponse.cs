﻿namespace rtperson.Responses
{
    public class GenericResponse
    {
        public bool Success => ErrorMessages.Count == 0;
        public List<string> ErrorMessages { get; private set; }

        public GenericResponse()
        {
            ErrorMessages = new List<string>();
        }

        public void SetFailure(List<string> errorMessages) => ErrorMessages = errorMessages;

    }
    public class GenericResponse<T> : GenericResponse
    {
        public T? Data { get; private set; }
        public void SetSuccess(T data) => Data = data;
    }
}
