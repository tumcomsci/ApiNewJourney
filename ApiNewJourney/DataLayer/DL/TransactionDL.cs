using ApiNewJourney.DataLayer.IDL;
using ApiNewJourney.Models;
using Dapper;
using System.Data;

namespace ApiNewJourney.DataLayer.DL
{
    public class TransactionDL : ITransactionDL
    {
        private readonly IDapperDL _iDapperDL;
        public  TransactionDL(IDapperDL iDapperDL)
        {
            _iDapperDL = iDapperDL;
        }

        public async Task<int> CreateTransaction(TransactionModel model)
        {
            try
            {
                var query = @"INSERT INTO [dbo].[Transaction]
                                   ([TransactionId]
                                   ,[OriginAccount]
                                   ,[DestinationAccount]
                                   ,[CurrentBalance]
                                   ,[Amount]
                                   ,[PackageId]
                                   ,[PackageCaculated]
		                           ,[CreatedDate]
		                           ,[ModifiedDate])
                             VALUES
                                   (@TransactionId
                                   ,@OriginAccount
                                   ,@DestinationAccount
                                   ,@CurrentBalance
                                   ,@Amount
                                   ,@PackageId
                                   ,@PackageCaculated
		                           ,@CreatedDate
		                           ,@ModifiedDate)";
                var parameters = new DynamicParameters();
                parameters.Add("TransactionId", Guid.NewGuid(), DbType.Guid);
                parameters.Add("OriginAccount", model.OriginAccount, DbType.Guid);
                parameters.Add("DestinationAccount", model.DestinationAccount, DbType.Guid);
                parameters.Add("CurrentBalance", model.CurrentBalance, DbType.Decimal);
                parameters.Add("Amount", model.Amount, DbType.Decimal);
                parameters.Add("PackageId", model.PackageId, DbType.Guid);
                parameters.Add("PackageCaculated", model.PackageCalculated, DbType.Decimal);
                parameters.Add("CreatedDate",DateTime.Now,DbType.DateTime);
                parameters.Add("ModifiedDate",DateTime.Now,DbType.DateTime);

                var results = await Task.FromResult<int>(_iDapperDL.Insert<int>(query, parameters, commandType: CommandType.Text));

                return results;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdateTransaction(TransactionModel model)
        {
            try
            {
                var query = @"UPDATE [dbo].[Transaction]
                               SET
	                               [OriginAccount] = @OriginAccount
                                  ,[DestinationAccount] = @DestinationAccount
                                  ,[CurrentBalance] = @Balance
                                  ,[Amount] = @Amount
                                  ,[PackageId] = @PackageId
                                  ,[PackageCaculated] = @PackageCaculated
                                  ,[CreatedDate] = @CreatedDate
                                  ,[ModifiedDate] = @ModifiedDate
                             WHERE TransactionId = @TransactionId";

                var parameters = new DynamicParameters();
                parameters.Add("TransactionId", Guid.NewGuid(), DbType.Guid);
                parameters.Add("OriginAccount", model.OriginAccount, DbType.Guid);
                parameters.Add("DestinationAccount", model.DestinationAccount, DbType.Guid);
                parameters.Add("Balance", model.CurrentBalance, DbType.Decimal);
                parameters.Add("Amount", model.Amount, DbType.Decimal);
                parameters.Add("PackageId", model.PackageId, DbType.Decimal);
                parameters.Add("PackageCaculated", model.PackageCalculated, DbType.Decimal);
                parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
                parameters.Add("ModifiedDate", DateTime.Now, DbType.DateTime);

                var results = await Task.FromResult<int>(_iDapperDL.Execute(query, parameters, commandType: CommandType.Text));
                return results;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<TransactionModel>> GetAllTransactionByAccountId(string accountId)
        {
            try
            {
                var query = @"SELECT [TransactionId]
                                  ,[OriginAccount]
                                  ,[DestinationAccount]
                                  ,[CurrentBalance]
                                  ,[Amount]
                                  ,[PackageId]
                                  ,[PackageCaculated]
                                  ,[CreatedDate]
                                  ,[ModifiedDate]
                              FROM [dbo].[Transaction]
                              WHERE OriginAccount = @OriginAccount";
                var parameters = new DynamicParameters();
                parameters.Add("OriginAccount", accountId, DbType.Guid);

                var result = await Task.FromResult(_iDapperDL.GetAll<TransactionModel>(query, parameters, commandType: CommandType.Text));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
