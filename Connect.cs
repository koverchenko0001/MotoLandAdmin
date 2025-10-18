using MySql.Data.MySqlClient;

namespace MotoLandAdmin {
    internal class Connect {
        public MySqlConnection Connection;

        private string ConnectionString;

        public Connect() {
            ConnectionString = $"SERVER=localhost;DATABASE=c2465motolanddb;UID=root;PASSWORD=;SslMode=None";
            Connection = new MySqlConnection(ConnectionString);
        }
    }
}
