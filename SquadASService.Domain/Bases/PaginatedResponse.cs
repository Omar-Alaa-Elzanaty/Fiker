using FluentValidation.Results;
using SquadAsService.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Domain.Bases
{
    public class PaginatedResponse<T>:BaseResponse<List<T>>
    {
        public PaginatedResponse()
        {
        }

        public PaginatedResponse(bool succeeded, List<T> data = default,
                               string message = null,
                               List<ValidationFailure> validationFailures = null, int count = 0,
                               HttpStatusCode httpStatusCode = HttpStatusCode.OK,
                               int pageNumber = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = pageNumber;
            StatusCode = httpStatusCode;
            IsSuccess = succeeded;
            Message = message;
            Errors = validationFailures?.GetErrorsList();
            PageSize = pageSize;
            TotalPages = count / pageSize + (count % pageSize > 0 ? 1 : 0);
            TotalCount = count;
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public static PaginatedResponse<T> Create(List<T> data, int count, int pageNumber, int pageSize)
        {
            return new PaginatedResponse<T>(true, data, null, null, count, HttpStatusCode.OK, pageNumber, pageSize);
        }

        public static PaginatedResponse<T> FailureAsync(string message, HttpStatusCode statusCode)
        {
            return new PaginatedResponse<T>()
            {
                Message = message,
                StatusCode = statusCode,
                IsSuccess = false
            };
        }
    }
}
