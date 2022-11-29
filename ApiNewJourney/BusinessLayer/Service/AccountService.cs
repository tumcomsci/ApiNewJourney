using ApiNewJourney.BusinessLayer.IService;
using ApiNewJourney.DataLayer.IDL;
using ApiNewJourney.Models;

namespace ApiNewJourney.BusinessLayer.Service
{
    public class AccountService : IAccountService
    {

        private readonly IAccountDL _iAccountDL;
        private readonly IAccountBalanceDL _iAccountBalanceDL;

        public AccountService(IAccountDL iAccountDL, IAccountBalanceDL iAccountBalanceDL)
        {
            _iAccountDL = iAccountDL;
            _iAccountBalanceDL = iAccountBalanceDL;
        }

        public async Task<MessageResultModel> CreateAccount(AccountModel _account)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var account = await _iAccountDL.GetAccount(_account.IBANNumber.ToString());
                if(account == null)
                {
                    var result = await _iAccountDL.InsertAccount(_account);
                    var newAccount = await _iAccountDL.GetAccount(_account.IBANNumber.ToString());
                    var accountBalance = await _iAccountBalanceDL.GetAccountBalance(newAccount.AccountId.ToString());
                    if(accountBalance == null)
                    {
                        await _iAccountBalanceDL.InsertAccountBalance(new AccountBalanceModel
                        {
                             AccountId = newAccount.AccountId,
                             Balance = 0
                        });
                    }
                    msg_res.Success = true;
                    msg_res.Message = "Success";
                    msg_res.Data = result;
                }
                else
                {
                    msg_res.Success = false;
                    msg_res.Message = "IBAN Number already existed.";
                }

                return msg_res;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
                return msg_res;
            }
        }

        public async Task<MessageResultModel> UpdateAccount(AccountModel _account)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var result = await _iAccountDL.UpdateAccount(_account);
                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
                return msg_res;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
                return msg_res; throw;
            }
        }

        public async Task<MessageResultModel> GetAccount(string _accountId)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                var result = await _iAccountDL.GetAccount(_accountId);
                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
                return msg_res;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
                return msg_res;
            }
        }
    }
}
