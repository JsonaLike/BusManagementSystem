using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Models
{
    public class TripRepository:ITripRepository
    {
        ReservationSystemContext _reservationSystemContext;
        public TripRepository(ReservationSystemContext _reservationSystemContext)
        {
            this._reservationSystemContext = _reservationSystemContext;
        }
        public IEnumerable<Trip> Trips => _reservationSystemContext.Trips
        .Include(t => t.Bus).ThenInclude(b => b.BusMapping)
        .Include(t => t.DepartureCityNavigation)
        .Include(t => t.ArrivalCityNavigation)
        .Include(t => t.Driver).ToList();
        public void Add(Trip trip)
        {
            _reservationSystemContext.Add(trip);
            _reservationSystemContext.SaveChanges();
        }

        public void Delete(Trip trip)
        {
            _reservationSystemContext.Remove(trip);
            _reservationSystemContext.SaveChanges();
        }

        public void Update(Trip trip)
        {
            _reservationSystemContext.Update(trip);
            _reservationSystemContext.SaveChanges();
        }
        public Trip Find(int TripId)
        {
            return _reservationSystemContext.Trips.Find(TripId);
        }
    }
}
