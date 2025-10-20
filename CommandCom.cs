using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MotoLandAdmin {
    internal class CommandCom {

        Connect con = new Connect();

        public bool LoginUser(string usermail, string userpassword) {
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
                bool isValid = false;
                while (reader.Read()) {
                    isValid = true;
/*                    User user = new User {
                        id = Convert.ToInt32(reader["UserID_MSTR"]),
                        nickname = reader["UserNickName_MSTR"].ToString(),
                        mail = reader["UserMail_MSTR"].ToString()
                    };*/
                    User user = new User(Convert.ToInt32(reader["UserID_MSTR"]),
                                         reader["UserNickName_MSTR"].ToString(),
                                         reader["UserMail_MSTR"].ToString());


                    /*                    Actor actor = new Actor {
                                            ActorId = Convert.ToInt32(reader["actor_id"]),
                                            FirstName = reader["first_name"].ToString(),
                                            LastName = reader["last_name"].ToString(),
                                            LastUpdate = Convert.ToDateTime(reader["last_update"])
                                        };
                                        actors.Add(actor);*/
                }




                //bool isValid = reader.Read();

                reader.Close();
                con.Connection.Close();

                return isValid;

            } catch (System.Exception ex) {
                MessageBox.Show(ex.Message);
                return false;
            }

        } ///public bool LoginUser



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
