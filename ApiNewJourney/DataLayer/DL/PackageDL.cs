using ApiNewJourney.DataLayer.IDL;
using ApiNewJourney.Models;
using Dapper;
using System.Data;

namespace ApiNewJourney.DataLayer.DL
{
    public class PackageDL : IPackageDL
    {
        private readonly IDapperDL _iDapperDL;

        public PackageDL(IDapperDL iDapperDL)
        {
            this._iDapperDL = iDapperDL;
        }

        public async Task<int> CreatePackage(PackageModel model)
        {
            try
            {
                var query = @"INSERT INTO [dbo].[Package]
                                   ([PackageId]
                                   ,[Name]
                                   ,[Amount]
                                   ,[Ratio]
                                   ,[ValidStartDate]
                                   ,[ValidEndDate])
                             VALUES
                                   (@PackageId
                                   ,@Name
                                   ,@Amount
                                   ,@Ratio
                                   ,@ValidStartDate
                                   ,@ValidEndDate)";

                var parameters = new DynamicParameters();
                parameters.Add("PackageId", Guid.NewGuid(), DbType.Guid);
                parameters.Add("Name", model.Name, DbType.String);
                parameters.Add("Amount", model.Amount, DbType.Decimal);
                parameters.Add("Ratio", model.Ratio, DbType.Decimal);
                parameters.Add("ValidStartDate",model.ValidStartDate, DbType.DateTime);
                parameters.Add("ValidEndDate",model.ValidEndDate, DbType.DateTime);

                var result = await Task.FromResult(_iDapperDL.Insert<int>(query, parameters, commandType: CommandType.Text));
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PackageModel> GetPackage(string packageId)
        {
            try
            {
                var query = @"SELECT * FROM Package WHERE PackageId = @PackageId";
                var parameters = new DynamicParameters();
                parameters.Add("PackageId",Guid.Parse(packageId),DbType.Guid);

                var results = await Task.FromResult(_iDapperDL.Get<PackageModel>(query,parameters, commandType: CommandType.Text));
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdatePackage(PackageModel model)
        {
            try
            {
                var query = @"UPDATE [dbo].[Package]
                             SET [Name] = @Name
                                  ,[Amount] = @Amount
                                  ,[Ratio] = @Ratio
                                  ,[ValidStartDate] = @ValidStartDate
                                  ,[ValidEndDate] = @ValidEndDate
                             WHERE PackageId = @PackageId";

                var parameters = new DynamicParameters();
                parameters.Add("Name",model.Name,DbType.String);
                parameters.Add("Amount",model.Amount, DbType.Decimal);
                parameters.Add("Ratio", model.Ratio, DbType.Decimal);
                parameters.Add("ValidStartDate",model.ValidStartDate,DbType.DateTime);
                parameters.Add("ValidEndDate", model.ValidEndDate, DbType.DateTime);
                parameters.Add("PackageId", model.PackageId, DbType.Guid);

                var results = await Task.FromResult(_iDapperDL.Execute(query,parameters, commandType: CommandType.Text));

                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<PackageModel>> GetAllPackages()
        {
            try
            {
                var query = @"SELECT * FROM Package ";
                var results = await Task.FromResult(_iDapperDL.GetAll<PackageModel>(query,null,commandType:CommandType.Text).ToList());
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
