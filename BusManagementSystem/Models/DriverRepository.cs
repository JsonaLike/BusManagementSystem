using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Models
{
    public class DriverRepository:IDriverRepository
    {
        ReservationSystemContext _reservationSystemContext;
        public DriverRepository(ReservationSystemContext _reservationSystemContext)
        {
            this._reservationSystemContext = _reservationSystemContext;
        }
        public IEnumerable<Driver> Drivers => _reservationSystemContext.Drivers
                .Include(d => d.LicenseTypeNavigation)
                .Include(d => d.Trips)
                .ThenInclude(t => t.Bus)
                .ThenInclude(b => b.BusMapping);
        public void Add(Driver driver)
        {
            _reservationSystemContext.Add(driver);
            _reservationSystemContext.SaveChanges();
        }

        public void Delete(Driver driver)
        {
            _reservationSystemContext.Remove(driver);
            _reservationSystemContext.SaveChanges();
        }

        public void Update(Driver driver)
        {
            _reservationSystemContext.Update(driver);
            _reservationSystemContext.SaveChanges();
        }
        public Driver Find(int DriverId)
        {
            return _reservationSystemContext.Drivers.Find(DriverId);
        }
    }
}
