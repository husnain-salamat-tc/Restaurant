using Restaurant.Core.DTOS;

namespace Restaurant.Core.Repositories
{
    public interface IReservationRepository
    {
        Task AddReservationAsync(int customerId, int tableNo, DateTime dateOfReserve); 
        Task RemoveReservationAsync(int Id);
        Task UpdateReservationAsync(int reservationId,int newTableNo, DateTime newDateOfReserve);
        Task<List<ReservationDto>> GetAllReservationsAsync();
        Task DisplayAllReservationsAsync();
        Task<ReservationDto> GetReservationByIdAsync(int reservationId);
    }
}
