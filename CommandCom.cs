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

        MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;



        public bool adminExist() {
            bool ret = false;
            try {
                con.Connection.Open();
                string sql = @"SELECT 
                                    Count(UserTypeID_MSTR) AS AdminCount 
                                FROM 
                                    user_mstr
                                WHERE
                                    UserTypeID_MSTR = 4 OR UserTypeID_MSTR = 5";
                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    ret = (Convert.ToInt32(reader["AdminCount"]) > 0) ? true : false;
                }
                reader.Close();
                con.Connection.Close();
                return ret;
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
        } ///public int getUserFlag



        public int getUserFlag(string usermail) {
            int userFlag = 0;
            try {
                con.Connection.Open();
                string sql = @"SELECT 
                                    UserFlagID_MSTR
                               FROM 
                                    user_mstr
                               WHERE 
                                    UserMail_MSTR= @usermail";

                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@usermail", usermail);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) {
                    userFlag = Convert.ToInt32(reader["UserFlagID_MSTR"]);
                }   
                reader.Close();
                con.Connection.Close();
                return userFlag;
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                return userFlag;
            }
        } ///public int getUserFlag


        public bool LoginUser(string usermail, string userpassword) {

            string salt = GenSalt();
            string hashedPassword = Hash256Password(userpassword, salt);


            try {
                con.Connection.Open();

                string sql = @"SELECT 
                                    UserID_MSTR,
                                    UserMail_MSTR,
                                    UserNickName_MSTR,
                                    UserTypeID_MSTR,
                                    UserFlagID_MSTR,
                                    PasswordSalt_MSTR,
                                    PasswordPassword_MSTR
                                    
                               FROM 
                                    user_mstr, password_mstr
                               WHERE 
                                    UserMail_MSTR= @usermail AND 
                                    UserID_MSTR = PasswordUserID_MSTR";

                                    //PasswordPassword_MSTR= @userpassword AND
                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);

                cmd.Parameters.AddWithValue("@usermail", usermail);

                MySqlDataReader reader = cmd.ExecuteReader();

                string userName = "";
                string userID = "";
                int userType = 0;
                int userFlag = 0;


                if (reader.Read()) {
                    userName = reader["UserNickName_MSTR"].ToString();
                    userID = reader["UserID_MSTR"].ToString();
                    userType = Convert.ToInt32(reader["UserTypeID_MSTR"]);
                    userFlag = Convert.ToInt32(reader["UserFlagID_MSTR"]);

                    _mainWindow.setUser(userID, userName, userType, userFlag);

                    string saltFromDb = reader["PasswordSalt_MSTR"].ToString();
                    string passwordFromDb = reader["PasswordPassword_MSTR"].ToString();
                    string hashedPasswordFromDb = Hash256Password(userpassword, saltFromDb);

                    con.Connection.Close();
                    return passwordFromDb == hashedPasswordFromDb;
                }
                reader.Close();
                con.Connection.Close();
                return false;

            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }

        } ///public bool LoginUser

        public string GenSalt() {
            byte[] salt = new byte[16];
            using (var rand = RandomNumberGenerator.Create()) {
                rand.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        } ///public string Gensalt

        public string Hash256Password(string password, string salt) {
            using (var h256 = new HMACSHA256(Encoding.UTF8.GetBytes(salt))) {
                byte[] h = h256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(h);
            }
        } ///public string HashPassword


        public bool ChangePassword(string userId, string oldPassword, string newPassword) {
            try {
                string oPassword = Hash256Password(oldPassword, GenSalt());
                string nPassword = Hash256Password(newPassword, GenSalt());
                string salt = GenSalt();

                con.Connection.Open();
                string sql = @"UPDATE 
                                    password_mstr 
                                SET 
                                    PasswordPassword_MSTR = @newpassword,
                                    PasswordSalt_MSTR = @salt
                                WHERE 
                                    PasswordUserID_MSTR = @userid AND
                                    PasswordPassword_MSTR = @oldpassword";

                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@newpassword", newPassword);
                cmd.Parameters.AddWithValue("@oldpassword", oldPassword);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.Parameters.AddWithValue("@userid", userId);
                cmd.ExecuteNonQuery();
                con.Connection.Close();
                return true;
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }
        } ///public bool ChangePassword


        public bool RegisterAdmin(string nickname, string password, string email, int typeid, int flagid) {
            try {
                con.Connection.Open();
                string sql = @"INSERT INTO 
                                    user_mstr (
                                        UserNickName_MSTR, 
                                        UserMail_MSTR, 
                                        UserTypeID_MSTR, 
                                        UserFlagID_MSTR) 
                                VALUES (
                                        @nickname,
                                        @email,
                                        @typeid,
                                        @flagid)";

                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@nickname", nickname);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@typeid", typeid);
                cmd.Parameters.AddWithValue("@flagid", flagid);
                cmd.ExecuteNonQuery();
                string getLastIdSql = "SELECT LAST_INSERT_ID() AS LastID";
                MySqlCommand getLastIdCmd = new MySqlCommand(getLastIdSql, con.Connection);
                MySqlDataReader reader = getLastIdCmd.ExecuteReader();
                int lastId = 0;
                if (reader.Read()) {
                    lastId = Convert.ToInt32(reader["LastID"]);
                }
                reader.Close();

                string newPassword = @"INSERT INTO 
                                            password_mstr (
                                                PasswordUserID_MSTR, 
                                                PasswordPassword_MSTR,  
                                                PasswordSalt_MSTR) 
                                            VALUES (
                                                @userid,
                                                @password,
                                                @salt)";

                MySqlCommand newPasswordCMD = new MySqlCommand(newPassword, con.Connection);
                string salt = GenSalt();
                string hashedPassword = Hash256Password(password, salt);
                newPasswordCMD.Parameters.AddWithValue("@userid", lastId);
                newPasswordCMD.Parameters.AddWithValue("@password", hashedPassword);
                newPasswordCMD.Parameters.AddWithValue("@salt", salt);
                newPasswordCMD.ExecuteNonQuery();
                con.Connection.Close();
                return true;
            } catch (System.Exception) {
                return false;
            }
        } ///public string RegisterAdmin




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
