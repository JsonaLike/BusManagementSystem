namespace BusManagementSystem.Models
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> Tickets { get; }
        void Add(Ticket Ticket);
        void Update(Ticket Ticket);
        void Delete(Ticket Ticket);
        Ticket Find(int PassengerId, int TripId);
    }
}
