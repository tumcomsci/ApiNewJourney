using ApiNewJourney.BusinessLayer.IService;
using ApiNewJourney.DataLayer.IDL;
using ApiNewJourney.Models;
using System.Formats.Asn1;

namespace ApiNewJourney.BusinessLayer.Service
{
    public class PackageService : IPackageService
    {
        private readonly IPackageDL _iPackageDL;
        public PackageService(IPackageDL iPackageDL)
        {
            _iPackageDL = iPackageDL;
        }

        public async Task<MessageResultModel> CreatePackage(PackageModel model)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var result = await _iPackageDL.CreatePackage(model);
                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
                msg_res.Data = ex.Data;
            }
            return msg_res;
        }

        public async Task<MessageResultModel> GetAllPackage()
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var result = await _iPackageDL.GetAllPackages();
                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
                msg_res.Data = ex.Data;
            }
            return msg_res;
        }

        public async Task<MessageResultModel> GetPackage(string packageId)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var result = await _iPackageDL.GetPackage(packageId);
                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
                msg_res.Data = ex.Data;
            }
            return msg_res;
        }

        public async Task<MessageResultModel> UpdatePackage(PackageModel model)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var result = await _iPackageDL.UpdatePackage(model);
                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
                msg_res.Data = ex.Data;
            }
            return msg_res;
        }
    }
}
