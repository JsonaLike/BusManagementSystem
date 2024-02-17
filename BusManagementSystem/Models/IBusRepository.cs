namespace BusManagementSystem.Models
{
    public interface IBusRepository
    {
        IEnumerable<Bus> Buses { get; }
        void Add(Bus bus);
        void Update(Bus bus);
        void Delete(Bus bus);
        Bus Find(int BusId);
    }
}