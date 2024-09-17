using System.Threading.Tasks;
using ApiNewJourney.Models;
namespace ApiNewJourney.BusinessLayer.IService
{
    public interface IAccountService
    {
        Task<MessageResultModel> CreateAccount(AccountModel _account);
        Task<MessageResultModel> UpdateAccount(AccountModel _account);
        Task<MessageResultModel> GetAccount(string _accountId);

    }
}
