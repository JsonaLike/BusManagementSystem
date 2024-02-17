namespace BusManagementSystem.Models
{
    public interface IPassengerRepository
    {
        IEnumerable<Passenger> Passengers { get; }
        void Add(Passenger Passenger);
        void Update(Passenger Passenger);
        void Delete(Passenger Passenger);
        Passenger Find(int PassengerId);
    }
}
