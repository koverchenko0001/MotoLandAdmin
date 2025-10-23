using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoLandAdmin {
    public class User {

        public string id;
        public string nickname;
        public string mail;
        public int usertype;


        /*public User(int userId, string userNickname, string userMail) {
            this.id = userId;
            this.nickname = userNickname;
            this.mail = userMail;
        }*/

        public User() {
            this.id = "";
            this.nickname = "";
            this.mail = "";
            this.usertype = 0;
        }

    }
}
