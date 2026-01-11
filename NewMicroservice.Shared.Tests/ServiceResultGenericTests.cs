using Xunit;
using NewMicroservice.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System.Text.Json;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace NewMicroservice.Shared.Tests
{
    public class ServiceResultGenericTests
    {
        [Fact]
        public void SuccessAsOk_ShouldReturnSuccessWithDataAndOkStatus()
        {
            // Arrange
            var data = new { Id = 1, Name = "Test" };

            // Act
            var result = ServiceResult<object>.SuccessAsOk(data);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailed);
            Assert.Equal(data, result.Data);
            Assert.Null(result.Fail);
        }

        [Fact]
        public void SuccessAsCreated_ShouldReturnSuccessWithDataAndCreatedStatus()
        {
            // Arrange
            var data = new { Id = 1, Name = "Test" };
            var url = "https://example.com/api/resource/1";

            // Act
            var result = ServiceResult<object>.SuccessAsCreated(data, url);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Created, result.Status);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailed);
            Assert.Equal(data, result.Data);
            Assert.Equal(url, result.UrlAsCreated);
            Assert.Null(result.Fail);
        }

        [Fact]
        public void Error_WithProblemDetailsAndStatus_ShouldReturnErrorWithProvidedDetails()
        {
            // Arrange
            var problemDetails = new ProblemDetails
            {
                Title = "Not Found",
                Detail = "Resource not found"
            };
            var status = HttpStatusCode.NotFound;

            // Act
            var result = ServiceResult<string>.Error(problemDetails, status);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(status, result.Status);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
            Assert.Equal(problemDetails, result.Fail);
            Assert.Null(result.Data);
        }

        [Fact]
        public void Error_WithTitleAndDescriptionAndStatus_ShouldReturnErrorWithDetails()
        {
            // Arrange
            var title = "Invalid Request";
            var description = "The request parameters are invalid";
            var status = HttpStatusCode.BadRequest;

            // Act
            var result = ServiceResult<string>.Error(title, description, status);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(status, result.Status);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
            Assert.NotNull(result.Fail);
            Assert.Equal(title, result.Fail.Title);
            Assert.Equal(description, result.Fail.Detail);
            Assert.Null(result.Data);
        }

        [Fact]
        public void Error_WithTitleAndStatus_ShouldReturnErrorWithTitleOnly()
        {
            // Arrange
            var title = "Access Denied";
            var status = HttpStatusCode.Forbidden;

            // Act
            var result = ServiceResult<byte[]>.Error(title, status);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(status, result.Status);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
            Assert.NotNull(result.Fail);
            Assert.Equal(title, result.Fail.Title);
            Assert.Null(result.Data);
        }

        [Fact]
        public void Error_WithErrorsDictionary_ShouldReturnValidationError()
        {
            // Arrange
            var errors = new Dictionary<string, object?>
            {
                { "Name", "Name is required" },
                { "Email", "Invalid email format" }
            };

            // Act
            var result = ServiceResult<List<string>>.Error(errors);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
            Assert.NotNull(result.Fail);
            Assert.Contains("Validation", result.Fail.Title);
            Assert.Null(result.Data);
        }

        [Fact]
        public void SuccessAsOk_WithNullData_ShouldReturnSuccess()
        {
            // Act
            var result = ServiceResult<string?>.SuccessAsOk(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result.Status);
            Assert.True(result.IsSuccess);
            Assert.Null(result.Data);
        }

        [Fact]
        public void SuccessAsCreated_WithComplexData_ShouldReturnCorrectly()
        {
            // Arrange
            var data = new List<int> { 1, 2, 3, 4, 5 };
            var url = "https://api.example.com/items";

            // Act
            var result = ServiceResult<List<int>>.SuccessAsCreated(data, url);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Created, result.Status);
            Assert.True(result.IsSuccess);
            Assert.Equal(data, result.Data);
            Assert.Equal(url, result.UrlAsCreated);
        }
    }
}
