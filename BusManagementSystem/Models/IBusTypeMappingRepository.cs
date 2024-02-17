namespace BusManagementSystem.Models
{
    public interface IBusTypeMappingRepository
    {
        IEnumerable<BusTypeMapping> BusTypes { get; }
        void Add(BusTypeMapping BusType);
        void Update(BusTypeMapping BusType);
        void Delete(BusTypeMapping BusType);
        BusTypeMapping Find(int LicenseTypeId);
    }
}
