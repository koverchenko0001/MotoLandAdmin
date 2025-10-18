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
        }

        private void loginButton_Click(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Height = 600;
            Application.Current.MainWindow.Width = 1000;
            Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
        }
    }
}
