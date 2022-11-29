using ApiNewJourney.Models;

namespace ApiNewJourney.DataLayer.IDL
{
    public interface IAccountBalanceDL
    {
        Task<AccountBalanceModel> GetAccountBalance(string accountId);
        Task<int> InsertAccountBalance(AccountBalanceModel model);
        Task<int> UpdateAccountBalance(AccountBalanceModel model);

    }
}
