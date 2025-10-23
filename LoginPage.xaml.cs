using Mysqlx.Connection;
using Mysqlx.Notice;
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


    public partial class LoginPage : Page {

        private MainWindow _mainWindow;


        public LoginPage() {
            InitializeComponent();
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            CommandCom _command;
            _command = new CommandCom();
            bool isAdminExist = _command.adminExist();
            addNewUserBtn.IsEnabled = !isAdminExist;
            
            userMailTB.IsEnabled = userPasswordPB.IsEnabled = isAdminExist;
        }


        private void userMailTB_TextChanged(object sender, TextChangedEventArgs e) {
            checkFieldsForEmpty();
        } ///private void userMailTB_TextChanged


        private void UserPasswordPB_PasswordChanged(object sender, RoutedEventArgs e) {
            checkFieldsForEmpty();
        } ///private void userPasswordPB_PasswordChanged


        private void checkFieldsForEmpty() {
            if (string.IsNullOrEmpty(userMailTB.Text) || string.IsNullOrEmpty(userPasswordPB.Password)) {
                loginUserBtn.IsEnabled = false;
            } else {
                loginUserBtn.IsEnabled = true;
            }
        } ///private void checkFieldsForEmpty


        private void loginUserBtn_Click(object sender, RoutedEventArgs e) {
            CommandCom _command;
            _command = new CommandCom();

            if (_command.LoginUser(userMailTB.Text, userPasswordPB.Password)) {
                Core cs = new Core();
                cs.setNewDimensions("menu");
            } else {
                MessageBox.Show("Hibás bejelentkezési adatok!\nCsak mondom!", "Bejelentkezési hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                resetLoginFields();
            }

        } ///private void loginUserBtn_Click


        private void resetLoginFields() {
            userMailTB.Clear();
            userPasswordPB.Clear();
            userMailTB.Focus();
        } ///private void resetLoginFields

        private void addNewUser_Click(object sender, RoutedEventArgs e) {
            _mainWindow.MainFrame.Navigate(new FirstAdminPage());
        } ///private void addNewUser_Click

    }
}
