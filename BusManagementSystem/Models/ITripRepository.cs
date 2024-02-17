namespace BusManagementSystem.Models
{
    public interface ITripRepository
    {
        IEnumerable<Trip> Trips { get; }
        void Add(Trip Trip);
        void Update(Trip Trip);
        void Delete(Trip Trip);
        Trip Find(int TripId);
    }
}
