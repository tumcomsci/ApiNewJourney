using ApiNewJourney.DataLayer.IDL;
using ApiNewJourney.Models;
using Dapper;
using System.Data;

namespace ApiNewJourney.DataLayer.DL
{
    public class AccountBalanceDL : IAccountBalanceDL
    {
        private readonly IDapperDL _iDappDL;

        public AccountBalanceDL(IDapperDL iDappDL)
        {
            _iDappDL = iDappDL;
        }

        public async Task<AccountBalanceModel> GetAccountBalance(string accountId)
        {
            try
            {
                var query = @"SELECT [AccountBalanceId]
                                ,[AccountId]
                                ,[Balance]
                                ,[IsActive]
                                ,[CreatedDate]
                            FROM [dbo].[AccountBalance]
                            WHERE AccountId = @AccountId AND IsActive = 1";
                var parameters = new DynamicParameters();
                parameters.Add("AccountId",Guid.Parse(accountId), System.Data.DbType.Guid);
                
                var result = await Task.FromResult(_iDappDL.Get<AccountBalanceModel>(query, parameters));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> InsertAccountBalance(AccountBalanceModel model)
        {
            try
            {
                var query = @"INSERT INTO [dbo].[AccountBalance]
                                ([AccountBalanceId]
                                ,[AccountId]
                                ,[Balance]
                                ,[IsActive]
                                ,[CreatedDate])
                            VALUES
                                (@AccountBalanceId
                                ,@AccountId
                                ,@Balance
                                ,@IsActive
                                ,@CreatedDate)";
                var parameters = new DynamicParameters();
                parameters.Add("AccountBalanceId", Guid.NewGuid(),DbType.Guid);
                parameters.Add("AccountId", model.AccountId,DbType.Guid);
                parameters.Add("Balance", model.Balance, DbType.Decimal);
                parameters.Add("IsActive", true, DbType.Boolean);
                parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);

                var result = await Task.FromResult(_iDappDL.Insert<int>(query, parameters, commandType: CommandType.Text));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateAccountBalance(AccountBalanceModel model)
        {
            try
            {
                var query = @"UPDATE [dbo].[AccountBalance]
                            SET
                                [Balance] = @Balance
                                ,[IsActive] = @IsActive
                                ,[CreatedDate] = @CreatedDate
                            WHERE AccountBalanceId = @AccountBalanceId";

                var parameters = new DynamicParameters();
                parameters.Add("AccountBalanceId",model.AccountBalanceId,DbType.Guid);
                parameters.Add("AccountId",model.AccountId,DbType.Guid);
                parameters.Add("Balance", model.Balance, DbType.Decimal);
                parameters.Add("IsActive", model.IsActive, DbType.Boolean);
                parameters.Add("CreatedDate",model.CreateDate, DbType.DateTime);

                var results = await Task.FromResult(_iDappDL.Execute(query, parameters, commandType: CommandType.Text));

                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
