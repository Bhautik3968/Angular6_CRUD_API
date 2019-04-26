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
    [RoutePrefix("api/Product")]    
    [Authorize]
    public class ProductController : ApiController
    {
        ProductDataContext _objContext = new ProductDataContext();       
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var data = _objContext.GetProducts();           
            return Ok(data);
        }  
        [Route("{id:int}",Name = "GetProductByID")]    
        [HttpGet()]
        public IHttpActionResult GetById(int id)
        {
            var data = _objContext.GetProductByID(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [Route("{searchText}", Name = "SearchProduct")]
        [HttpGet()]
        public IHttpActionResult SearchProducts(string searchText)
        {
            var data = _objContext.SearchProducts(searchText);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [Route("{id}")]
        [HttpDelete]       
        public IHttpActionResult Delete(int id)
        {
            var data = _objContext.GetProductByID(id);
            if (data == null)
            {
                return NotFound();
            }
            _objContext.DeleteProduct(id);
            //return Content(HttpStatusCode.NoContent, "");
            return Ok(id);            
        }
        
        [HttpPost]  
        [Route("")]     
        public IHttpActionResult Add(Product _objProduct)
        {
            if (_objProduct == null)
            {
                return BadRequest();
            }
            _objProduct = _objContext.SaveProduct(_objProduct);           
            return CreatedAtRoute("GetProductByID", new { id = _objProduct.ID }, _objProduct);
        }
        [Route("")]
        [HttpPut]
        public IHttpActionResult Update(Product _objProduct)
        {
            if (_objProduct == null || _objProduct.ID==0)
            {
                return BadRequest();
            }
            var data = _objContext.GetProductByID(_objProduct.ID);
            if (data == null)
            {
                return NotFound();
            }
            _objContext.UpdateProduct(_objProduct);
            //return Content(HttpStatusCode.NoContent, "");
            return Ok(_objProduct);
        }
    }
}
