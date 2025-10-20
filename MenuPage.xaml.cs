using Mysqlx.Connection;
using Mysqlx.Notice;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MotoLandAdmin {


    //SqlStatements sql = new SqlStatements();

    public partial class MenuPage : Page {

        private MainWindow _mainWindow;

        public MenuPage(MainWindow mainWindow) {
            InitializeComponent();
            _mainWindow = (MainWindow)Application.Current.MainWindow;
        }


//            Connect conn = new Connect();

  
        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            core cs = new core();
            if (sender is MenuItem btn) {
                switch ( btn.Name) {
                    case "Exit":
                        _mainWindow.Close();
                        break;
                    case "logOut": 
                            cs.setNewDimensions("login");
                            //_mainWindow.MainFrame.Navigate(new LoginPage());
                        break;
                    default:
                        break;
                }
            }
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e) {
            User user = new User();
            userNameItem.Header = user.ToString();
        }
        ///private void MenuItem_Click



    }
}
