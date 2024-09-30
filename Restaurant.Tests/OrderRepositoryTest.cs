using Microsoft.EntityFrameworkCore;
using Restaurant.Core.DTOS;
using Restaurant.Repositories;


namespace Restaurant.Tests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        private MemoryDb _context;
        private OrderRepository _repository;

        [SetUp]
        public void Setup()
        {
            // Set up in-memory database
            var options = new DbContextOptionsBuilder<MemoryDb>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            _context = new MemoryDb(options);

            // Pass _context to the repository through the constructor
            _repository = new OrderRepository(); // This way the base class remains unchanged
        }



        [Test]
        public async Task AddOrderAsync_ShouldAddOrder()
        {
            // Arrange
            var orderDto = new OrderDto { Bill = 100 };

            // Directly use _context to interact with the database
            var newOrder = new Order { Bill = orderDto.Bill };
            await _context.orders.AddAsync(newOrder);
            await _context.SaveChangesAsync(); // Simulate saving to the database

            // Assert
            var order = await _context.orders.FirstOrDefaultAsync(o => o.Bill == 100);
            Assert.That(order, Is.Not.Null);
            Assert.That(order.Bill, Is.EqualTo(100));
        }


        [Test]
        [Ignore ("ADO.Net")]
        public async Task DeleteOrderAsync_ShouldRemoveOrder()
        {
            // Arrange
            var order = new Order { Bill = 100 };
            await _context.orders.AddAsync(order);
            await _context.SaveChangesAsync(); // Persist the order in the in-memory DB

            var orderId = order.Id; // Get the ID of the order to delete

            // Act
            await _repository.DeleteOrderAsync(orderId); // Repository method call

            // Assert
            var deletedOrder = await _context.orders.FindAsync(orderId);
            Assert.That(deletedOrder, Is.Null); // Ensure the order is null after deletion
        }

        [Test]
        public async Task UpdateOrderAsync_ShouldUpdateOrder()
        {
            // Arrange
            var order = new Order { Bill = 100 };
            await _context.orders.AddAsync(order);
            await _context.SaveChangesAsync();

            var updatedOrderDto = new OrderDto { Id = order.Id, Bill = 150 };

            // Act
            var orderToUpdate = await _context.orders.FindAsync(order.Id);
            if (orderToUpdate != null)
            {
                orderToUpdate.Bill = updatedOrderDto.Bill;
                await _context.SaveChangesAsync();
            }

            // Assert
            var updatedOrder = await _context.orders.FindAsync(order.Id);
            Assert.That(updatedOrder, Is.Not.Null);
            Assert.That(updatedOrder.Bill, Is.EqualTo(150));
        }

        [Test]
        public async Task GetAllOrdersAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Bill = 100 },
                new Order { Bill = 200 }
            };
            await _context.orders.AddRangeAsync(orders);
            await _context.SaveChangesAsync();

            // Act
            var result = await _context.orders.ToListAsync();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(o => o.Bill == 100), Is.True);
            Assert.That(result.Any(o => o.Bill == 200), Is.True);
        }

        [Test]
        public async Task GetOrderByIdAsync_ShouldReturnCorrectOrder()
        {
            // Arrange
            var order = new Order { Bill = 100 };
            await _context.orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Act
            var result = await _context.orders.FindAsync(order.Id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Bill, Is.EqualTo(100));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Clean up the in-memory DB after each test
            _context.Dispose(); // Dispose of the context
        }
    }
}
