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
        [HttpPost]
        [Route("Login")]
        public Response Login (Registration registration)
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.Login(registration, conn);

            return response;

        }
        [HttpPost ]
        [Route ("UserApproval")]
        public Response UserApproval (Registration registration)
        {
            var response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.UserApproval (registration, connection);
            return response;
        }
        [HttpPost]
        [Route("StaffRegistration")]
        public Response StaffRegistration(Staff staff)
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.StaffRegistration(staff, conn);

            return response;
        }
        [HttpPost]
        [Route("DeleteStaff")]
        public Response DeleteStaff(Staff staff)
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.DeleteStaff(staff, conn);

            return response;
        }
    }
}
