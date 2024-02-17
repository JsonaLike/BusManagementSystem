using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Models
{
    public class PassengerRepository:IPassengerRepository
    {
        ReservationSystemContext _reservationSystemContext;
        public PassengerRepository(ReservationSystemContext _reservationSystemContext)
        {
            this._reservationSystemContext = _reservationSystemContext;
        }
        public IEnumerable<Passenger> Passengers => _reservationSystemContext.Passengers;
        public void Add(Passenger passenger)
        {
            _reservationSystemContext.Add(passenger);
            _reservationSystemContext.SaveChanges();
        }

        public void Delete(Passenger passenger)
        {
            _reservationSystemContext.Remove(passenger);
            _reservationSystemContext.SaveChanges();
        }

        public void Update(Passenger passenger)
        {
            _reservationSystemContext.Update(passenger);
            _reservationSystemContext.SaveChanges();
        }
        public Passenger Find(int PassengerId)
        {
            return _reservationSystemContext.Passengers.Find(PassengerId);
        }
    }
}
