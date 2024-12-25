using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Fiker.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fiker.Domain.Bases
{
    public class BaseResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string? Message { get; set; }
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string>? Errors { get; set; }

        public BaseResponse() { }

        public static BaseResponse<T> Success(T data, string message = "Success")
        {
            return new BaseResponse<T>
            {
                StatusCode = HttpStatusCode.OK,
                Message = message,
                Data = data,
                IsSuccess = true
            };
        }
        
        public static BaseResponse<T> Success(string message = "Success")
        {
            return new BaseResponse<T>
            {
                StatusCode = HttpStatusCode.OK,
                Message = message,
                IsSuccess = true
            };
        }

        public static BaseResponse<T> Fail(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new BaseResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                IsSuccess = false
            };
        }

        public static BaseResponse<T> Error(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new BaseResponse<T>
            {
                StatusCode = statusCode,
                Message = message,
                IsSuccess = false
            };
        }

        public static BaseResponse<T> ValidationFailure(List<ValidationFailure> validationFailures, HttpStatusCode statusCode=HttpStatusCode.UnprocessableEntity)
        {
            List<string> errors = validationFailures.GetErrorsList();

            var response = new BaseResponse<T>()
            {
                IsSuccess = false,
                StatusCode = statusCode,
                Errors = errors
            };

            return response;
        }

        public static BaseResponse<T> ValidationFailure(List<IdentityError> identityErrors, HttpStatusCode httpStatusCode=HttpStatusCode.UnprocessableEntity)
        {
            List<string> errors = identityErrors.GetErrorsList();

            var response = new BaseResponse<T>()
            {
                IsSuccess = false,
                StatusCode = httpStatusCode,
                Errors = errors
            };

            return response;
        }
    }
}
