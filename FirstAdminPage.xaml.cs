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
    /// <summary>
    /// Interaction logic for FirstAdminPage.xaml
    /// </summary>
    public partial class FirstAdminPage : Page {
        MainWindow _mainWindow = (MainWindow) Application.Current.MainWindow;
        CommandCom command = new CommandCom();
        public FirstAdminPage() {
            InitializeComponent();
            adminNickNameTB.Focus();
        }

        private void adminCancelBtn_Click(object sender, RoutedEventArgs e) {
            _mainWindow.MainFrame.Navigate(new LoginPage());

        }

        private void adminRegistrationBtn_Click(object sender, RoutedEventArgs e) {
            if (command.RegisterAdmin(
                        adminNickNameTB.Text,
                        adminPasswordPB.Password,
                        adminMailTB.Text,
                        (adminTypeCB.Text == "admin") ? 4 : 5,
                        1)) {
                MessageBox.Show("Sikeres admin regisztráció!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                _mainWindow.MainFrame.Navigate(new LoginPage());
            } else {
                MessageBox.Show("Hiba az admin regisztráció során!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                _mainWindow.MainFrame.Navigate(new LoginPage());
            }
        }
    }
}
