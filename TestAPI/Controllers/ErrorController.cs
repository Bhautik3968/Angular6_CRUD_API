using System.Web.Http;
using TestAPI.DataContext;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [RoutePrefix("api/Error")]
    [Authorize]
    public class ErrorController :ApiController
    {
        [HttpPost]
        [Route("")]
        public IHttpActionResult Add(Error _objError)
        {
            ErrorDataContext _objContext = new ErrorDataContext();
            if (_objError == null)
            {
                return BadRequest();
            }
            _objError.ID=_objContext.SaveError(_objError);            
            return Ok(_objError);
        }
    }
}