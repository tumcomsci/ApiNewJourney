using ApiNewJourney.BusinessLayer.IService;
using ApiNewJourney.DataLayer.IDL;
using ApiNewJourney.Models;

namespace ApiNewJourney.BusinessLayer.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionDL _iTransactionDL;
        private readonly IAccountBalanceDL _iAccountBalanceDL;
        private readonly IPackageDL _iPackageDL;

        public TransactionService(ITransactionDL iTransactionDL, IAccountBalanceDL iAccountBalanceDL, IPackageDL iPackageDL)
        {
            this._iTransactionDL = iTransactionDL;
            this._iAccountBalanceDL = iAccountBalanceDL;
            this._iPackageDL = iPackageDL;
        }

        public async Task<MessageResultModel> CreateTransaction(string accountIdOrigin, string accountIdDest, string packageId, decimal amount)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                bool flagComplete = await Task.Run(async () => {
                    AccountBalanceModel accountBalanceOrigin = await _iAccountBalanceDL.GetAccountBalance(accountIdOrigin);
                    AccountBalanceModel accountBalanceDest = await _iAccountBalanceDL.GetAccountBalance(accountIdDest);
                    PackageModel package = await _iPackageDL.GetPackage(packageId);

                    try
                    {
                        AccountBalanceModel adjustAccount = accountBalanceOrigin;
                        //1. Calculate Package Amount 
                        decimal? amountAdjust = CalculatePackage(amount, package, accountBalanceOrigin);
                        TransactionModel newTrans = new TransactionModel
                        {
                            OriginAccount = accountBalanceOrigin.AccountBalanceId,
                            DestinationAccount = accountBalanceDest.AccountBalanceId,
                            CurrentBalance = accountBalanceOrigin.Balance,
                            Amount = amountAdjust,
                            PackageId = package.PackageId
                        };
                        //Create Transaction
                        var result = await _iTransactionDL.CreateTransaction(newTrans);
                        //Adjust Original Balance
                        adjustAccount.Balance = newTrans.CurrentBalance + newTrans.Amount;
                        await _iAccountBalanceDL.UpdateAccountBalance(adjustAccount);

                        
                        if (amountAdjust < 0)
                        {
                            adjustAccount = accountBalanceDest;
                            await CreatePackageTransaction(accountBalanceDest,accountBalanceOrigin, package, amountAdjust);
                            adjustAccount.Balance = adjustAccount.Balance + amountAdjust;
                            await _iAccountBalanceDL.UpdateAccountBalance(adjustAccount);
                        }

                        //Account Change
                        adjustAccount.Balance = adjustAccount.Balance + amount + amountAdjust;
                        //Adjust Balance
                        await _iAccountBalanceDL.UpdateAccountBalance(adjustAccount);

                        msg_res.Success = true;
                        msg_res.Message = "Success";
                        msg_res.Data = result;

                    }
                    catch (Exception ex)
                    {
                        msg_res.Success = false;
                        msg_res.Message = ex.Message;
                        msg_res.Data = ex.Data;
                    }

                    return true;
                });



            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
                msg_res.Data = ex.Data;
            }

            return msg_res;
        }

        public async Task<MessageResultModel> CreatePackageTransaction(AccountBalanceModel accountBalanceIdOrigin, AccountBalanceModel accountBalanceIdDest, PackageModel package, decimal? amount)
        {
            MessageResultModel msg_res = new MessageResultModel();
            try
            {
                TransactionModel packageTrans = new TransactionModel
                {
                    OriginAccount = accountBalanceIdOrigin.AccountBalanceId,
                    DestinationAccount = accountBalanceIdDest != null ? accountBalanceIdDest.AccountBalanceId : null,
                    CurrentBalance = accountBalanceIdOrigin != null ? accountBalanceIdOrigin.Balance : null,
                    PackageId = package != null ? package.PackageId : null,
                    Amount = amount
                };

                var result = await _iTransactionDL.CreateTransaction(packageTrans);

                msg_res.Success = true;
                msg_res.Message = "Success";
                msg_res.Data = result;
            }
            catch (Exception ex)
            {
                msg_res.Success = false;
                msg_res.Message = ex.Message;
                msg_res.Data = ex.Data;
            }
            return msg_res;
        }

        public Task<MessageResultModel> UpdateTransaction(TransactionModel origin)
        {
            throw new NotImplementedException();
        }

        private decimal? CalculatePackage(decimal? amount, PackageModel package, AccountBalanceModel model)
        {
            decimal? result = 0;
            if (package != null)
            {
                result = ((amount * package.Ratio) / package.Amount);
            }

            return result;
        }
    }
}
