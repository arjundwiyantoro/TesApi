using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace tesapi4.Controllers
{

    public class result
    {
        public string userid { get; set; }
        public string namalengkap { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string status { get; set; }

        public string error { get; set; }

        public result(string userid, string namalengkap, string username, string password,string status, string error)
        {
            this.userid = userid;
            this.namalengkap = namalengkap;
            this.username = username;
            this.password = password;
            this.error = error;
        }



    }


    public class result3
    {
        public string userid { get; set; }
        public string namalengkap { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public string status { get; set; }

        public string error { get; set; }




    }





    public class result2
    {
        public string status { get; set; }
        public string error { get; set; }


        public result2(string status, string error)
        {
            this.status = status;
            this.error = error;

        }

    }

    public class UsersController : ApiController
    {
        public List<result> Get()
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand com = conn.CreateCommand();

            com.CommandText = "select * from tbl_user";

            var result = new List<result>();

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                result.Add(new result(null, null, null, null,null, ex.Message));
                return result;
            }

            MySqlDataReader mda = com.ExecuteReader();

            while (mda.Read())
            {
                result.Add(new result(mda["userid"].ToString(), mda["namalengkap"].ToString(), mda["username"].ToString(), mda["password"].ToString(), mda["status"].ToString(), null));
            }

            return result;
        }

        public List<result> Get(int id)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand com = conn.CreateCommand();

            com.CommandText = "select * from tbl_user where userid=" + id;

            var result = new List<result>();

            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                result.Add(new result(null, null, null, null,null, ex.Message));
                return result;
            }

            MySqlDataReader mda = com.ExecuteReader();

            while (mda.Read())
            {
                result.Add(new result(mda["userid"].ToString(), mda["namalengkap"].ToString(), mda["username"].ToString(), mda["password"].ToString(), mda["status"].ToString(), null));
            }

            return result;



        }

        public List<result2> Post(result3 res)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand com = conn.CreateCommand();

            var result2 = new List<result2>();

            com.CommandText = "insert into tbl_user(namalengkap,username,password,status) Values(@nama,@username,@password,@status)";
            com.Parameters.AddWithValue("@nama", res.namalengkap.ToString());
            com.Parameters.AddWithValue("@username", res.username.ToString());
            com.Parameters.AddWithValue("@password", res.password.ToString());
            com.Parameters.AddWithValue("@status", res.status.ToString());

            try
            {
                conn.Open();
                com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                result2.Add(new result2("Gagal", ex.Message));
                return result2;
            }
            conn.Close();
            result2.Add(new result2("Berhasil", null));
            return result2;
        }

        public List<result2> Delete(int id)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand com = conn.CreateCommand();

            var result2 = new List<result2>();

            com.CommandText = "delete from tbl_user where userid=" + id;

            try
            {
                conn.Open();
                com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                result2.Add(new result2("Gagal", ex.Message));
                return result2;
            }
            result2.Add(new result2("Berhasil", null));
            return result2;
        }

      


    }
}
