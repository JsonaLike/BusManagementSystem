namespace BusManagementSystem.Models
{
    public class CityRepository:ICityRepository
    {
        ReservationSystemContext _reservationSystemContext;
        public CityRepository(ReservationSystemContext _reservationSystemContext) {
        this._reservationSystemContext = _reservationSystemContext;
        }

        public IEnumerable<City> Cities => _reservationSystemContext.Cities;

        public void Add(City city)
        {
            _reservationSystemContext.Add(city);
            _reservationSystemContext.SaveChanges();
        }

        public void Delete(City city)
        {
            _reservationSystemContext.Remove(city);
            _reservationSystemContext.SaveChanges();
        }

        public void Update(City city)
        {
            _reservationSystemContext.Update(city);
            _reservationSystemContext.SaveChanges();
        }
        public City Find(int CityId)
        {
            return _reservationSystemContext.Cities.Find(CityId);
        }
    }
}
