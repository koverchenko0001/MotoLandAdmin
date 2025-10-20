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

        private void Button_Click(object sender, RoutedEventArgs e) {
            if (sender is Button button) {
                switch (button.Name) {
                    case "ExitMenu":
                        Application.Current.Shutdown();
                        break;
                    default:
                        break;
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {

        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();

        }

        private void NewMenu_Click(object sender, RoutedEventArgs e) {
            Connect conn = new Connect();
        }

        private void logOut_Click(object sender, RoutedEventArgs e) {
            logOutUser();
        }

        private void logOutUser() {
            Application.Current.MainWindow.Height = 500;
            Application.Current.MainWindow.Width = 300;
            Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;

            _mainWindow.MainFrame.Navigate(new LoginPage());
        } ///private void logOutUser
    }
}
