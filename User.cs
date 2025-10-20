using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoLandAdmin {
    public class User {

        public int id;
        public string nickname;
        public string mail;


        public User(int userId, string userNickname, string userMail) {
            this.id = userId;
            this.nickname = userNickname;
            this.mail = userMail;
        }

        public User() {
            this.id = 0;
            this.nickname = "";
            this.mail = "";
        }

        public override string ToString() {
            return $"User ID: {id}, Nickname: {nickname}, Mail: {mail}";
        }   

        public void clearUser() {
            id = 0;
            nickname = "";
            mail = "";
        }

        public string getUserInfo() {
            return $"ID: {id}, Nickname: {nickname}, Mail: {mail}";
        }

        public string getNickname() {
            return nickname;
        }

/*        public setNickName(string nickname) { 
            this.nickname = nickname;
        }*/

    }
}
