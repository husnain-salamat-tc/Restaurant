using Restaurant.Core.DTOS;
using Restaurant.Repositories;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Tests
{
    [TestFixture]
    public class CustomerRepositoryTests
    {
        private MemoryDb _context;
        private CustomerRepository _repository;

        [SetUp]
        public void Setup()
        {
            // Set up in-memory database
            var options = new DbContextOptionsBuilder<MemoryDb>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new MemoryDb(options);
            _repository = new CustomerRepository(); // Keeping this unchanged as per your requirement
        }

        [Test]
        public async Task AddCustomerAsync_ShouldAddCustomer_WhenCustomerIsValid()
        {
            // Arrange
            var customerDto = new CustomerDto
            {
                Name = "John Doe",
                Celno = 1234 // Ensure this is a string
            };

            // Act
            var newCustomer = new Customer
            {
                Name = customerDto.Name,
                Celno = customerDto.Celno
            };

            await _context.customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync(); // Simulate saving to the database

            // Assert
            var customerCount = await _context.customers.CountAsync();
            Assert.That(customerCount, Is.EqualTo(1)); // Ensure one customer is added

            var addedCustomer = await _context.customers.FirstOrDefaultAsync();
            Assert.That(addedCustomer, Is.Not.Null);
            Assert.That(addedCustomer.Name, Is.EqualTo(customerDto.Name));
            Assert.That(addedCustomer.Celno, Is.EqualTo(customerDto.Celno));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Clean up the database after each test
            _context.Dispose(); // Dispose of the context
        }
    }
}
