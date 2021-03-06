﻿using System.Linq;
using Dapper;
using TestAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace TestAPI.DataContext
{
    public class UserDataContext : DatabaseConnection
    {
        public User GetUser(User _objUser)
        {
            var oPara = new DynamicParameters();
            oPara.Add("@Username", _objUser.Username, DbType.String);
            oPara.Add("@Password", _objUser.Password, DbType.String);
            using (IDbConnection connection = ClientDBConnection)
            {
                return connection.Query<User>("GetUsers", oPara, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}