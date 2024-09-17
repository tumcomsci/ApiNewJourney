using ApiNewJourney.BusinessLayer.IService;
using ApiNewJourney.DataLayer.IDL;
using ApiNewJourney.Models;

namespace ApiNewJourney.BusinessLayer.Service
{
    public class AccountBalanceService : IAccountBalanceService
    {
        private readonly IAccountBalanceDL _iAccountBalanceDL;
        public AccountBalanceService(IAccountBalanceDL iAccountBalanceDL)
        {
            _iAccountBalanceDL = iAccountBalanceDL;
        }
        public async Task<MessageResultModel> CreateAccountBalance(AccountBalanceModel model)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var result = await _iAccountBalanceDL.InsertAccountBalance(model);
                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
            }
            return msg_res;
        }

        public async Task<MessageResultModel> GetAccountBalance(string accountId)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var result = await _iAccountBalanceDL.GetAccountBalance(accountId);
                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
            }
            return msg_res;
        }

        public async Task<MessageResultModel> UpdateAccountBalance(AccountBalanceModel model)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var result = await _iAccountBalanceDL.UpdateAccountBalance(model);
                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
            }
            return msg_res;

        }
    }
}
