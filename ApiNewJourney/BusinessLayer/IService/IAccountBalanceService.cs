using ApiNewJourney.Models;

namespace ApiNewJourney.BusinessLayer.IService
{
    public interface IAccountBalanceService
    {
        Task<MessageResultModel> CreateAccountBalance(AccountBalanceModel model);
        Task<MessageResultModel> UpdateAccountBalance(AccountBalanceModel model);
        Task<MessageResultModel> GetAccountBalance(string accountId);
    }
}
