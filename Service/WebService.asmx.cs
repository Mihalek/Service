using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Service
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        string result;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetRegisteredUsers()
        {


            try
            {

                SqlConnection con = new SqlConnection();
                SqlCommand command = new SqlCommand("Select nazwisko from tabela where imie='Jan'", con);
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionWebService"].ToString();
                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())

                {
                    result = reader[0].ToString();
                }
                reader.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                return "wyjatek: " + ex;
            }
            return "Solde: " + result;

        }

        [WebMethod]
        public void uaktualnij()
        {
            string sqlQuery1, sqlQuery2;
            sqlQuery1 = String.Concat(@"Update tabela SET imie ='zmiana' WHERE id =25");
            sqlQuery2 = String.Concat(@"Update tabela SET id = 0 where imie = 'Jan'");
            SqlTransaction tran = null;
            try
            {

                SqlConnection con = new SqlConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionWebService"].ToString();
                con.Open();
                tran = con.BeginTransaction();

                SqlCommand command = new SqlCommand(sqlQuery1, con);
                command.Transaction = tran;
                command.ExecuteNonQuery();

                command = new SqlCommand(sqlQuery2, con);
                command.Transaction = tran;
                command.ExecuteNonQuery();
                tran.Commit();



            }
            catch (Exception ex)
            {
                tran.Rollback();
            }


        }

        [WebMethod]
        public void Dodaj(string a, string b, int c)
        {

            try
            {

                SqlConnection con = new SqlConnection();
             
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionWebService"].ToString();
                con.Open();

                SqlCommand command = new SqlCommand("dodaj",con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@imie", SqlDbType.VarChar).Value = a;
                command.Parameters.AddWithValue("@nazwisko", SqlDbType.VarChar).Value = b;
                command.Parameters.AddWithValue("@id", SqlDbType.Int).Value = c;
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                
            }


        }

    }
}
