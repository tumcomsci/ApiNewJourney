using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ApiNewJourney.DataLayer.IDL
{
    public interface IDapperDL : IDisposable
    {
        DbConnection GetDbConnection();
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);
        List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);
        int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);
        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);
    }
}
