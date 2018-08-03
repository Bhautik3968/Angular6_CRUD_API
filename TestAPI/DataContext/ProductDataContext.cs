using System.Linq;
using Dapper;
using TestAPI.Models;
using System.Data;
using System.Collections.Generic;
using System;
namespace TestAPI.DataContext
{
    public class ProductDataContext : DatabaseConnection
    {

        public List<Product> GetProducts()
        {
            using (IDbConnection connection = ClientDBConnection)
            {
                return connection.Query<Product>("GetProducts", commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public Product GetProductByID(int ID)
        {
            using (IDbConnection connection = ClientDBConnection)
            {
                var oPara = new DynamicParameters();
                oPara.Add("@ID", ID, DbType.Int32);
                return connection.Query<Product>("GetProductByID", oPara, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }


        public Product SaveProduct(Product _objProduct)
        {
            var oPara = new DynamicParameters();
            oPara.Add("@ID", _objProduct.ID, DbType.Int32, ParameterDirection.Output);
            oPara.Add("@Name", _objProduct.Name.Trim(), DbType.String);
            oPara.Add("@Price", _objProduct.Price, DbType.String);
            oPara.Add("@Quantity", _objProduct.Quantity, DbType.Int32);
            oPara.Add("@Image", _objProduct.Image, DbType.String);
            using (IDbConnection connection = ClientDBConnection)
            {
                connection.Execute("SaveProduct", oPara, commandType: CommandType.StoredProcedure);
            }
            _objProduct.ID = oPara.Get<int>("@ID");
            return _objProduct;
        }

        public void UpdateProduct(Product _objProduct)
        {
            var oPara = new DynamicParameters();
            oPara.Add("@ID", _objProduct.ID, DbType.Int32);
            oPara.Add("@Name", _objProduct.Name.Trim(), DbType.String);
            oPara.Add("@Price", _objProduct.Price, DbType.String);
            oPara.Add("@Quantity", _objProduct.Quantity, DbType.Int32);
            oPara.Add("@Image", _objProduct.Image, DbType.String);
            using (IDbConnection connection = ClientDBConnection)
            {
                connection.Execute("UpdateProduct", oPara, commandType: CommandType.StoredProcedure);
            }         
           
        }

        public void DeleteProduct(int ID)
        {
            var oPara = new DynamicParameters();
            oPara.Add("@ID", ID, DbType.Int32);
            using (IDbConnection connection = ClientDBConnection)
            {
                connection.Execute("DeleteProduct", oPara, commandType: CommandType.StoredProcedure);
            }           
        }
    }
}