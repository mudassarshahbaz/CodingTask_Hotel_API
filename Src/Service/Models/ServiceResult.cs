using DTO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult()
        {
        }

        public ServiceResult(ErrorResponse error, bool isSuccess, string message, T data)
        {
            Error = error;
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public T Data { get; set; }
    }

    public class ServiceListResult<T> : ServiceResult<T>
    {
        public ServiceListResult(ErrorResponse error, bool isSuccess, string message, T data)
        {
            Error = error;
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
    }

    public class ServiceResult
    {
        public ServiceResult() { }

        public ServiceResult(ErrorResponse error, bool isSuccess, string message)
        {
            Error = error;
            IsSuccess = isSuccess;
            Message = message;
        }

        public ErrorResponse Error { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }

    public static class ServiceResults
    {
        public static ServiceResult GetSuccessfully()
        {
            string message = "Data successfully get";
            var result = new ServiceResult(null, true, message);
            return result;
        }

        public static ServiceResult<TData> GetSuccessfully<TData>(TData data)
        {
            string message = "Data successfully get";
            var result = new ServiceResult<TData>(null, true, message, data);
            return result;
        }

        public static class Errors
        {

            public static ServiceResult Required(string value)
            {
                string message = $"{value} required";
                var result = new ServiceResult(new ErrorResponse(nameof(Invalid), message), false, message);
                return result;
            }

            public static ServiceResult<TData> Required<TData>(string value, TData data)
            {
                string message = $"{value} required";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(Invalid), message), false, message, data);
                return result;
            }

            public static ServiceResult Invalid(string value)
            {
                string message = $"Invalid {value}";
                var result = new ServiceResult(new ErrorResponse(nameof(Invalid), message), false, message);
                return result;
            }

            public static ServiceResult<TData> Invalid<TData>(string value, TData data)
            {
                string message = $"Invalid {value}";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(Invalid), message), false, message, data);
                return result;
            }

            public static ServiceResult NotFound(string value)
            {
                string message = $"{value} not found";
                var result = new ServiceResult(new ErrorResponse(nameof(NotFound), message), false, message);
                return result;
            }

            public static ServiceResult<TData> NotFound<TData>(string value, TData data)
            {
                string message = $"{value} not found";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(NotFound), message), false, message, data);
                return result;
            }

            public static ServiceResult UnhandledError(string exMessage)
            {
                string message = $"Unhandled error";
                var result = new ServiceResult(new ErrorResponse(nameof(UnhandledError), message), false, exMessage);
                return result;
            }

            public static ServiceResult<TData> UnhandledError<TData>(string exMessage, TData data)
            {
                string message = $"Unhandled error";
                var result = new ServiceResult<TData>(new ErrorResponse(nameof(UnhandledError), message), false, exMessage, data);
                return result;
            }
        }
    }
}
