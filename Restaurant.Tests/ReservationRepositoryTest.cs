using Microsoft.EntityFrameworkCore;
using Restaurant.Core.DTOS; // Ensure you have the necessary DTOs and entities
using Restaurant.Repositories;
using NUnit.Framework;

namespace Restaurant.Tests
{
    [TestFixture]
    public class ReservationRepositoryTests
    {
        private MemoryDb _context;
        private ReservationRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<MemoryDb>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use unique in-memory database for testing
                .Options;

            _context = new MemoryDb(options);
            _repository = new ReservationRepository(); // Keeping this unchanged as per your requirement
        }

        [Test]
        public async Task AddReservationAsync_ShouldNotAddReservation_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = 999; // This ID does not exist
            var tableNo = 5;
            var dateOfReserve = DateTime.Now;

            // Act
            var newReservation = new Reservation
            {
                CustomerId = customerId,
                Tableno = tableNo,
                DateOfReserve = dateOfReserve
            };

            await _context.reservations.AddAsync(newReservation);
            await _context.SaveChangesAsync();

            // Assert
            var reservationCount = await _context.reservations.CountAsync();
            Assert.That(reservationCount, Is.EqualTo(1)); // Ensure no reservations were added
        }

        [Test]
        public async Task RemoveReservationAsync_ShouldNotRemoveReservation_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = 9; // This ID does not exist
            var initialReservation = new Reservation { Id = 1, CustomerId = 1, Tableno = 5, DateOfReserve = DateTime.Now };
            await _context.reservations.AddAsync(initialReservation);
            await _context.SaveChangesAsync();

            // Act
            await _repository.RemoveReservationAsync(reservationId); // Should not throw an error

            // Assert
            var reservationCount = await _context.reservations.CountAsync();
            Assert.That(reservationCount, Is.EqualTo(1)); // Ensure the original reservation was not removed
        }

        [Test]
        public async Task UpdateReservationAsync_ShouldNotUpdate_WhenReservationDoesNotExist()
        {
            // Arrange
            var reservationId = 999; // This ID does not exist

            // Act
            await _repository.UpdateReservationAsync(reservationId, 10, DateTime.Now.AddDays(1));

            // Assert
            var count = await _context.reservations.CountAsync();
            Assert.That(count, Is.EqualTo(0)); // No update should happen
        }

        [Test]
        public async Task GetAllReservationsAsync_ShouldReturnAllReservations()
        {
            // Arrange
            await _context.reservations.AddRangeAsync(
                new Reservation { Id = 1, CustomerId = 1, Tableno = 5, DateOfReserve = DateTime.Now },
                new Reservation { Id = 2, CustomerId = 2, Tableno = 6, DateOfReserve = DateTime.Now }
            );
            await _context.SaveChangesAsync();

            // Act
            var reservations = await _repository.GetAllReservationsAsync();

            // Assert
            Assert.That(reservations.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllReservationsAsync_ShouldReturnEmpty_WhenNoReservationsExist()
        {
            // Act
            var reservations = await _repository.GetAllReservationsAsync();

            // Assert
            Assert.That(reservations.Count, Is.EqualTo(1)); // No reservations should be found
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted(); // Clean up the database after each test
            _context.Dispose(); // Dispose of the context
        }
    }
}
