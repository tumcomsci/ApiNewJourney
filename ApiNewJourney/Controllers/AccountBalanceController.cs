using ApiNewJourney.BusinessLayer.IService;
using ApiNewJourney.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiNewJourney.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AccountBalanceController : Controller
    {
        private readonly IAccountBalanceService _iAccountBalanceService;
        public AccountBalanceController(IAccountBalanceService iAccountBalanceService)
        {
            _iAccountBalanceService = iAccountBalanceService;
        }

        [HttpGet(nameof(GetAccountBalance))]
        public async Task<MessageResultModel> GetAccountBalance(string accountId)
        {
            return await _iAccountBalanceService.GetAccountBalance(accountId);
        }

        [HttpPost(nameof(CreateAccountBalance))]
        public async Task<MessageResultModel> CreateAccountBalance(AccountBalanceModel model)
        {
            return await _iAccountBalanceService.CreateAccountBalance(model);
        }

        [HttpPut(nameof(UpdateAccountBalance))]
        public async Task<MessageResultModel> UpdateAccountBalance(AccountBalanceModel model)
        {
            return await _iAccountBalanceService.UpdateAccountBalance(model);
        }
    }
}
