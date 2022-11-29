using ApiNewJourney.Models;

namespace ApiNewJourney.BusinessLayer.IService
{
    public interface ILogTransactionService
    {
        Task<MessageResultModel> CreateLogTransactionService(string Action, string Request, string Response, string FunctionName, string ApiUrl, string UpdateBy);
    }
}
