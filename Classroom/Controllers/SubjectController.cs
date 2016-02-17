using System.Web.Http;

namespace Classroom.Controllers
{
    public class SubjectController : ApiController
    {
        public IHttpActionResult Index()
        {
            return Ok();
        }
    }
}
