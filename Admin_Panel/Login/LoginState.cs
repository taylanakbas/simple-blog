using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Admin_Panel.Login
{
    public class LoginState
    {

        public bool IsLoginSuccess(string username, string password)
        {

            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string loginQuery = "Select AuthorId, Username, Psw From Author";

            SqlCommand loginCommand = new SqlCommand(loginQuery, connection);
            SqlDataReader reader = loginCommand.ExecuteReader();


            while (reader.Read())
            {
                //Success
                if (reader["Username"].ToString().Equals(username) && reader["Psw"].ToString().Equals(password))
                {
                    var userId = (reader["AuthorId"]);
                    if (userId != null)
                    {
                        HttpContext.Current.Session["Login"] = userId.ToString();
                        reader.Close();
                        connection.Close();
                        return true;
                    }



                }
            }
            return false;

        }


    }


}