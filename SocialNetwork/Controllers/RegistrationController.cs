using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SocialNetwork.Model;
using System.Data.SqlClient;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly  IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        [HttpPost ]
        [Route ("Registration")]
        public Response Registration(Registration registration)
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString ("Connstring").ToString ());
            DAL dal = new DAL();
            dal.Registration (registration , conn);
            
            return response;
        }
        
    }
}
