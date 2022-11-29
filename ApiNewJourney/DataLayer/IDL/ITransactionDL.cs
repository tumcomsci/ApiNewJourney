using ApiNewJourney.Models;

namespace ApiNewJourney.DataLayer.IDL
{
    public interface ITransactionDL
    {
        Task<int> CreateTransaction(TransactionModel model);
        Task<int> UpdateTransaction(TransactionModel model);
        Task<List<TransactionModel>> GetAllTransactionByAccountId(string accountId);
    }
}
