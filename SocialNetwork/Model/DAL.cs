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

            }

        }
    }
}

