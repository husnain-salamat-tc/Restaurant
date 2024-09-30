namespace Restaurant.Core.DTOS
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int Tableno { get; set; }
        public int CustomerDtoId { get; set; }
        public DateTime DateOfReserve { get; set; }
        public CustomerDto customerDto { get; set; }


    }
}
