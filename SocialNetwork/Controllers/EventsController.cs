using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Model;
using System.Data.SqlClient;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EventsController(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        [HttpPost]
        [Route("AddEvents")]
        public Response AddEvents(Event _event)
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.AddEvent(_event, conn);

            return response;
        }
        [HttpGet]
        [Route("EventsList")]
        public Response EventsList()
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.EventList(conn);

            return response;
        }
    }
}
