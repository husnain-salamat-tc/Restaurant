using Restaurant.Core.DTOS;

namespace Restaurant.Core.Services
{
    public interface IReservationService
    {
        Task AddReservationAsync(int customerId, int tableNo, DateTime dateOfReserve);
        Task RemoveReservationAsync(int n);
        Task UpdateReservationAsync(int reservationId,int newTableNo, DateTime newDateOfReserve);
        Task<List<ReservationDto>> GetAllReservationsAsync();
        Task DisplayAllReservations();
        Task<ReservationDto> GetReservationByIdAsync(int reservationId);
    }
}
