using System.Linq;
using Dapper;
using TestAPI.Models;
using System.Data;
using System.Collections.Generic;
using System;
namespace TestAPI.DataContext
{
    public class ErrorDataContext : DatabaseConnection
    {
        public int SaveError(Error _objError)
        {
            var oPara = new DynamicParameters();           
            oPara.Add("@ErrorMessage",_objError.ErrorMessage, DbType.String);
            oPara.Add("@ErrorDescription",_objError.ErrorDescription, DbType.String);
            oPara.Add("@ID", _objError.ID, DbType.Int32,ParameterDirection.Output);
            using (IDbConnection connection = ClientDBConnection)
            {
                connection.Execute("SaveAPIErrors", oPara, commandType: CommandType.StoredProcedure);
            }            
            return oPara.Get<int>("@ID");
        }       
    }
}