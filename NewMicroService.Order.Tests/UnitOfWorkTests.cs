using Xunit;
using Moq;
using NewMicroService.Order.Persistance;
using NewMicroService.Order.Persistance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace NewMicroService.Order.Tests
{
    public class UnitOfWorkTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;

        public UnitOfWorkTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid().ToString())
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new AppDbContext(options);
            _unitOfWork = new UnitOfWork(_context);
        }

        [Fact]
        public async Task CommitAsync_ShouldCallSaveChangesAsync()
        {
            // Act
            var result = await _unitOfWork.CommitAsync();

            // Assert
            Assert.Equal(0, result); // No changes in empty context
        }

        [Fact]
        public async Task CommitAsync_ShouldReturnNumberOfAffectedRows()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid().ToString())
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new AppDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context);
                var order = Domain.Entities.Order.CreateUnPaidOrder(Guid.NewGuid(), null);

                context.Orders.Add(order);

                // Act
                var result = await unitOfWork.CommitAsync();

                // Assert
                Assert.Equal(1, result);
            }
        }

        [Fact]
        public async Task BeginTransactionAsync_ShouldBeCallable()
        {
            // Act & Assert - InMemory database returns null for CurrentTransaction, but call should succeed
            await _unitOfWork.BeginTransactionAsync();
            // No exception should be thrown
        }

        [Fact]
        public async Task CommitTransactionAsync_ShouldBeCallable()
        {
            // Arrange
            await _unitOfWork.BeginTransactionAsync();

            // Act & Assert
            await _unitOfWork.CommitTransactionAsync();
            // No exception should be thrown
        }

        [Fact]
        public async Task BeginTransactionAsync_ThenCommitTransactionAsync_ShouldWork()
        {
            // Act
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.CommitTransactionAsync();

            // Assert - operations should complete without error
            Assert.True(true);
        }

        [Fact]
        public async Task CommitAsync_WithCancellationToken_ShouldCompleteSuccessfully()
        {
            // Arrange
            var cts = new CancellationTokenSource();

            // Act
            var result = await _unitOfWork.CommitAsync(cts.Token);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task BeginTransactionAsync_WithCancellationToken_ShouldBeCallable()
        {
            // Arrange
            var cts = new CancellationTokenSource();

            // Act & Assert
            await _unitOfWork.BeginTransactionAsync(cts.Token);
            // No exception should be thrown
        }

        [Fact]
        public async Task CommitTransactionAsync_WithCancellationToken_ShouldBeCallable()
        {
            // Arrange
            var cts = new CancellationTokenSource();
            await _unitOfWork.BeginTransactionAsync(cts.Token);

            // Act & Assert
            await _unitOfWork.CommitTransactionAsync(cts.Token);
            // No exception should be thrown
        }

        [Fact]
        public async Task CommitAsync_WithMultipleChanges_ShouldReturnCorrectCount()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid().ToString())
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            using (var context = new AppDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context);
                var order1 = Domain.Entities.Order.CreateUnPaidOrder(Guid.NewGuid(), null);
                var order2 = Domain.Entities.Order.CreateUnPaidOrder(Guid.NewGuid(), 10f);

                context.Orders.Add(order1);
                context.Orders.Add(order2);

                // Act
                var result = await unitOfWork.CommitAsync();

                // Assert
                Assert.Equal(2, result);
            }
        }

        [Fact]
        public async Task UnitOfWork_ShouldPersistDataCorrectly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid().ToString())
                .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            Guid orderId;
            var buyerId = Guid.NewGuid();

            // Act - Create and save order
            using (var context = new AppDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context);
                var order = Domain.Entities.Order.CreateUnPaidOrder(buyerId, null);
                orderId = order.Id;

                context.Orders.Add(order);
                await unitOfWork.CommitAsync();
            }

            // Assert - Verify data persists
            using (var context = new AppDbContext(options))
            {
                var savedOrder = context.Orders.FirstOrDefault(o => o.Id == orderId);
                Assert.NotNull(savedOrder);
                Assert.Equal(buyerId, savedOrder.BuyerId);
            }
        }

        [Fact]
        public async Task CommitAsync_WithDefaultCancellationToken_ShouldCompleteSuccessfully()
        {
            // Act
            var result = await _unitOfWork.CommitAsync(default(CancellationToken));

            // Assert
            Assert.Equal(0, result);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
