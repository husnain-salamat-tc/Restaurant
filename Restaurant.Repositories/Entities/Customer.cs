namespace Restaurant.Repositories
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Celno { get; set; }  
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
    }


}
