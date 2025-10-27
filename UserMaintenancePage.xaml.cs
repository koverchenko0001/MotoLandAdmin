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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MotoLandAdmin {

    public partial class UserMaintenancePage : Page {
        CommandCom command = new CommandCom();
        MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        private bool isChanged = false;

        public UserMaintenancePage() {
            InitializeComponent();
            _mainWindow.statusBarText.Text = "ESC-Kilépés   F2-Mentés   F3-Szerkesztés";
            /// DATAGRID UPLOADING
            userDataGrid.ItemsSource = command.GetAllUser();
            userDataGrid.Focus();
            userDataGrid.SelectedIndex = 0;
            /// COUNTRIES COMBOBOX UPLOADING
            countryCB.ItemsSource = command.getCountries().DefaultView;
            countryCB.DisplayMemberPath = "CountriesCountry_MSTR";
            countryCB.SelectedValuePath = "CountriesID_MSTR";
            /// CITIES COMBOBOX UPLOADING
            cityCB.ItemsSource = command.getCities().DefaultView;
            cityCB.DisplayMemberPath = "CitiesCity_MSTR";
            cityCB.SelectedValuePath = "CitiesID_MSTR";
            /// CITIES COMBOBOX UPLOADING
            birthPlaceCityCB.ItemsSource = command.getCities().DefaultView;
            birthPlaceCityCB.DisplayMemberPath = "CitiesCity_MSTR";
            birthPlaceCityCB.SelectedValuePath = "CitiesID_MSTR";
            /// GENDER COMBOBOX UPLOADING
            genderCB.ItemsSource = command.getGender().DefaultView;
            genderCB.DisplayMemberPath = "GenderGender_MSTR";
            genderCB.SelectedValuePath = "GenderID_MSTR";
        }

        private void usersSave_Click(object sender, RoutedEventArgs e) {
            saveUserMaintenancePage();    
        }

        private void usersCancel_Click(object sender, RoutedEventArgs e) {
            cancelUserMaintenancePage();
        }

        private void cancelUserMaintenancePage() {
            if (isChanged) {
                if (MessageBox.Show("A tartalom megváltozott!\nSzeretnéd menteni?", "Figyelem!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) {
                    saveUserMaintenancePage();
                }
            }
            exitUserMaintenance();
        }

        private void exitUserMaintenance() {
            isChanged = false;
            _mainWindow.activPageName = "HomePage";
            _mainWindow.MainFrame.Navigate(new HomePage());
            _mainWindow.userMaintenance.IsEnabled = true;
        }

        private void saveUserMaintenancePage() { 

            if (userDataGrid.SelectedItem is DataRowView _row) {
                var userRow = new {
                    uID = _row["UserID_MSTR"],
                    uName = _row["UserNickName_MSTR"],
                    uFirstName = _row["UserFirstName_DET"],
                    uMiddleName = _row["UserMiddleName_DET"],
                    uLastName = _row["UserLastName_DET"],
                    uGender = _row["GenderGender_MSTR"],
                    uPhone = _row["UserPhone_DET"],
                    uCountry = _row["CountriesCountry_MSTR"],
                    uPostCode = _row["UserPostCode_DET"],
                    uCity = _row["CitiesCity_MSTR"],
                    uStreet = _row["UserStreet_DET"],
                    uAddress = _row["UserAddress_DET"],
                    uMotherName = _row["UserMotherName_DET"],
                    uBirthPlace = _row["CitiesCity_MSTR"],
                    uBirthDate = _row["UserBirthDate_DET"]
                };

                command.UpdateUser(userRow);
                userDataGrid.ItemsSource = command.GetAllUser();
            }
            MessageBox.Show("Mentve!");

        }

        private void userDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) {
            isChanged = true;
        }

        private void KeyUp(KeyEventArgs e) { 
            if (e.Key == Key.Escape) {
                cancelUserMaintenancePage();
            }
            if (e.Key == Key.F2) {
                saveUserMaintenancePage();
            }
            e.Handled = true;
        }

        private void userDataGrid_KeyUp(object sender, KeyEventArgs e) {
            KeyUp(e);
        }

        private void usersAdd_Click(object sender, RoutedEventArgs e) {
            NewUserWindow newUserWindow = new NewUserWindow();
            newUserWindow.ShowDialog();
        }
    }
}
