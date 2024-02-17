namespace BusManagementSystem.Models
{
    public interface IGenderMappingRepository
    {
        IEnumerable<GenderMapping> GenderMappings { get; }
        void Add(GenderMapping GenderMapping);
        void Update(GenderMapping GenderMapping);
        void Delete(GenderMapping GenderMapping);
        GenderMapping Find(int GenderId);
    }
}
