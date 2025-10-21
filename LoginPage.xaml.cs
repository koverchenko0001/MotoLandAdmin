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
        private CommandCom _command;
        private Boolean loginFieldsAreEmpty = true;


        public LoginPage() {
            InitializeComponent();
            _mainWindow = (MainWindow)Application.Current.MainWindow;
        }


        private void setNewDimensions() {
            Application.Current.MainWindow.Height = 600;
            Application.Current.MainWindow.Width = 1000;
            Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;

            _mainWindow.MainFrame.Navigate(new MenuPage(_mainWindow));
        } ///private void setNewDimensions


        private void userMailTB_TextChanged(object sender, TextChangedEventArgs e) {
            //checkFieldsForEmpty();
        } ///private void userMailTB_TextChanged


        private void userPasswordPB_PasswordChanged(object sender, RoutedEventArgs e) {
            //checkFieldsForEmpty();
        } ///private void userPasswordPB_PasswordChanged


        private void checkFieldsForEmpty() {
            if (string.IsNullOrWhiteSpace(userMailTB.Text) || string.IsNullOrWhiteSpace(userPasswordPB.Password)) {
                loginUserBtn.IsEnabled = false;
            } else {
                loginUserBtn.IsEnabled = true;
            }
        } ///private void checkFieldsForEmpty


        private void loginUserBtn_Click(object sender, RoutedEventArgs e) {
            try {
                _command = new CommandCom();
                if (_command.LoginUser(userMailTB.Text, userPasswordPB.Password)) {
                    Core cs = new Core();
                    cs.setNewDimensions("menu");
                } else {
                    MessageBox.Show("Hibás bejelentkezési adatok!\nCsak mondom!", "Bejelentkezési hiba!");
                    resetLoginFields();
                }
            } catch (System.Exception ex) {

                MessageBox.Show(ex.Message, "Error");
            }
        } ///private void loginUserBtn_Click


        private void resetLoginFields() {
            userMailTB.Clear();
            userPasswordPB.Clear();
            userMailTB.Focus();
        } ///private void resetLoginFields

    }
}
