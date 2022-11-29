using ApiNewJourney.BusinessLayer.IService;
using ApiNewJourney.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiNewJourney.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _iTransactionService;
        private readonly IPackageService _iPackageService;
        private readonly IAccountBalanceService _iAccountBalanceService;

        public TransactionController(ITransactionService transactionService, IPackageService packageService, IAccountBalanceService accountBalanceService)
        {
            _iTransactionService = transactionService;
            _iPackageService = packageService;
            _iAccountBalanceService= accountBalanceService;
        }

        [HttpPost(nameof(CreateTransaction))]
        public async Task<MessageResultModel> CreateTransaction(string accountIdOrigin, string accountIdDest, string packageId, decimal amount)
        {
            return await _iTransactionService.CreateTransaction(accountIdOrigin, accountIdDest, packageId, amount);
        }
    }
}
