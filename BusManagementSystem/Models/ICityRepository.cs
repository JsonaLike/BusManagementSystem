namespace BusManagementSystem.Models
{
    public interface ICityRepository
    {
        IEnumerable<City> Cities { get; }
        void Add(City city);
        void Update(City city);
        void Delete(City city);
        City Find(int CityId);

    }
}