using ApiNewJourney.Models;

namespace ApiNewJourney.DataLayer.IDL
{
    public interface IPackageDL
    {
        Task<PackageModel> GetPackage(string packageId);
        Task<List<PackageModel>> GetAllPackages();
        Task<int> CreatePackage(PackageModel model);
        Task<int> UpdatePackage(PackageModel model);
    }
}
