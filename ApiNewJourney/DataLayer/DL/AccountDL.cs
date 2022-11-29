using ApiNewJourney.DataLayer.IDL;
using ApiNewJourney.Models;
using Dapper;
using Microsoft.Identity.Client;
using System.Data;
using System.Diagnostics;

namespace ApiNewJourney.DataLayer.DL
{
    public class AccountDL : IAccountDL
    {
        private readonly IDapperDL _iDapperDL;
        public AccountDL(IDapperDL iDapperDL)
        {
            _iDapperDL = iDapperDL;
        }

        public async Task<AccountModel> GetAccount(string iBanNumber)
        {
            var query = @"SELECT * FROM Account WHERE IBANNumber='" + iBanNumber + "' and IsActive = 1";
            var result = await Task.FromResult(_iDapperDL.Get<AccountModel>(query, null, commandType: System.Data.CommandType.Text));
            return result;
        }

        public async Task<int> InsertAccount(AccountModel model)
        {
            try
            {
                var query = @"INSERT INTO [dbo].[Account]
                                ([AccountId]
                                ,[UserName]
                                ,[FirstName]
                                ,[LastName]
                                ,[IBANNumber]
                                ,[CreatedDate]
                                ,[IsActive])
                            VALUES
                                (@AccountId
                                ,@UserName
                                ,@FirstName
                                ,@LastName
                                ,@IBANNumber
                                ,@CreatedDate
                                ,@IsActive)";
                var parameters = new DynamicParameters();
                parameters.Add("AccountId", Guid.NewGuid(), DbType.Guid);
                parameters.Add("UserName", model.UserName, DbType.String);
                parameters.Add("FirstName", model.FirstName, DbType.String);
                parameters.Add("LastName", model.LastName, DbType.String);
                parameters.Add("IBANNumber", model.IBANNumber, DbType.String);
                parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
                parameters.Add("IsActive", true, DbType.Boolean);

                var result = await Task.FromResult(_iDapperDL.Insert<int>(query, parameters, commandType: CommandType.Text));

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public async Task<int> UpdateAccount(AccountModel model)
        {
            try
            {
                var query = @"UPDATE [dbo].[Account]
                            SET [UserName] = @UserName
                                ,[FirstName] = @FirstName
                                ,[LastName] = @LastName
                                ,[IBANNumber] = @IBANNumber
                                ,[CreatedDate] = @CreatedDate
                                ,[IsActive] = @IsActive
                            WHERE AccountId = @AccountId";

                var parameters = new DynamicParameters();
                parameters.Add("AccountId",model.AccountId, DbType.Guid);
                parameters.Add("UserName", model.UserName, DbType.String);
                parameters.Add("FirstName", model.FirstName, DbType.String);
                parameters.Add("LastName", model.LastName, DbType.String);
                parameters.Add("IBANNumber", model.IBANNumber, DbType.String);
                parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime);
                parameters.Add("IsActive", true, DbType.Boolean);

                var result = await Task.FromResult(_iDapperDL.Execute(query, parameters, commandType: CommandType.Text));

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
