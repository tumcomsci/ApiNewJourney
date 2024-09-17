using ApiNewJourney.BusinessLayer.IService;
using ApiNewJourney.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiNewJourney.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        public readonly IAccountService _service;
        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet(nameof(GetByUser))]
        public async Task<MessageResultModel> GetByUser(string userName)
        {
            var result = await _service.GetAccount(userName);
            return result;
        }

        [HttpPost(nameof(CreateAccount))]
        public async Task<MessageResultModel> CreateAccount([FromBody]AccountModel account)
        {
            var result = await _service.CreateAccount(account);
            return result;
        }

        [HttpPut(nameof(UpdateAccount))]
        public async Task<MessageResultModel> UpdateAccount([FromBody]AccountModel account)
        {
            var result = await _service.UpdateAccount(account);
            return result;
        }
    }
}
