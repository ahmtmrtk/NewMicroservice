using Xunit;
using NewMicroservice.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace NewMicroservice.Shared.Tests
{
    public class ServiceResultTests
    {
        [Fact]
        public void SuccessAsNoContent_ShouldReturnSuccessWithNoContentStatus()
        {
            // Act
            var result = ServiceResult.SuccessAsNoContent();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NoContent, result.Status);
            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailed);
            Assert.Null(result.Fail);
        }

        [Fact]
        public void ErrorAsNotFound_ShouldReturnErrorWithNotFoundStatus()
        {
            // Act
            var result = ServiceResult.ErrorAsNotFound();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotFound, result.Status);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
            Assert.NotNull(result.Fail);
            Assert.Equal("Not Found", result.Fail.Title);
            Assert.Equal("The requested resource is not found.", result.Fail.Detail);
        }

        [Fact]
        public void Error_WithProblemDetailsAndStatus_ShouldReturnErrorWithProvidedDetails()
        {
            // Arrange
            var problemDetails = new ProblemDetails
            {
                Title = "Test Error",
                Detail = "This is a test error"
            };
            var status = HttpStatusCode.BadRequest;

            // Act
            var result = ServiceResult.Error(problemDetails, status);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(status, result.Status);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
            Assert.Equal(problemDetails, result.Fail);
            Assert.Equal("Test Error", result.Fail?.Title);
        }

        [Fact]
        public void Error_WithTitleAndDescriptionAndStatus_ShouldReturnErrorWithDetails()
        {
            // Arrange
            var title = "Unauthorized";
            var description = "User is not authorized";
            var status = HttpStatusCode.Unauthorized;

            // Act
            var result = ServiceResult.Error(title, description, status);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(status, result.Status);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
            Assert.NotNull(result.Fail);
            Assert.Equal(title, result.Fail.Title);
            Assert.Equal(description, result.Fail.Detail);
        }

        [Fact]
        public void Error_WithTitleAndStatus_ShouldReturnErrorWithTitleOnly()
        {
            // Arrange
            var title = "Server Error";
            var status = HttpStatusCode.InternalServerError;

            // Act
            var result = ServiceResult.Error(title, status);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(status, result.Status);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
            Assert.NotNull(result.Fail);
            Assert.Equal(title, result.Fail.Title);
        }

        [Fact]
        public void Error_WithErrorsDictionary_ShouldReturnValidationError()
        {
            // Arrange
            var errors = new Dictionary<string, object?>
            {
                { "Field1", "Error message 1" },
                { "Field2", "Error message 2" }
            };

            // Act
            var result = ServiceResult.Error(errors);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailed);
            Assert.NotNull(result.Fail);
            Assert.Contains("Validation", result.Fail.Title);
            Assert.NotEmpty(result.Fail.Extensions ?? new Dictionary<string, object?>());
        }

        [Fact]
        public void Error_WithEmptyErrorsDictionary_ShouldReturnValidationError()
        {
            // Arrange
            var errors = new Dictionary<string, object?>();

            // Act
            var result = ServiceResult.Error(errors);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.BadRequest, result.Status);
            Assert.False(result.IsSuccess);
        }
    }
}
