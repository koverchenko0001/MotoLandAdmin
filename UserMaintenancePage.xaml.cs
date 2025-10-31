using MySqlX.XDevAPI.Relational;
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
        private int selectedRowIndex = 0;

        List<int> idList = new List<int>();




        public UserMaintenancePage() {
            InitializeComponent();
            _mainWindow.statusBarText.Text = "ESC-Kilépés   Ctrl+S-Mentés   F2-Cella szerkesztés   F3-Új felhasználó  F4-Cella lista  F5-Frissítés   F8-Törlés";

            refreshUsers();
        }

        private void usersSave_Click(object sender, RoutedEventArgs e) {
            saveUserMaintenancePage();    
        }

        private void usersCancel_Click(object sender, RoutedEventArgs e) {
            cancelUserMaintenancePage();
        }

        private void cancelUserMaintenancePage() {
            if (isChanged) {
                MessageBoxResult res =
                    MessageBox.Show("A tartalom megváltozott!\nSzeretnéd menteni?",
                                    "Figyelem!",
                                    MessageBoxButton.YesNoCancel,
                                    MessageBoxImage.Question,
                                    MessageBoxResult.Cancel);
                if (res == MessageBoxResult.Yes) {
                    saveUserMaintenancePage();
                    exitUserMaintenance();
                } else if (res == MessageBoxResult.No)
                    exitUserMaintenance();
            } else 
                exitUserMaintenance();
        }

        private void exitUserMaintenance() {
            isChanged = false;
            _mainWindow.activPageName = "HomePage";
            _mainWindow.MainFrame.Navigate(new HomePage());
            _mainWindow.resetActiveItem();
        }

        private void saveUserMaintenancePage() {
            DataRowView r;
            for (int ic = 0; ic < idList.Count; ic++) {
                userDataGrid.SelectedIndex = idList[ic];
                r = (DataRowView)userDataGrid.SelectedItems[0];

                var userRow = new {
                    uID = r["UserID_MSTR"],
                    uName = r["UserNickName_MSTR"],
                    uFirstName = r["UserFirstName_DET"],
                    uMiddleName = r["UserMiddleName_DET"],
                    uLastName = r["UserLastName_DET"],
                    uGender = r["GenderID_MSTR"],
                    uPhone = r["UserPhone_DET"],
                    uCountry = r["CountriesID_MSTR"],
                    uPostCode = r["UserPostCode_DET"],
                    uCity = r["UserCity_DET"],
                    uStreet = r["UserStreet_DET"],
                    uAddress = r["UserAddress_DET"],
                    uMotherName = r["UserMotherName_DET"],
                    uBirthPlace = r["UserBirthPlace_DET"],
                    uBirthDate = r["UserBirthDate_DET"]
                };

                command.updateUser(userRow);

            }
            isChanged = false;
            userDataGrid.Focus();
            idList.Clear();
            MessageBox.Show("Frissítve!", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void userDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) {
            isChanged = true;

            var currentRowIndex = userDataGrid.Items.IndexOf(userDataGrid.CurrentItem);
            if (!idList.Contains(currentRowIndex)) 
                idList.Add(currentRowIndex);
        }


        private void userDataGrid_KeyUp(object sender, KeyEventArgs e) {

            if (e.Key == Key.Escape) {
                cancelUserMaintenancePage();
            }
            if (e.Key == Key.S && Keyboard.Modifiers == ModifierKeys.Control) { 
                saveUserMaintenancePage();
            }
            if (e.Key == Key.F3) {
                userAdd();
            }
            if (e.Key == Key.F5) {
                refreshUsers();
            }
            if (e.Key == Key.F6) {
                showUserDetails();
            }
            if (e.Key == Key.F8) {
                userRemove();
            }
            e.Handled = true;
        }

        private void usersAdd_Click(object sender, RoutedEventArgs e) {
            userAdd();
        }

        private void userAdd() { 
            NewUserWindow newUserWindow = new NewUserWindow();
            newUserWindow.ShowDialog();
            refreshUsers();
        }

        private void usersRefresh_Click(object sender, RoutedEventArgs e) {
            refreshUsers();
        }

        private void refreshUsers() {

            /// COUNTRIES COMBOBOX UPLOADING
            countryCB.ItemsSource = command.getCountries().DefaultView;
            countryCB.DisplayMemberPath = "CountriesCountry_MSTR";
            countryCB.SelectedValuePath = "CountriesID_MSTR";
            /// GENDER COMBOBOX UPLOADING
            genderCB.ItemsSource = command.getGender().DefaultView;
            genderCB.DisplayMemberPath = "GenderGender_MSTR";
            genderCB.SelectedValuePath = "GenderID_MSTR";

            /// DATAGRID UPLOADING
            userDataGrid.ItemsSource = command.getAllUser();
            userDataGrid.Focus();
            userDataGrid.SelectedIndex = selectedRowIndex;
        }

        private void userRemove() {
            if (MessageBox.Show("Biztos, hogy törölni akarod?\nA felhasználó minden adata törlésre kerül!",
                                "Törlés",
                                
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes) {

                if (userDataGrid.SelectedItem is DataRowView item) {
                    var user = new {
                        uid = item["UserID_MSTR"]
                    };
                    command.deleteUser(user);
                }
                refreshUsers();
            }
        }

        private void showUserDetails() {
            List<object> userDetails = new List<object>();

            if (userDataGrid.SelectedItem is DataRowView r) {
                userDetails.Add(r["UserRegDate_DET"]);
                userDetails.Add(r["UserLastModifiedDate_DET"]);
                userDetails.Add(r["UserTypeID_MSTR"]);
                userDetails.Add(r["UserFlagID_MSTR"]);
                userDetails.Add(r["UserID_MSTR"]);
                userDetails.Add(r["UserNickname_MSTR"]);
            }
            UserDetails userDetailWindow = new UserDetails();
            userDetailWindow.setDetailRow(userDetails);
            userDetailWindow.ShowDialog();
            refreshUsers();
        }

        private void usersRemove_Click(object sender, RoutedEventArgs e) {
            userRemove();
        }

        private void usersDetails_Click(object sender, RoutedEventArgs e) {
            showUserDetails();
        }
    }
}
