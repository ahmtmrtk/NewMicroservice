using Xunit;
using NewMicroservice.Shared.Services;

namespace NewMicroservice.Shared.Tests
{
    public class IdentityServiceFakeTests
    {
        [Fact]
        public void GetUserId_ShouldReturnExpectedGuid()
        {
            // Arrange
            var identityServiceFake = new IdentityServiceFake();
            var expectedUserId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6");

            // Act
            var result = identityServiceFake.GetUserId;

            // Assert
            Assert.Equal(expectedUserId, result);
        }

        [Fact]
        public void GetUserId_ShouldReturnConsistentValue()
        {
            // Arrange
            var identityServiceFake = new IdentityServiceFake();

            // Act
            var firstCall = identityServiceFake.GetUserId;
            var secondCall = identityServiceFake.GetUserId;

            // Assert
            Assert.Equal(firstCall, secondCall);
        }

        [Fact]
        public void Username_ShouldReturnExpectedUsername()
        {
            // Arrange
            var identityServiceFake = new IdentityServiceFake();
            var expectedUsername = "Ahmet16";

            // Act
            var result = identityServiceFake.Username;

            // Assert
            Assert.Equal(expectedUsername, result);
        }

        [Fact]
        public void Username_ShouldReturnConsistentValue()
        {
            // Arrange
            var identityServiceFake = new IdentityServiceFake();

            // Act
            var firstCall = identityServiceFake.Username;
            var secondCall = identityServiceFake.Username;

            // Assert
            Assert.Equal(firstCall, secondCall);
        }

        [Fact]
        public void IdentityServiceFake_ShouldInstantiateSuccessfully()
        {
            // Act
            var identityServiceFake = new IdentityServiceFake();

            // Assert
            Assert.NotNull(identityServiceFake);
            Assert.NotNull(identityServiceFake.Username);
            Assert.NotEmpty(identityServiceFake.Username);
        }

        [Fact]
        public void GetUserId_ShouldNotBeEmptyGuid()
        {
            // Arrange
            var identityServiceFake = new IdentityServiceFake();

            // Act
            var result = identityServiceFake.GetUserId;

            // Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public void Username_ShouldNotBeNullOrEmpty()
        {
            // Arrange
            var identityServiceFake = new IdentityServiceFake();

            // Act
            var result = identityServiceFake.Username;

            // Assert
            Assert.False(string.IsNullOrEmpty(result));
        }
    }
}
