using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Model;
using System.Data.SqlClient;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        
        private readonly IConfiguration _configuration;
        public NewsController(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        [HttpPost ]
        [Route("AddNews")]
        public Response AddNews(News news)
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.AddNews(news, conn);

            return response;
        }
        [HttpGet ]
        [Route("NewsList")]
        public Response NewsList()
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.NewsList( conn);

            return response;
        }
    }
}
