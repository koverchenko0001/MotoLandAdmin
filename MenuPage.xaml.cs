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
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page {
        public MenuPage() {
            InitializeComponent();
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
    }
}
