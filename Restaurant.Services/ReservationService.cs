using Restaurant.Core.DTOS;
using Restaurant.Core.Repositories;
using Restaurant.Core.Services;
using Restaurant.Repositories;

namespace Restaurant.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task AddReservationAsync(int customerId, int tableNo, DateTime dateOfReserve)
        {
            await _reservationRepository.AddReservationAsync(customerId, tableNo, dateOfReserve);
        }

        public async Task DisplayAllReservations()
        {
            await _reservationRepository.DisplayAllReservationsAsync();
        }

        public async Task<List<ReservationDto>> GetAllReservationsAsync()
        {
            return await _reservationRepository.GetAllReservationsAsync();
        }

        public async Task<ReservationDto> GetReservationByIdAsync(int reservationId)
        {
            return await _reservationRepository.GetReservationByIdAsync(reservationId);
        }

        public async Task RemoveReservationAsync(int reservationId)
        {
            await _reservationRepository.RemoveReservationAsync(reservationId);
        }

        public async Task UpdateReservationAsync(int reservationId, int newTableNo, DateTime newDateOfReserve)
        {
            await _reservationRepository.UpdateReservationAsync(reservationId, newTableNo, newDateOfReserve);
        }
    }

}
