using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplicationSec_200238F
{
    public partial class Registration : System.Web.UI.Page
    {
        string MYDBConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string HashResult;
        static string salt;
        byte[] Key;
        byte[] IV;

        public class Object
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var appName = "ApplicationSec_200238F";
            var secretCode = "scrtCode";
            var api = new GoogleAPI();
            var setup = api.Pair(appName, secretCode);
            Response.Write(setup.Html);
        }

        public bool Captcha()
        {
            System.Diagnostics.Debug.WriteLine("Captccha");
            bool result = true;

            string captchaResponse = Request.Form["g-recaptcha-response"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(" https://www.google.com/recaptcha/api/siteverify?secret=6LcMz1keAAAAAHnXPgIMlsER4ZxNFm6qN1KU6ou8 &response=" + captchaResponse);

            try
            {
                using (WebResponse webResponse = req.GetResponse())
                {
                    using (StreamReader streamRead = new StreamReader(webResponse.GetResponseStream()))
                    {
                        string jsonResponse = streamRead.ReadToEnd();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        Object jsonObject = js.Deserialize<Object>(jsonResponse);
                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
        protected void validateregister()
        {
            string TBfname = firstname.Text.ToString().Trim();
            string TBlname = lastname.Text.ToString().Trim();
            string TBemail = email.Text.ToString().Trim();
            string TBpassword = tb_password.Text.ToString().Trim();
            string TBdate = DOB.Text.ToString().Trim();
            string TBcreditcard = creditcard.Text.ToString().Trim();

            int zeroError = 0;

            if (Regex.IsMatch(firstname.Text, "^[a - zA - Z]$"))
            {
                lbl_fname.Visible = false;
                zeroError += 1;
            }
            else
            {
                lbl_fname.Text = "*Only alphabets allowed";
                lbl_fname.Visible = true;
            }
            if (Regex.IsMatch(lastname.Text, "^[a - zA - Z]$"))
            {
                lbl_lname.Visible = false;
                zeroError += 1;
            }
            else
            {
                lbl_lname.Text = "*Only alphabets allowed";
                lbl_lname.Visible = true;
            }

            string eRegex = @"\A(?:[a-z0-9!#$%&'+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'+/=?^_`{|}~-]+)@(?:[a-z0-9](?:[a-z0-9-][a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            if (Regex.IsMatch(email.Text, eRegex))
            {
                lbl_email.Visible = false;
                zeroError += 1;
            }
            else
            {
                lbl_email.Text = "*Invalid email";
                lbl_email.Visible = true;
            }
            if (Regex.IsMatch(tb_password.Text, "^(?=[^A-Z\n][A-Z])(?=[^a-z\n][a-z])(?=[^0-9\n][0-9])(?=[^#?!@$%^&\n-][#?!@$%^&-]).{12,}$"))
            {
                lbl_pwdchecker.Visible = false;
                zeroError += 1;
            }
            else
            {
                lbl_pwdchecker.Text = "*Password is not strong enough";
                lbl_pwdchecker.Visible = true;
            }
            if (Regex.IsMatch(creditcard.Text, @"(?<=\D|^)\d{16}(?=\D|$)"))
            {
                lbl_creditcard.Visible = false;
                zeroError += 1;
            }
            else
            {
                creditcard.Text = "*Invalid credit card number";
                creditcard.Visible = true;
            }

            if (zeroError == 6)
            {
                return;
            }


        }
        // cannot work :(
        //private bool checkExistsEmail(string email)
        //{
        //    SqlConnection con = new SqlConnection(MYDBConnection);
        //    string sql = "SELECT * FROM Account WHERE Email=@USERID";
        //    SqlCommand cmd = new SqlCommand(sql, con);
        //    cmd.Parameters.AddWithValue("@USERID", email);
        //    con.Open();
        //    var exist = cmd.ExecuteScalar();
        //    con.Close();
        //    System.Diagnostics.Debug.WriteLine(exist);
        //    if (exist != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        protected bool UniqueEmail(string userEmail)
        {
            string st = null;
            SqlConnection scon = new SqlConnection(MYDBConnection);
            string sql = "select email FROM [Account]";
            SqlCommand command = new SqlCommand(sql, scon);
            try
            {
                scon.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["email"] != null)
                        {
                            if (reader["email"] != DBNull.Value)
                            {
                                st = reader["email"].ToString();
                                if (st == userEmail)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { scon.Close(); }
            return false;
        }
        private int CheckPassword(string password)
        {
            System.Diagnostics.Debug.WriteLine("Check password");
            int score = 0;

            if (password.Length < 8)
            {
                return 1;
            }

            else
            {
                score = 1;
            }

            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }

            if (Regex.IsMatch(password, "[^A-Za-z0-9]"))
            {
                score++;
            }
            return score;
        }

        protected void btn1_click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ddd");
            if (Captcha())
            {
                System.Diagnostics.Debug.WriteLine("not bot");
                status();

            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Bot");
                status();
                //lblCaptchaMessage.Text = "Prove that you are a human";
                //lblCaptchaMessage.ForeColor = Color.Red;
            }
        }

        private void status()
        {
            System.Diagnostics.Debug.WriteLine("zzz");
            // cannot work :(
            //bool existEmail = checkExistsEmail(email.Text);
            //if (existEmail == true)
            //{
            //    lbl_email.Text = "Email Exist, Try again!";
            //    lbl_email.ForeColor = Color.Red;
            //}
            if (UniqueEmail(email.Text.ToString().Trim()) == true)
            {
                lbl_email.Text = "Email Exist, Try again!";
                lbl_email.ForeColor = Color.Red;
                return;
            }
            int scores = CheckPassword(tb_password.Text = HttpUtility.HtmlEncode(tb_password.Text));
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Excellent";
                    break;
                default:
                    break;
            }
            lbl_pwdchecker.Text = "Status : " + status;
            if (scores < 4)
            {
                lbl_pwdchecker.ForeColor = Color.Red;
            }
            else
            {
                lbl_pwdchecker.ForeColor = Color.Green;

                string imageFileName = FileUpload1.FileName;
                string imageFilePath = "/Image/" + imageFileName;
                int imageSize = FileUpload1.PostedFile.ContentLength;

                if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.FileName != " ")
                {
                    if (FileUpload1.PostedFile.ContentLength > 500000)
                    {
                        uploadChecker.Text = "File size exceeded the limit";
                        uploadChecker.ForeColor = Color.Red;
                    }
                    else
                    {
                        FileUpload1.SaveAs(Server.MapPath(imageFilePath));
                        Img1.ImageUrl = "~/" + imageFilePath;

                        string pwd = tb_password.Text.ToString().Trim();
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        byte[] saltByte = new byte[8];
                        rng.GetBytes(saltByte);

                        salt = Convert.ToBase64String(saltByte);
                        SHA512Managed hashing = new SHA512Managed();
                        string pwdSalt = pwd + salt;
                        byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                        byte[] hashSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdSalt));

                        HashResult = Convert.ToBase64String(hashSalt);
                        RijndaelManaged cipher = new RijndaelManaged();
                        cipher.GenerateKey();
                        Key = cipher.Key;
                        IV = cipher.IV;
                        createAccount(imageFilePath);
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    uploadChecker.Text = "Please upload an image";
                    uploadChecker.ForeColor = Color.Red;
                }
            }
        }

        protected void createAccount(string imageFilePath)
        {
            System.Diagnostics.Debug.WriteLine("dddd");
            try
            {
                using (SqlConnection scon = new SqlConnection(MYDBConnection))
                {
                    using (SqlCommand scmd = new SqlCommand("INSERT INTO Account VALUES(@firstname,@lastname,@creditcard,@email,@DOB,@PasswordHash,@PasswordSalt,@profilepicture,@IV,@Key,@FCount,@DateTime)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            scmd.CommandType = CommandType.Text;
                            scmd.Parameters.AddWithValue("@firstname", firstname.Text = HttpUtility.HtmlEncode(firstname.Text).Trim());
                            scmd.Parameters.AddWithValue("@lastname", lastname.Text = HttpUtility.HtmlEncode(lastname.Text).Trim());
                            scmd.Parameters.AddWithValue("@creditcard", Convert.ToBase64String(encryptData(creditcard.Text = HttpUtility.HtmlEncode(creditcard.Text).Trim())));
                            scmd.Parameters.AddWithValue("@email", email.Text.Trim());
                            scmd.Parameters.AddWithValue("@DOB", DOB.Text.Trim());
                            scmd.Parameters.AddWithValue("@PasswordHash", HashResult);
                            scmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            scmd.Parameters.AddWithValue("@profilepicture", imageFilePath);
                            scmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            scmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            scmd.Parameters.AddWithValue("@FCount", 0);
                            scmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
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

        protected byte[] encryptData(string data)
        {
            System.Diagnostics.Debug.WriteLine("Encryption");
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;

                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                //
            }
            return cipherText;
        }
    }
}