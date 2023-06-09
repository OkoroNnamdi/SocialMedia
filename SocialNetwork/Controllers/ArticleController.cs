﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Model;
using System.Data.SqlClient;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ArticleController(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        [HttpPost ]
       [Route("AddArticle")]
        public Response AddArticle(Article article)
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.AddArticle(article, conn);

            return response;
        }
        [HttpPost ]
        [Route ("ListArticle")]
        public Response ListArticle(Article article)
        {
            var response = new Response();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.ArticleList(article, conn);
            return response;
        }
        [HttpPost]
        [Route("ArticleApproval")]
        public Response ArticleApproval(Article article)
        {
            var response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Connstring").ToString());
            DAL dal = new DAL();
            dal.ArticleApproval(article, connection);
            return response;
        }
    }
}
