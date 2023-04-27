using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
namespace SocialNetwork.Model
{
    public class DAL
    {
        public Response Registration(Registration registration,SqlConnection connection)
        {
            var response = new Response();
            SqlCommand command = new SqlCommand("INSERT INTO REGISTRATION1(Name,Email," +
                "PhoneNo,password,IsActive,IsApproved)VALUES" +
                "('"+registration.Name+"','"+registration.Email
                +"','"+registration.PhoneNo  +"','"+registration.Password +"',1,0)");
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.Statuscode = 200;
                response.StatusMessag = "Sucessfully Registered";
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessag = "Registration failed";
            }

            return response;
        }
        public Response Login (Registration registration,SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Registration1 where Email = " +
                "'" + registration.Email + "' and Password = '" + registration.Password + "'",
                connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            if (dt.Rows.Count > 0)
            { 
                response.Statuscode = 200;
                response.StatusMessag = "Login Sucessful";
                Registration reg = new Registration();
                reg.Id = Convert.ToInt32(dt.Rows[0]["ID"]);
                reg.Name = Convert.ToString(dt.Rows[0]["Name"]);
                reg.Email = Convert.ToString(dt.Rows[0]["Email"]);
                response.Registration = reg;

            }
            else
            {
                response.Statuscode =100;
                response.StatusMessag = "Login failed";
                response.Registration = null;

            }
            return response;

        }
        public Response UserApproval (Registration registration,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("update Registration1 set IsApproved = 1 where ID = '"+registration.Id  +"' And IsActive =1",connection );
            connection.Open ();
            int i = cmd.ExecuteNonQuery();
            if (i> 0)
            {
                response.Statuscode =200;
                response.StatusMessag = "User approval Sucessful";
            }
            else
            {
                response.Statuscode =100;
                response.StatusMessag = "User Approval failed";
            }
            return response;
        }
        public Response AddNews (News news,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into News(Title, Content,Email,IsActive,CreateOn)Values('" + news.Title + "','" + news.Content + "','" + news.Email + "',1,GEtDATE()");

            connection.Open ();
            int i = cmd.ExecuteNonQuery();
            connection.Close ();
            if(i> 0)
            {
                response.Statuscode =200;
                response.StatusMessag = "News Creation sucessful";

            }
            else
            {
                response.Statuscode =100;
                response.StatusMessag = "News creation failed";
            }
            return response;
        }
        public Response NewsList(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("select * from News where IsApproved =1", connection);
            DataTable dt = new DataTable ();
            da.Fill (dt);
            List<News> list = new List<News>();
            if(dt.Rows.Count > 0)
            {
                for(int i=0;  i<dt.Rows.Count; i++)
                {
                    News news = new News();
                    news.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    news.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    news.Content = Convert.ToString(dt.Rows[i]["Content"]);
                    news.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    news.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    news.CreateOn = Convert.ToDateTime(dt.Rows[i]["createdOn"]);
                    list.Add(news);
                }
                if(list.Count > 0)
                {
                    response.Listnews = list;
                    response.Statuscode = 200;
                    response.StatusMessag = "News data found";

                }
                else
                {
                    response.Listnews = null;
                    response.Statuscode = 100;
                    response.StatusMessag = "News data not found";
                }

            }
            else
            {
                response.Listnews = null;
                response.Statuscode = 100;
                response.StatusMessag = "News data not found";
            }
            return response;

        }
        public Response AddArticle(Article article, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Article(Title, Content,Email,Image,IsActive,IsApproved)Values('" + article .Title + "','" + article .Content + "','" + article .Email + "','"+ article.Image + "',1,0",connection );

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.Statuscode = 200;
                response.StatusMessag = "Article Creation sucessful";

            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessag = "Article creation failed";
            }
            return response;
        }
        public Response ArticleList( Article article, SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = null;
            if(article.type == "User")
            {
                da = new SqlDataAdapter("SELECT * FROM  Article WHERE Email ='"+article.Email +"' AND IsActive =1",connection );
            }
            if(article.type == "Page")
            {
                da= new SqlDataAdapter("SELECT * FROM  Article ", connection);
            }
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Article > listArticles = new List<Article>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    article = new Article();
                    article.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    article.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    article.Content = Convert.ToString(dt.Rows[i]["Content"]);
                    article.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    article.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    article.Image = Convert.ToString(dt.Rows[i]["Image"]);

                    listArticles.Add(article);
                }
                if (listArticles.Count > 0)
                {
                    response.Statuscode = 200;
                    response.StatusMessag = "Article  data found";
                    response.articles = listArticles;
                }
                else
                {
                    response.articles = null;
                    response.Statuscode = 100;
                    response.StatusMessag = "Article  data not found";
                }

            }
            else
            {
                response.Listnews = null;
                response.Statuscode = 100;
                response.StatusMessag = "Article data not found";
            }
            return response;

        }
        public Response ArticleApproval(Article article, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("update Article set IsApproved = 1 where ID = '" + article.Id + "' And IsActive =1", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection .Close();
            if (i > 0)
            {
                response.Statuscode = 200;
                response.StatusMessag = "Article approval Sucessful";
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessag = "Article Approval failed";
            }
            return response;
        }
        public Response StaffRegistration(Staff staff, SqlConnection connection)

        {
            var response = new Response();
            SqlCommand command = new SqlCommand("INSERT INTO Staff(Name,Email," +
                "password,IsActive)VALUES" +
                "('" + staff.Name + "','" + staff.Email+ "','" + staff.Password + "',1)");
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.Statuscode = 200;
                response.StatusMessag = "Staff sucessfully Registered";
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessag = "Staff Registration failed";
            }

            return response;
        }
        public Response DeleteStaff(Staff staff, SqlConnection connection)

        {
            var response = new Response();
            SqlCommand command = new SqlCommand("Delete from staff where ID =1 and IsActive =1",connection );
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.Statuscode = 200;
                response.StatusMessag = "Staff sucessfully Deleted";
            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessag = "Staff Deletion failed";
            }

            return response;
        }
        public Response AddEvent(Event even , SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("Insert into Event(Title, Content,Email,IsActive,)Values('" + even.Title + "','" + even .Content + "','" + even.Email + "',1,'"+even.CreatedOn+"'", connection);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.Statuscode = 200;
                response.StatusMessag = "Event Creation sucessful";

            }
            else
            {
                response.Statuscode = 100;
                response.StatusMessag = "Event creation failed";
            }
            return response;
        }
        public Response EventList( SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM  Event Where IsActive =1", connection);  
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Event> listEvent = new List<Event>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Event @event = new Event();

                    @event.Id = Convert.ToInt32(dt.Rows[i]["ID"]);
                    @event.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    @event.Content = Convert.ToString(dt.Rows[i]["Content"]);
                    @event.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    @event.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    @event.CreatedOn = Convert.ToDateTime(dt.Rows[i]["CreatedOn"]);
                    

                    listEvent.Add(@event);
                }
                if (listEvent.Count > 0)
                {
                    response.Statuscode = 200;
                    response.StatusMessag = "Event  data found";
                    response.events = listEvent;
                }
                else
                {
                    response.events = null;
                    response.Statuscode = 100;
                    response.StatusMessag = "Event  data not found";
                }

            }
            else
            {
                response.Listnews = null;
                response.Statuscode = 100;
                response.StatusMessag = "Event data not found";
            }
            return response;

        }
    }
}

