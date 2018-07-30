using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TestAPI.DataContext;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [RoutePrefix("api")]
    [Authorize]
    public class ProductController : ApiController
    {
        ProductDataContext _objContext = new ProductDataContext();
        [Route("Products")]
        [HttpGet]
        public IHttpActionResult Products()
        {
            var data = _objContext.GetProducts();
            return Ok(data);
        }
        [Route("Products/{ID}")]
        [HttpGet]
        public IHttpActionResult Products(int ID)
        {
            var data = _objContext.GetProductByID(ID);
            return Ok(data);
        }

        [Route("Product/{ID}")]
        [HttpDelete]
        [ResponseType(typeof(Response))]        
        public IHttpActionResult Product(int ID)
        {
            Response response = new Response();
            try
            {
                response = _objContext.DeleteProduct(ID);
            }
            catch (Exception ex)
            {
                response = new Response { Success = false, StatusCode = 100, ResponseString = ex.Message };
            }
            return Ok(response);
        }
        [Route("SaveProduct")]
        [HttpPost]
        [ResponseType(typeof(Product))]
        public IHttpActionResult SaveProduct(Product _objProduct)
        {           
            try
            {
                _objProduct = _objContext.SaveProduct(_objProduct);
            }
            catch (Exception ex)
            {
                _objProduct.Success = false;
                _objProduct.ResponseString = ex.Message;                
            }
            return Ok(_objProduct);
        }
    }
}
