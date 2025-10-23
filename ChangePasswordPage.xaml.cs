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
    /// Interaction logic for ChangePasswordPage.xaml
    /// </summary>
    public partial class ChangePasswordPage : Page {
        private MainWindow _mainWindow;
        private string uid => _mainWindow.uid;
        private int userFlag = 0;

        public ChangePasswordPage(int userFlag) {
            InitializeComponent();
            this.userFlag = userFlag;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) {
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            _mainWindow.MainFrame.Navigate(new LoginPage());
        } ///private void CancelBtn_Click

        private bool ChangePasswordValid() {
            bool isValid = true;
            if (oldPasswordBox.Password.Length < 6) {
                isValid = false;
            }
            if (newPasswordBox.Password.Length < 6) {
                isValid = false;
            }
            if (newPasswordBox.Password != cNewPasswordBox.Password) {
                isValid = false;
            }
            return isValid;
        } ///private bool ChangePasswordValid

        private void clearFields() {
            oldPasswordBox.Password = "";
            newPasswordBox.Password = "";
            cNewPasswordBox.Password = "";
            oldPasswordBox.Focus();
        } ///private void clearFields


        private void ChangeBtn_Click(object sender, RoutedEventArgs e) {
            if (!ChangePasswordValid()) {
                MessageBox.Show("Hibás adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else {
                MessageBoxResult result = 
                    MessageBox.Show("Biztos, hogy megváltoztatod a jelszavad?", "Jelszó módosítás",
                        MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No) {
                    return;
                }
            }
            CommandCom commandCom = new CommandCom();
            commandCom.ChangePassword(uid, oldPasswordBox.Password, newPasswordBox.Password);
        } ///private void ChangeBtn_Click
    }
}
