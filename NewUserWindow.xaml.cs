using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace MotoLandAdmin {
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window {

        MainWindow _mainWindow = new MainWindow();
        CommandCom command = new CommandCom();

        public NewUserWindow() { 
            InitializeComponent();
            DataTable flagDT = command.getFlag();
            userFlagCB.ItemsSource = flagDT.DefaultView;
            userFlagCB.SelectedValuePath = "FlagID_MSTR";
            userFlagCB.DisplayMemberPath = "FlagFlag_MSTR";
            userFlagCB.SelectedIndex = 0;

            //IF LOGGED USER ISN'T ROOT LEVEL THAN TRUE ELSE FALSE
            userFlagCB.IsReadOnly = (_mainWindow.usertype == 5) ? false : true;
         
            DataTable typeDT = command.getType();
            userTypeCB.ItemsSource = typeDT.DefaultView;
            userTypeCB.SelectedValuePath = "UserTypeID_MSTR";
            userTypeCB.DisplayMemberPath = "UserTypeType_MSTR";
            userTypeCB.SelectedIndex = 2;

            DataTable genderDT = command.getGender();
            userGenderCB.ItemsSource = genderDT.DefaultView;
            userGenderCB.SelectedValuePath = "GenderID_MSTR";
            userGenderCB.DisplayMemberPath = "GenderGender_MSTR";

            DataTable countryDT = command.getCountries();
            userCountryCB.ItemsSource = countryDT.DefaultView;
            userCountryCB.SelectedValuePath = "CountriesID_MSTR";
            userCountryCB.DisplayMemberPath = "CountriesCountry_MSTR";

            DataTable cityDT = command.getCities();
            userCityCB.ItemsSource = cityDT.DefaultView;
            userCityCB.SelectedValuePath = "CitiesID_MSTR";
            userCityCB.DisplayMemberPath = "CitiesCity_MSTR";

            DataTable birthcityDT = command.getCities();
            userBirthPlaceCB.ItemsSource = birthcityDT.DefaultView;
            userBirthPlaceCB.SelectedValuePath = "CitiesID_MSTR";
            userBirthPlaceCB.DisplayMemberPath = "CitiesCity_MSTR";


            userNameTB.Focus();

        }

        private void Window_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                closeNewUser();
            }
        }

        private void newUserCancel_Click(object sender, RoutedEventArgs e) {
            closeNewUser();
        }

        private void newUserSave_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Új felhasználó rögzítve!","Mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            closeNewUser();
        }
        private void closeNewUser() {
            Close();
        }

    }
}
