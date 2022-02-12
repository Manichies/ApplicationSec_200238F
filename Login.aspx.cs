using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace ApplicationSec_200238F
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                
        }
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        protected void Button_Click(object send, EventArgs e)
        {
            var pin = pinNumber.Text;
            var secretCode = "scrtCode";
            var api = new GoogleAPI();
            var ok = api.ValidatePin(pin, secretCode);

            System.Diagnostics.Debug.WriteLine("Yayy");
            string email = userEmail.Text.ToString().Trim();
            string passW = userPassword.Text.ToString().Trim();

            var failcount = FailCount(email);
            if (failcount == 10)
            {
                var failDate = getDateTime(email);
                var recoverAccount = failDate.AddSeconds(25);
                var currentTime = DateTime.Now;
                if (recoverAccount > currentTime)
                {
                    error.Text = "Account Still locked";
                    error.ForeColor = Color.Red;
                    return;
                }
                else
                {
                    UpdateFailDateCount(email, 0, failDate);
                }
            }
            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(email);
            string dbSalt = getDBSalt(email);
            try
            {
                if (dbSalt !=null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Salting");
                    string pwdWithSalt = passW + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    if (userHash ==(dbHash) && ok == true)
                    {

                        var failCounter = FailCount(email);
                        failCounter = 0;
                        UpdateFailDateCount(email, failCounter, DateTime.Now);

                        Session["UserID"] = email;

                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                        Response.Redirect("DisplayProfile.aspx", false);
                    }
                    else
                    {
                        var failCounter = FailCount(email);
                        failCounter += 1;
                        if(failCounter < 3)
                        {
                            error.Text = "Invalid login details";
                            error.ForeColor = Color.Red;
                            UpdateFailDateCount(email, failCounter, DateTime.Now);
                        }
                        else
                        {
                            error.Text = "Error Account has been locked out";
                            error.ForeColor = Color.Red;
                            DateTime failDate = DateTime.Now;
                            failCounter = 10;
                            UpdateFailDateCount(email, failCounter, failDate);
                            

                        }
                        System.Diagnostics.Debug.WriteLine("Error");
                        error.Text = "Invalid login details";
                        error.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                //
            }
        }
        protected void UpdateFailDateCount(string email, int fcount, DateTime dt)
        {
            System.Diagnostics.Debug.WriteLine("dddd");
            try
            {
                using (SqlConnection scon = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand scmd = new SqlCommand("Update Account set FCount = @FCount, DateTime = @Datetime WHERE email = @email"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            scmd.CommandType = CommandType.Text;
                            scmd.Parameters.AddWithValue("@email", email);
                            scmd.Parameters.AddWithValue("@FCount", fcount);
                            scmd.Parameters.AddWithValue("@DateTime", dt);
                            scmd.Connection = scon;
                            scon.Open();
                            scmd.ExecuteNonQuery();
                            scon.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        protected int FailCount(string userid)
        {
            int d = 0;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select FCount FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["FCount"] != null)
                        {
                            if (reader["FCount"] != DBNull.Value)
                            {
                                d = (int)reader["FCount"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return d;
        }

        protected DateTime getDateTime(string userid)
        {
            DateTime d = DateTime.Now;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select DateTime FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["DateTime"] != null)
                        {
                            if (reader["DateTime"] != DBNull.Value)
                            {
                                d =(DateTime)reader["DateTime"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return d;
        }

        protected string getDBHash(string userid)
        {
            string d = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if(reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                d = reader["PasswordHash"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return d;
        }

        protected string getDBSalt(string userid)
        {
            string st = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PasswordSalt FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PasswordSalt"] != null)
                        {
                            if (reader["PasswordSalt"] != DBNull.Value)
                            {   
                                st = reader["PasswordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return st;
        }
        //protected void LoginMe(object sender, EventArgs e)
        //{
        //    if (tb_userid.Text.Trim().Equals("u") && tb_pwd.Text.Trim().Equals("p"))
        //    {
        //        Session["LoggedIn"] = tb_userid.Text.Trim();

        //        string guid = Guid.NewGuid().ToString();
        //        Session["AuthToken"] = guid;

        //        Response.Cookies.Add(new HttpCookie("AuthToken", guid));

        //        Response.Redirect("HomePage.aspx", false);
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Wrong username or password";
        //    }
        //}
    }
}