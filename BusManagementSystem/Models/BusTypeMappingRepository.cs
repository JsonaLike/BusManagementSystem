using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Models
{
    public class BusTypeMappingRepository:IBusTypeMappingRepository
    {
        ReservationSystemContext _reservationSystemContext;
        public BusTypeMappingRepository(ReservationSystemContext _reservationSystemContext)
        {
            this._reservationSystemContext = _reservationSystemContext;
        }
        public IEnumerable<BusTypeMapping> BusTypes => _reservationSystemContext.BusTypeMappings;


        public void Add(BusTypeMapping BusTypeMapping)
        {
            _reservationSystemContext.Add(BusTypeMapping);
            _reservationSystemContext.SaveChanges();
        }

        public void Delete(BusTypeMapping BusTypeMapping)
        {
            _reservationSystemContext.Remove(BusTypeMapping);
            _reservationSystemContext.SaveChanges();
        }

        public void Update(BusTypeMapping BusTypeMapping)
        {
            _reservationSystemContext.Update(BusTypeMapping);
            _reservationSystemContext.SaveChanges();
        }
        public BusTypeMapping Find(int BusTypeId)
        {
            return _reservationSystemContext.BusTypeMappings.Find(BusTypeId);
        }

      
    }
}
