using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Shared.Helpers
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public string? Status { get; set; }
        public string? StatusMessage { get; set; }
        public string? CorrelationId { get; set; }
        public HttpStatusCode StatusCode { get; set; } // Add this property
    }
    public class ApiResponseHelper
    {
        public Task<ApiResponse<T>> GenerateResponse<T>(T? data, Guid CorrelationId, HttpStatusCode statusCode)
        {
            var response = new ApiResponse<T>
            {
                Data = data,
                CorrelationId = CorrelationId.ToString()
            };

            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    response.Status = "Success";
                    response.StatusMessage = "Data retrieved successfully";
                    response.StatusCode = (HttpStatusCode)(int)statusCode;
                    break;

                case HttpStatusCode.Created:
                    response.Status = "Success";
                    response.StatusMessage = "Resource created successfully.";
                    response.StatusCode = (HttpStatusCode)(int)statusCode;
                    break;

                case HttpStatusCode.NoContent:
                    response.Status = "Success";
                    response.StatusMessage = "No content available.";
                    response.Data = default; // Nullify data for 204 responses
                    response.StatusCode = (HttpStatusCode)(int)statusCode;
                    break;

                case HttpStatusCode.Unauthorized:
                    response.Status = "Failed";
                    response.StatusMessage = "Unauthorized Access";
                    response.Data = default; // Nullify data for unauthorized access
                    response.StatusCode = (HttpStatusCode)(int)statusCode;
                    break;

                case HttpStatusCode.Forbidden:
                    response.Status = "Failed";
                    response.StatusMessage = "Forbidden access.";
                    response.Data = default;
                    response.StatusCode = (HttpStatusCode)(int)statusCode;
                    break;

                case HttpStatusCode.NotFound:
                    response.Status = "Failed";
                    response.StatusMessage = "Resource not found.";
                    response.Data = default;
                    response.StatusCode = (HttpStatusCode)(int)statusCode;
                    break;

                case HttpStatusCode.InternalServerError:
                    response.Status = "Failed";
                    response.StatusMessage = "Internal server error occurred while processing request";
                    response.Data = default; // Nullify data for server errors
                    response.StatusCode = (HttpStatusCode)(int)statusCode;
                    break;

                default:
                    response.Status = "Failed";
                    response.StatusMessage = "Unexpected error.";
                    response.Data = default;
                    break;
            }

            // Wrap the response in a Task to make the method asynchronous
            return Task.FromResult(response);
        }
    }
}