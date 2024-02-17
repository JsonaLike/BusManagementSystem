namespace BusManagementSystem.Models
{
    public interface ILicenseTypeMappingRepository
    {
        IEnumerable<LicenseTypeMapping> LicenseTypes { get; }
        void Add(LicenseTypeMapping licenseType);
        void Update(LicenseTypeMapping licenseType);
        void Delete(LicenseTypeMapping licenseType);
        LicenseTypeMapping Find(int LicenseTypeId);
    }
}