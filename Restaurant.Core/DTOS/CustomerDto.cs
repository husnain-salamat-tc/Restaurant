namespace Restaurant.Core.DTOS
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Celno { get; set; }
        public List<ReservationDto> Reservations { get; set; } = new List<ReservationDto>();

    }
}
