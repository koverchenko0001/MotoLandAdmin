using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

        /// FOR SETTING MAIN MENU ITEMS
        public string uid = "";
        public string username = "";
        public int usertype = 0;
        public int userFlag = 0;

        public string activPageName = "";


        public MainWindow() {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
        }


        public void setUser(string userId, string userName, int usertype, int userFlag) {
            this.uid = userId;
            this.username = userName;
            this.usertype = usertype;
            this.userFlag = userFlag;

            /// SET USERNAME IN USER MENU HEADER
            userNameItem.Header = this.username;
            /// HIDE ITEMS IF NOT ADMIN OR ROOT
            usersMainItem.Visibility = 
                (this.usertype == 4 || this.usertype == 5) ? Visibility.Visible : Visibility.Collapsed;


        } ///public void setUser

        private void Window_Closing(object sender, CancelEventArgs e) {
            e.Cancel = MessageBox.Show("Biztos, hogy bezárod?", "Kilépés",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No;
        } ///private void Window_Closing

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            Core cs = new Core();
            if (sender is MenuItem btn) {
                activPageName = btn.Name;
                switch (btn.Name) {
                    case "Exit":
                        Close();
                        break;
                    case "accountSetup":
                        MainFrame.Navigate(new MyAccountPage());
                        break;
                    case "properties":
                        MainFrame.Navigate(new PropertiesPage());
                        break;
                    case "changePassword":
                        MainFrame.Navigate(new ChangePasswordPage(4));
                        break;
                    case "logOut":
                        cs.setNewDimensions("login");
                        break;
                    default:
                        break;
                }
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e) {

            if (activPageName == "HomePage") {
                if (e.Key == Key.Escape) {
                    Close();
                }
            }

            if (activPageName == "accountSetup") { 
                MyAccountPage myAccountPage = new MyAccountPage();
                if (e.Key == Key.Escape) {
                    myAccountPage.cancelProfil();
                }
                if (e.Key == Key.F2) {
                    myAccountPage.saveProfil();
                }
                    
            }


                    
                        

        }
        ///private void MenuItem_Click



    }
}
