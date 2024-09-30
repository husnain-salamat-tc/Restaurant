using Microsoft.EntityFrameworkCore;
using Restaurant.Core.DTOS;
using Restaurant.Core.Repositories;

namespace Restaurant.Repositories
{
    public class ReservationRepository : IReservationRepository
    {

        public async Task AddReservationAsync(int customerId, int tableNo, DateTime dateOfReserve)
        {
            var _context = new RestaurantDbContext();
            try
                {

                    var customerExists = await _context.Customers.AnyAsync(c => c.Id == customerId);
                    if (!customerExists)
                    {
                        Console.WriteLine($"Customer with ID {customerId} does not exist.");
                        return; // Exit if the customer does not exist
                    }

                    // Create a new Reservation entity
                    Reservation reservation = new Reservation
                    {
                        CustomerId = customerId, // Set the customer ID
                        Tableno = tableNo,       // Set the table number
                        DateOfReserve = dateOfReserve // Set the reservation date
                    };

                    // Add the Reservation entity to the context
                    _context.Reservations.Add(reservation);

                    // Save changes to the database asynchronously
                    await _context.SaveChangesAsync();

                    Console.WriteLine("Reservation made successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while adding the reservation: {ex.Message}");
                }
            }
        





        public async Task RemoveReservationAsync(int reservationId)
        {

            var _context = new RestaurantDbContext();
            try
                {
                    // Find the reservation by ID
                    var reservation = await _context.Reservations.FindAsync(reservationId);

                    if (reservation != null)
                    {
                        // Remove the reservation from the context
                        _context.Reservations.Remove(reservation);

                        // Save changes to the database
                        await _context.SaveChangesAsync();

                        Console.WriteLine("Reservation removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Reservation not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            
        }


        public async Task UpdateReservationAsync(int reservationId,int newTableNo, DateTime newDateOfReserve)
        {
            var _context = new RestaurantDbContext();
            try
                {
                    // Find the existing reservation by its ID
                    var reservation = await _context.Reservations.FindAsync(reservationId);
                    if (reservation == null)
                    {
                        Console.WriteLine("Reservation not found");
                        return;
                    }

                    // Update the reservation's details
                    reservation.DateOfReserve = newDateOfReserve;
                    reservation.Tableno= newTableNo;    

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Reservation updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Not Found");
                }
            }
        
        public async Task<List<ReservationDto>> GetAllReservationsAsync()
        {
            var _context = new RestaurantDbContext();
            try
                {
                    // Fetch all reservations asynchronously
                    var reservations = await _context.Reservations.ToListAsync();

                    // Map to ReservationDto
                    var reservationDtos = reservations.Select(r => new ReservationDto
                    {
                        Id = r.Id,
                        CustomerDtoId = r.CustomerId,
                        DateOfReserve = r.DateOfReserve
                    }).ToList();

                    return reservationDtos;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return new List<ReservationDto>(); // Return an empty list on error
                }
            }
        

        public async Task DisplayAllReservationsAsync()
        {
            var reservationDtos = await GetAllReservationsAsync(); // Await the async method

            if (reservationDtos.Count == 0)
            {
                Console.WriteLine("No reservations found.");
                return; // No need to return a value, just exit
            }

            foreach (var reservationDto in reservationDtos)
            {
                Console.WriteLine($"Reservation ID: {reservationDto.Id}");
                Console.WriteLine($"Customer ID: {reservationDto.CustomerDtoId}");
                Console.WriteLine($"Table no: {reservationDto.Tableno}");
                Console.WriteLine($"Reservation Date: {reservationDto.DateOfReserve}");
                Console.WriteLine("----------------------------------------------------");
            }
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int reservationId)
        {
            var _context = new RestaurantDbContext();
            try
                {
                    // Find the reservation by ID asynchronously
                    var reservation = await _context.Reservations.FindAsync(reservationId);

                    if (reservation == null)
                    {
                        Console.WriteLine($"Reservation with ID {reservationId} not found.");
                        return null; // Return null if not found
                    }

                    // Map to ReservationDto
                    var reservationDto = new ReservationDto
                    {
                        Id = reservation.Id,
                        CustomerDtoId = reservation.CustomerId,
                        DateOfReserve = reservation.DateOfReserve
                    };

                    return reservationDto;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return null; // Return null on error
                }
            }
        }

    }




