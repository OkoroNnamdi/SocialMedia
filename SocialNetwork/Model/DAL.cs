using System.Data;
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

            return response;
        }
    }
}

