using ApiNewJourney.Models;

namespace ApiNewJourney.BusinessLayer.IService
{
    public interface IPackageService
    {
        Task<MessageResultModel> GetPackage(string packageId);
        Task<MessageResultModel> GetAllPackage();
        Task<MessageResultModel> CreatePackage(PackageModel model);
        Task<MessageResultModel> UpdatePackage(PackageModel model);
    }
}
