using ApiNewJourney.DataLayer.IDL;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace ApiNewJourney.DataLayer.DL
{
    public class DapperDL : IDapperDL
    {
        private readonly IConfiguration _config;
        private string ConnectionString = "DefaultConnection";

        public DapperDL(IConfiguration config)
        {
            this._config = config;
        }

        public void Dispose()
        {

        }

        public DbConnection GetDbConnection()
        {
            return new SqlConnection(_config.GetConnectionString(ConnectionString));
        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            int result = -1;
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Execute(sp, parms, commandType: commandType, transaction: tran);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));
            return db.Query<T>(sp,parms,commandType: commandType).FirstOrDefault();
        }

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));
            return db.Query<T>(sp,parms,commandType: commandType).ToList();
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;
            using IDbConnection db = new SqlConnection(_config.GetConnectionString(ConnectionString));
           
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(db.State == ConnectionState.Open)
                    db.Close();
            }
            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            throw new NotImplementedException();
        }
    }
}
