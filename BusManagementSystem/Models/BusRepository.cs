using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Models
{
    public class BusRepository: IBusRepository
    {
        ReservationSystemContext _reservationSystemContext;
        public BusRepository(ReservationSystemContext _reservationSystemContext)
        {
            this._reservationSystemContext = _reservationSystemContext;
        }
        public IEnumerable<Bus> Buses => _reservationSystemContext.buses.Include(b => b.BusMapping);
        public void Add(Bus bus)
        {
            _reservationSystemContext.Add(bus);
            _reservationSystemContext.SaveChanges();
        }

        public void Delete(Bus bus)
        {
            _reservationSystemContext.Remove(bus);
            _reservationSystemContext.SaveChanges();
        }

        public void Update(Bus bus)
        {
            _reservationSystemContext.Update(bus);
            _reservationSystemContext.SaveChanges();
        }
        public Bus Find(int BusId)
        {
            return _reservationSystemContext.buses.Find(BusId);
        }
    }
}
