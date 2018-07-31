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
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [Route("Products/{ID}")]
        [HttpGet]
        public IHttpActionResult Products(int ID)
        {
            var data = _objContext.GetProductByID(ID);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [Route("Product/{ID}")]
        [HttpDelete]       
        public IHttpActionResult Product(int ID)
        {          
            _objContext.DeleteProduct(ID);
            return Ok(ID);
        }
        [Route("SaveProduct")]
        [HttpPost]       
        public IHttpActionResult SaveProduct(Product _objProduct)
        {           
            _objProduct = _objContext.SaveProduct(_objProduct);
            return Ok(_objProduct);
        }
    }
}
