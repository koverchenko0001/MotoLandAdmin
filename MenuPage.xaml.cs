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
        private CommandCom _command;

        public MenuPage(MainWindow mainWindow) {
            InitializeComponent();
            _mainWindow = (MainWindow)Application.Current.MainWindow;
        }


//            Connect conn = new Connect();

  
        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            Core cs = new Core();
            if (sender is MenuItem btn) {
                switch ( btn.Name) {
                    case "Exit":
                        _mainWindow.Close();
                        break;
                    case "changePassword":
                        //_mainWindow.MainFrame.Navigate(new ());
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
/*            _command = new CommandCom();
            userNameItem.Header = _command.loggedUser;*/
        }
        ///private void MenuItem_Click



    }
}
