using Microsoft.EntityFrameworkCore;

namespace BusManagementSystem.Models
{
    public class LicenseTypeMappingRepository:ILicenseTypeMappingRepository
    {
        ReservationSystemContext _reservationSystemContext;
        public LicenseTypeMappingRepository(ReservationSystemContext _reservationSystemContext)
        {
            this._reservationSystemContext = _reservationSystemContext;
        }
        public IEnumerable<LicenseTypeMapping> LicenseTypes => _reservationSystemContext.LicenseTypeMappings;
        public void Add(LicenseTypeMapping LicenseTypeMapping)
        {
            _reservationSystemContext.Add(LicenseTypeMapping);
            _reservationSystemContext.SaveChanges();
        }

        public void Delete(LicenseTypeMapping LicenseTypeMapping)
        {
            _reservationSystemContext.Remove(LicenseTypeMapping);
            _reservationSystemContext.SaveChanges();
        }

        public void Update(LicenseTypeMapping LicenseTypeMapping)
        {
            _reservationSystemContext.Update(LicenseTypeMapping);
            _reservationSystemContext.SaveChanges();
        }
        public LicenseTypeMapping Find(int LicenseTypeId)
        {
            return _reservationSystemContext.LicenseTypeMappings.Find(LicenseTypeId);
        }
    }
}
