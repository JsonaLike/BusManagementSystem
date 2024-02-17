using BusManagementSystem.Roles;
using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Models
{
    public class TicketRepository:ITicketRepository
    {
        ReservationSystemContext _reservationSystemContext;
        public TicketRepository(ReservationSystemContext _reservationSystemContext)
        {
            this._reservationSystemContext = _reservationSystemContext;
        }

        public IEnumerable<Ticket> Tickets => _reservationSystemContext.Tickets.Include(t => t.Passenger)
            .Include(t => t.Trip)
                .ThenInclude(trip => trip.Bus)
            .Include(t => t.Trip)
                .ThenInclude(trip => trip.Driver)
            .Include(t => t.Trip)
            .ThenInclude(bus => bus.DepartureCityNavigation)
            .Include(t => t.Trip)
            .ThenInclude(bus => bus.ArrivalCityNavigation);
        public void Add(Ticket Ticket)
        {
            _reservationSystemContext.Add(Ticket);
            _reservationSystemContext.SaveChanges();
        }

        public void Delete(Ticket Ticket)
        {
            _reservationSystemContext.Remove(Ticket);
            _reservationSystemContext.SaveChanges();
        }

        public void Update(Ticket Ticket)
        {
            _reservationSystemContext.Update(Ticket);
            _reservationSystemContext.SaveChanges();
        }

        public Ticket Find(int PassengerId ,int TripId)
        {
            return _reservationSystemContext.Tickets.Include(t => t.Trip).SingleOrDefault(t => t.PassengerId == PassengerId && t.TripId == TripId);
        }
    }
}
