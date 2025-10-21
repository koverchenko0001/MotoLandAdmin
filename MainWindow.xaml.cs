using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public MainWindow() {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
        }

        private void Window_Closing(object sender, CancelEventArgs e) {
            e.Cancel = MessageBox.Show("Biztos, hogy bezárod?", "Kilépés",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No;
        } ///private void Window_Closing

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            Core cs = new Core();
            if (sender is MenuItem btn) {
                switch (btn.Name) {
                    case "Exit":
                        Close();
                        break;
                    case "accountSetup":
                        MainFrame.Navigate(new AccountSetupPage());
                        break;
                    case "properties":
                        MainFrame.Navigate(new PropertiesPage());
                        break;
                    case "changePassword":
                        MainFrame.Navigate(new ChangePasswordPage());
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



    }
}
