using ApiNewJourney.BusinessLayer.IService;
using ApiNewJourney.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiNewJourney.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PackageController : Controller
    {
        private readonly IPackageService _iPackageService;
        public PackageController(IPackageService iPackageService)
        {
            _iPackageService = iPackageService;
        }

        [HttpGet(nameof(GetPackage))]
        public async Task<MessageResultModel> GetPackage(string packageId)
        {
            return await _iPackageService.GetPackage(packageId);
        }

        [HttpGet(nameof(GetAllPackage))]
        public async Task<MessageResultModel> GetAllPackage()
        {
            return await _iPackageService.GetAllPackage();
        }

        [HttpPost(nameof(CreatePackage))]
        public async Task<MessageResultModel> CreatePackage([FromBody]PackageModel model)
        {
            return await _iPackageService.CreatePackage(model);
        }

        [HttpPut(nameof(UpdatePackage))]
        public async Task<MessageResultModel> UpdatePackage([FromBody]PackageModel model)
        {
            return await _iPackageService.UpdatePackage(model);
        }
    }
}
