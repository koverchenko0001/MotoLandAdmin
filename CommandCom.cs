using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MotoLandAdmin {
    public class CommandCom {

        Connect con = new Connect();

        public bool LoginUser(string usermail, string userpassword) {
            //userpassword = Hash256Password(userpassword, GenSalt())
            bool isValid = false;
            try {
                con.Connection.Open();

                string sql = @"SELECT 
                                    UserID_MSTR,
                                    UserMail_MSTR,
                                    UserNickName_MSTR
                               FROM 
                                    user_mstr, password_mstr
                               WHERE 
                                    UserMail_MSTR= @usermail AND 
                                    PasswordPassword_MSTR= @userpassword AND
                                    UserID_MSTR = PasswordUserID_MSTR";

                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);

                cmd.Parameters.AddWithValue("@usermail", usermail);
                cmd.Parameters.AddWithValue("@userpassword", userpassword);

                MySqlDataReader reader = cmd.ExecuteReader();

                User user = new User();

                if (reader.Read()) {
                    /*loggedUID = reader["UserID_MSTR"].ToString();
                    loggedUser = reader["UserNickName_MSTR"].ToString();*/
                    user.id = reader["UserID_MSTR"].ToString();
                    user.mail = reader["UserMail_MSTR"].ToString();
                    user.nickname = reader["UserNickName_MSTR"].ToString();
                    isValid = true;
                }
                reader.Close();
                con.Connection.Close();

                return isValid;

            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }

        } ///public bool LoginUser

        public string GenSalt() {
            byte[] salt = new byte[16];
            //using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider()) {
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        } ///public string Gensalt

        public string Hash256Password(string password, string salt) {
            //byte[] saltBytes = Convert.FromBase64String(salt);
            using (var h256 = new HMACSHA256(Encoding.UTF8.GetBytes(salt))) {
                byte[] h = h256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(h);
            }
        } ///public string HashPassword



        public string RegisterUser(string username, string password, string fullname, string email) {
            try {
                con.Connection.Open();

                string sql = "INSERT INTO `users`(`UserName`, `Password`, `FullName`, `Email`) VALUES (@username,@password,@fullname,@email)";

                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@fullname", fullname);
                cmd.Parameters.AddWithValue("@email", email);

                cmd.ExecuteNonQuery();

                con.Connection.Close();

                return "Sikeres regisztráció";
            } catch (System.Exception ex) {
                return ex.Message;
            }

        } ///public string RegisterUser

        public DataView GetAllUser() {
            try {
                con.Connection.Open();

                string sql = "SELECT * FROM users";

                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, con.Connection);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                con.Connection.Close();

                return dt.DefaultView;
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }

        } ///public DataView GetAllUser

        public void DeleteUser(object id) {
            try {
                con.Connection.Open();

                string sql = "DELETE FROM users WHERE Id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                con.Connection.Close();
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
            }
        } ///public void DeleteUser


        public void UpdateUser(object Row) {
            try {
                con.Connection.Open();

                string sql = "UPDATE `users` SET `UserName`=@username,`Password`=@password,`FullName`=@fullname,`Email`=@email,`RegDate`=@date WHERE Id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);

                var usr = Row.GetType().GetProperties();

                cmd.Parameters.AddWithValue("@username", usr[1].GetValue(Row));
                cmd.Parameters.AddWithValue("@password", usr[2].GetValue(Row));
                cmd.Parameters.AddWithValue("@fullname", usr[3].GetValue(Row));
                cmd.Parameters.AddWithValue("@email", usr[4].GetValue(Row));
                cmd.Parameters.AddWithValue("@date", usr[5].GetValue(Row));
                cmd.Parameters.AddWithValue("@id", usr[0].GetValue(Row));

                cmd.ExecuteNonQuery();

                con.Connection.Close();
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
            }

        } ///public void UpdateUser



    } ///internal class CommandCom
}
