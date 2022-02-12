using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationSec_200238F
{
    public partial class DisplayProfile : System.Web.UI.Page
    {

        string MYDBConnectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        string userID = null;
        byte[] Key;
        byte[] IV;
        byte[] creditcard = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)  
            {
                if(!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    userID = (string)Session["userID"];
                    displayDetails(userID);
                    btnLogOut.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }

        }

        protected void displayDetails(string userid)
        {
            SqlConnection connection = new SqlConnection(MYDBConnectionstring);
            string sql = "select * FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["firstname"] != DBNull.Value)
                        {
                            ufirstname.Text = reader["firstname"].ToString();
                        }
                        if (reader["lastname"] != DBNull.Value)
                        {
                            ulastname.Text = reader["lastname"].ToString();
                        }
                        if (reader["creditcard"] != DBNull.Value)
                        {
                            creditcard = Convert.FromBase64String(reader["creditcard"].ToString());
                        }
                        if (reader["email"] != DBNull.Value)
                        {
                            uemail.Text = reader["email"].ToString();
                        }
                        if (reader["DOB"] != DBNull.Value)
                        {
                            uDOB.Text = reader["DOB"].ToString();
                        }
                        if (reader["profilepicture"] != DBNull.Value)
                        {
                            uprofilepicture.Attributes["src"] = reader["profilepicture"].ToString();
                        }
                        if (reader["IV"] != DBNull.Value)
                        {
                            IV = Convert.FromBase64String(reader["IV"].ToString());
                        }
                        if (reader["Key"] != DBNull.Value)
                        {
                            Key = Convert.FromBase64String(reader["Key"].ToString());
                        }
                    }
                    ucreditcard.Text = decryptData(creditcard);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
        }

        protected string decryptData(byte[] cipherText)
        {
            string plainText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform decryptTransform = cipher.CreateDecryptor();

                using (MemoryStream memDcrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream cryDecrypt = new CryptoStream(memDcrypt, decryptTransform, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamDecrypt = new StreamReader(cryDecrypt))
                        {
                            plainText = streamDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
                return plainText;
        }

        protected void Logout(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("Login.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
                if (Request.Cookies["AuthToken"] != null)
                {
                    Response.Cookies["AuthToken"].Value = string.Empty;
                    Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
                }

            }
        }
    }
}