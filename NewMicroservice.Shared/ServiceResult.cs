
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace NewMicroservice.Shared
{
    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }
        public ProblemDetails? Fail { get; set; }
        [JsonIgnore]
        public bool IsSuccess => Fail == null;
        [JsonIgnore]
        public bool IsFailed => !IsSuccess;

        public static ServiceResult SuccessAsNoContent() => new ServiceResult { Status = HttpStatusCode.NoContent };
        public static ServiceResult ErrorAsNotFound() => new ServiceResult
        {
            Status = HttpStatusCode.NotFound,
            Fail = new ProblemDetails { Title = "Not Found", Detail = "The requested resource is not found." }
        };
        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                Fail = problemDetails,
                Status = status
            };
        }
        public static ServiceResult Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                Fail = new ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = status.GetHashCode()
                },
                Status = status
            };
        }
        public static ServiceResult Error(string title, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                Fail = new ProblemDetails
                {
                    Title = title,

                    Status = status.GetHashCode()
                },
                Status = status
            };
        }
        public static ServiceResult Error(IDictionary<string, object?> errors)
        {
            return new ServiceResult()
            {
                Fail = new ProblemDetails
                {
                    Title = "Validation erorors occured",
                    Detail = "Please check error property for more details",
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                    Extensions = errors
                },
                Status = HttpStatusCode.BadRequest
            };
        }
        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult()
                {
                    Fail = new ProblemDetails
                    {
                        Title = exception.Message
                    },
                    Status = exception.StatusCode
                };
            }
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult()
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }

    }
    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }
        public string? UrlAsCreated { get; set; }
        public static ServiceResult<T> SuccessAsOk(T data) => new ServiceResult<T> { Status = HttpStatusCode.Created, Data = data};      
        public static ServiceResult<T> SuccessAsCreated(T data, string url) => new ServiceResult<T> { Status = HttpStatusCode.Created, Data = data, UrlAsCreated = url };      
        public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                Status = status
            };
        }
        public new static ServiceResult<T> Error(string title, string description, HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                Fail = new ProblemDetails
                {
                    Title = title,
                    Detail = description,
                    Status = status.GetHashCode()
                },
                Status = status
            };
        }
        public new static ServiceResult<T> Error(string title, HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                Fail = new ProblemDetails
                {
                    Title = title,

                    Status = status.GetHashCode()
                },
                Status = status
            };
        }
        public new static ServiceResult<T> Error(IDictionary<string, object?> errors)
        {
            return new ServiceResult<T>()
            {
                Fail = new ProblemDetails
                {
                    Title = "Validation erorors occured",
                    Detail = "Please check error property for more details",
                    Status = HttpStatusCode.BadRequest.GetHashCode(),
                    Extensions = errors
                },
                Status = HttpStatusCode.BadRequest
            };
        }
        public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    Status = exception.StatusCode
                };
            }
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                Status = exception.StatusCode
            };
        }

    }

}
