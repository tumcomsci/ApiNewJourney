using ApiNewJourney.Models;

namespace ApiNewJourney.DataLayer.IDL
{
    public interface IAccountDL
    {
        Task<AccountModel> GetAccount(string iBanNumber);
        Task<int> InsertAccount(AccountModel account);
        Task<int> UpdateAccount(AccountModel account);
    }
}
