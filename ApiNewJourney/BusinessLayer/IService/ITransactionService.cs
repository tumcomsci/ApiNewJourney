using ApiNewJourney.Models;

namespace ApiNewJourney.BusinessLayer.IService
{
    public interface ITransactionService
    {
        Task<MessageResultModel> CreateTransaction(string accountBalanceIdOrigin, string accountBalanceIdDest, string packageId, decimal amount);
        Task<MessageResultModel> CreatePackageTransaction(AccountBalanceModel accountBalanceIdOrigin, AccountBalanceModel accountBalanceIdDest, PackageModel package, decimal? amount);
        Task<MessageResultModel> UpdateTransaction(TransactionModel origin);
    }
}
