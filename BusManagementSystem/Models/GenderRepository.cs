using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Models
{
    public class GenderRepository:IGenderMappingRepository
    {
        ReservationSystemContext _reservationSystemContext;
        public GenderRepository(ReservationSystemContext _reservationSystemContext)
        {
            this._reservationSystemContext = _reservationSystemContext;
        }
        public IEnumerable<GenderMapping> GenderMappings => _reservationSystemContext.GenderMappings;


        public void Add(GenderMapping GenderMapping)
        {
            _reservationSystemContext.Add(GenderMapping);
            _reservationSystemContext.SaveChanges();
        }

        public void Delete(GenderMapping GenderMapping)
        {
            _reservationSystemContext.Remove(GenderMapping);
            _reservationSystemContext.SaveChanges();
        }

        public void Update(GenderMapping GenderMapping)
        {
            _reservationSystemContext.Update(GenderMapping);
            _reservationSystemContext.SaveChanges();
        }
        public GenderMapping Find(int GenderId)
        {
            return _reservationSystemContext.GenderMappings.Find(GenderId);
        }
    }
}
