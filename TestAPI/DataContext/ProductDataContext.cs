using System.Linq;
using Dapper;
using TestAPI.Models;
using System.Data;
using System.Collections.Generic;
using System;
namespace TestAPI.DataContext
{
    public class ProductDataContext:DatabaseConnection
    {      
        
        public List<Product> GetProducts()
        {
            List<Product> lstProduct = new List<Product>();
            try
            {
                using (IDbConnection connection = ClientDBConnection)
                {
                    lstProduct = connection.Query<Product>("GetProducts", commandType: CommandType.StoredProcedure).ToList() ?? new List<Product>();
                }                
            }
            catch (Exception) { }
            return lstProduct;    
        }

        public Product GetProductByID(int ID)
        {
            Product _objProduct = new Product();
            try
            {
                using (IDbConnection connection = ClientDBConnection)
                {
                    var oPara = new DynamicParameters();
                    oPara.Add("@ID",ID, DbType.Int32);
                    _objProduct = connection.Query<Product>("GetProductByID", oPara, commandType: CommandType.StoredProcedure).FirstOrDefault()?? new Product();
                }
            }
            catch (Exception){}
            return _objProduct;
        }



        public Product SaveProduct(Product _objProduct)
        {           
            try
            {
                var oPara = new DynamicParameters();
                oPara.Add("@ID", _objProduct.ID, DbType.Int32,ParameterDirection.InputOutput);
                oPara.Add("@Name", _objProduct.Name.Trim(), DbType.String);
                oPara.Add("@Price", _objProduct.Price, DbType.String);
                oPara.Add("@Quantity", _objProduct.Quantity, DbType.Int32);
                oPara.Add("@Image", _objProduct.Image, DbType.String);
                using (IDbConnection connection = ClientDBConnection)
                {
                    connection.Execute("SaveProduct", oPara, commandType: CommandType.StoredProcedure);
                }
                _objProduct.ID = oPara.Get<int>("@ID");
                _objProduct.Success = true;
                _objProduct.ResponseString = "";                         
            }
            catch (Exception ex)
            {
                _objProduct.Success = false;
                _objProduct.ResponseString = ex.Message;                
            }
            return _objProduct;
        }

        public Response DeleteProduct(int ID)
        {
            
            Response _objResponse = new Response();
            try
            {
                var oPara = new DynamicParameters();
                oPara.Add("@ID", ID, DbType.Int32);               
                using (IDbConnection connection = ClientDBConnection)
                {
                    connection.Execute("DeleteProduct", oPara, commandType: CommandType.StoredProcedure);
                }
                _objResponse= new Response(true, "Product deleted successfully");
            }
            catch (Exception ex)
            {
                _objResponse= new Response(false, ex.Message);
            }
            return _objResponse;
        }
    }
}