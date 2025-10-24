using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                string sql = "";
                MySqlCommand cmd;
                /// USER_MSTR TABLE INSERT
                sql = @"INSERT INTO 
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

                cmd = new MySqlCommand(sql, con.Connection);
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




                /// USER_DET TABLE INSERT
                sql = @"INSERT INTO 
                            user_det (
                                UserMSTRID_DET, 
                                UserFirstName_DET, 
                                UserMiddleName_DET, 
                                UserLastName_DET,
                                UserGenderID_DET,
                                UserPhone_DET,
                                UserCountryID_DET,
                                UserPostCode_DET,
                                UserCityID_DET,
                                UserStreet_DET,
                                UserAddress_DET,
                                UserMotherName_DET,
                                UserBirthPlaceID_DET,
                                UserBirthDate_DET,
                                UserLastModifiedDate_DET) 
                        VALUES (
                                @uid,
                                '',
                                '',
                                '',
                                0,
                                '',
                                0,
                                '',
                                0,
                                '',
                                '',
                                '',
                                0,
                                '1970-01-01',
                                @lastmodified)";

                cmd = new MySqlCommand(sql, con.Connection);
                cmd.Parameters.AddWithValue("@uid", lastId);
                cmd.Parameters.AddWithValue("@lastmodified", DateTime.Now);
                cmd.ExecuteNonQuery();





                /// PASSWORD_MSTR TABLE INSERT
                sql = @"INSERT INTO 
                            password_mstr (
                                PasswordUserID_MSTR, 
                                PasswordPassword_MSTR,  
                                PasswordSalt_MSTR) 
                            VALUES (
                                @userid,
                                @password,
                                @salt)";

                cmd = new MySqlCommand(sql, con.Connection);
                string salt = GenSalt();
                string hashedPassword = Hash256Password(password, salt);
                cmd.Parameters.AddWithValue("@userid", lastId);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@salt", salt);
                cmd.ExecuteNonQuery();
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

        public DataTable getTableFromDB(string tableName) {
            try {
                con.Connection.Open();
                string sql = "SELECT * FROM " + tableName;
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, con.Connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                con.Connection.Close();
                return dt;
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        } /// public DataTable getTableFromDB

        public DataTable getUserDataByID(string uid) {
            try {
                con.Connection.Open();
                string sql = "SELECT * FROM user_mstr, user_det WHERE UserID_MSTR = " + uid + " AND UserMSTRID_DET = UserID_MSTR";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, con.Connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                con.Connection.Close();
                return dt;
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        } /// public DataTable getUserTypesFromTable


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


        public void updateUserProfile(string uid, string nickname, string firstname, string middlename, string lastname, int birthplaceid, DateTime     
                                       birthdate, int genderid, string email, string phone, string motherName, int countryID, int cityid, string street, 
                                       string address, string postcode) {
            try {
                con.Connection.Open();
                
                string sql = @"UPDATE
                                    user_mstr, user_det 
                                SET 
                                    UserNickName_MSTR = @nickname,
                                    UserFirstName_DET = @firstname,
                                    UserMiddleName_DET = @middlename,
                                    UserLastName_DET = @lastname,
                                    UserBirthPlaceID_DET = @birthplaceid,
                                    UserBirthDate_DET = @birthdate,
                                    UserGenderID_DET = @genderid,
                                    UserMail_MSTR = @email,
                                    UserPhone_DET = @phone,
                                    UserMotherName_DET = @mothername,
                                    UserCountryID_DET = @countryid,
                                    UserCityID_DET = @cityid,
                                    UserStreet_DET = @street,
                                    UserAddress_DET = @address,
                                    UserPostCode_DET = @postcode,
                                    UserLastModifiedDate_DET = @moddate 
                                WHERE
                                    UserID_MSTR = @uid AND
                                    UserMSTRID_DET = UserID_MSTR";

                MySqlCommand cmd = new MySqlCommand(sql, con.Connection);

                cmd.Parameters.AddWithValue("@uid", uid);
                cmd.Parameters.AddWithValue("@nickname", nickname);
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@middlename", middlename);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@birthplaceid", birthplaceid);
                cmd.Parameters.AddWithValue("@birthdate", birthdate);
                cmd.Parameters.AddWithValue("@genderid", genderid);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@mothername", motherName);
                cmd.Parameters.AddWithValue("@countryid", countryID);
                cmd.Parameters.AddWithValue("@cityid", cityid);
                cmd.Parameters.AddWithValue("@street", street);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@postcode", postcode);
                cmd.Parameters.AddWithValue("@moddate", DateTime.Now);

                cmd.ExecuteNonQuery();

                con.Connection.Close();
            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                con.Connection.Close();
            }

        } ///public void UpdateUser



    } ///internal class CommandCom
}
