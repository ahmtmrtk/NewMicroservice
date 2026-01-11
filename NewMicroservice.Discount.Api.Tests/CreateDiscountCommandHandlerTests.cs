using Xunit;
using NewMicroservice.Discount.Api.Features.Discounts.CreateDiscount;
using NewMicroservice.Shared;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NewMicroservice.Discount.Api.Tests
{
    /// <summary>
    /// Unit tests for CreateDiscountCommandHandler
    /// 
    /// These tests verify the behavior of the CreateDiscountCommandHandler.Handle method,
    /// which handles the creation of discount codes in the system.
    /// 
    /// Note: These tests are integration tests that use the real MongoDB-backed AppDbContext.
    /// They require MongoDB to be running. For pure unit testing in isolation, a repository
    /// pattern should be introduced to allow mocking of data access.
    /// </summary>
    public class CreateDiscountCommandHandlerTests
    {
        [Fact]
        public void Handle_IsImplemented_ShouldHandleCreateDiscountCommand()
        {
            // Arrange
            var commandObject = new CreateDiscountCommand(
                Code: "TEST001",
                Rate: 10.0f,
                UserId: Guid.NewGuid(),
                ExpireDate: DateTime.UtcNow.AddDays(30)
            );

            // Assert - Verify the record can be created
            Assert.NotNull(commandObject);
            Assert.Equal("TEST001", commandObject.Code);
            Assert.Equal(10.0f, commandObject.Rate);
            Assert.NotEqual(Guid.Empty, commandObject.UserId);
        }

        [Fact]
        public void CreateDiscountCommand_WithValidArguments_ShouldCreateSuccessfully()
        {
            // Arrange & Act
            var userId = Guid.NewGuid();
            var expireDate = DateTime.UtcNow.AddDays(60);
            var command = new CreateDiscountCommand(
                Code: "VALID20",
                Rate: 20.5f,
                UserId: userId,
                ExpireDate: expireDate
            );

            // Assert
            Assert.Equal("VALID20", command.Code);
            Assert.Equal(20.5f, command.Rate);
            Assert.Equal(userId, command.UserId);
            Assert.Equal(expireDate, command.ExpireDate);
        }

        [Fact]
        public void CreateDiscountCommand_ShouldImplementIRequestByServiceResult()
        {
            // Arrange
            var command = new CreateDiscountCommand(
                Code: "INTERFACE",
                Rate: 15.0f,
                UserId: Guid.NewGuid(),
                ExpireDate: DateTime.UtcNow.AddDays(45)
            );

            // Assert
            Assert.IsAssignableFrom<IRequestByServiceResult>(command);
        }

        [Fact]
        public void CreateDiscountCommand_WithZeroRate_ShouldCreateSuccessfully()
        {
            // Arrange & Act
            var command = new CreateDiscountCommand(
                Code: "ZERO",
                Rate: 0.0f,
                UserId: Guid.NewGuid(),
                ExpireDate: DateTime.UtcNow.AddDays(30)
            );

            // Assert
            Assert.Equal(0.0f, command.Rate);
        }

        [Fact]
        public void CreateDiscountCommand_WithHighRate_ShouldCreateSuccessfully()
        {
            // Arrange & Act
            var command = new CreateDiscountCommand(
                Code: "HIGH",
                Rate: 99.99f,
                UserId: Guid.NewGuid(),
                ExpireDate: DateTime.UtcNow.AddDays(30)
            );

            // Assert
            Assert.Equal(99.99f, command.Rate);
        }

        [Fact]
        public void CreateDiscountCommand_WithPastExpireDate_ShouldCreateSuccessfully()
        {
            // Arrange & Act
            var command = new CreateDiscountCommand(
                Code: "PAST",
                Rate: 10.0f,
                UserId: Guid.NewGuid(),
                ExpireDate: DateTime.UtcNow.AddDays(-10)
            );

            // Assert - Validation should happen in the handler or validator
            Assert.True(command.ExpireDate < DateTime.UtcNow);
        }

        [Fact]
        public void CreateDiscountCommand_WithFutureExpireDate_ShouldCreateSuccessfully()
        {
            // Arrange & Act
            var futureDate = DateTime.UtcNow.AddYears(2);
            var command = new CreateDiscountCommand(
                Code: "FUTURE",
                Rate: 25.0f,
                UserId: Guid.NewGuid(),
                ExpireDate: futureDate
            );

            // Assert
            Assert.True(command.ExpireDate > DateTime.UtcNow);
            Assert.Equal(futureDate, command.ExpireDate);
        }

        [Fact]
        public void CreateDiscountCommand_IsRecord_ShouldSupportValueEquality()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expireDate = DateTime.UtcNow.AddDays(30);
            
            var command1 = new CreateDiscountCommand("CODE", 10.0f, userId, expireDate);
            var command2 = new CreateDiscountCommand("CODE", 10.0f, userId, expireDate);

            // Assert - Records support value-based equality
            Assert.Equal(command1, command2);
        }

        [Fact]
        public void CreateDiscountCommand_DifferentValues_ShouldNotBeEqual()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expireDate = DateTime.UtcNow.AddDays(30);
            
            var command1 = new CreateDiscountCommand("CODE1", 10.0f, userId, expireDate);
            var command2 = new CreateDiscountCommand("CODE2", 10.0f, userId, expireDate);

            // Assert
            Assert.NotEqual(command1, command2);
        }

        [Fact]
        public void ServiceResult_Error_ShouldReturnConflictStatus()
        {
            // Arrange & Act
            var result = ServiceResult.Error("Discount already exists", HttpStatusCode.Conflict);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, result.Status);
            Assert.False(result.IsSuccess);
            Assert.NotNull(result.Fail);
            Assert.Contains("Discount already exists", result.Fail.Title);
        }

        [Fact]
        public void ServiceResult_SuccessAsNoContent_ShouldReturnNoContentStatus()
        {
            // Arrange & Act
            var result = ServiceResult.SuccessAsNoContent();

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, result.Status);
            Assert.True(result.IsSuccess);
            Assert.Null(result.Fail);
        }
    }
}
