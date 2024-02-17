namespace BusManagementSystem.Models
{
    public interface IDriverRepository
    {
        IEnumerable<Driver> Drivers { get; }
        void Add(Driver driver);
        void Update(Driver driver);
        void Delete(Driver driver);
        Driver Find(int DriverId);
    }
}
