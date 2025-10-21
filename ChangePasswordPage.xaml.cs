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
        public ChangePasswordPage() {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e) {
            _mainWindow = (MainWindow)Application.Current.MainWindow;
            _mainWindow.MainFrame.Navigate(new StartPage(_mainWindow));
        }
    }
}
