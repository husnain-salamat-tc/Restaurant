namespace Restaurant.Repositories
{
    public class Reservation
    {
        public int Id { get; set; }
        public int Tableno { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateOfReserve { get; set; }

        public Customer Customer { get; set; }
    }

}
