using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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


    public partial class MyAccountPage : Page {

        MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;
        CommandCom commandCom = new CommandCom();

        public MyAccountPage() {
            InitializeComponent();
            _mainWindow.statusBarText.Text = "ESC-Kilépés   F2-Mentés";
            uploadControls();
        }

        private void uploadControls() {
            DataTable dt;

            dt = commandCom.getTableFromDB("countries_mstr");
            /// COUNTRIES TABLE
            addressCountriesCB.ItemsSource = dt.DefaultView;
            addressCountriesCB.SelectedValuePath = "CountriesID_MSTR";
            addressCountriesCB.DisplayMemberPath = "CountriesCountry_MSTR";

            dt = commandCom.getTableFromDB("cities_mstr");
            /// CITIES TABLE
            addressCitiesCB.ItemsSource = dt.DefaultView;    
            addressCitiesCB.SelectedValuePath = "CitiesID_MSTR";
            addressCitiesCB.DisplayMemberPath = "CitiesCity_MSTR";
            /// BIRTHPLACE TABLE
            birthPlaceCB.ItemsSource = dt.DefaultView;
            birthPlaceCB.SelectedValuePath = "CitiesID_MSTR";
            birthPlaceCB.DisplayMemberPath = "CitiesCity_MSTR";

            dt = commandCom.getTableFromDB("gender_mstr");
            /// GENDER TABLE
            genderCB.ItemsSource = dt.DefaultView;
            genderCB.SelectedValuePath = "GenderID_MSTR";
            genderCB.DisplayMemberPath = "GenderGender_MSTR";

            /// USER DATA
            DataTable dtUsers = commandCom.getUserDataByID(_mainWindow.uid);
            foreach (DataRow row in dtUsers.Rows) {
                nickNameTB.Text = row["UserNickName_MSTR"].ToString();
                firstNameTB.Text = row["UserFirstName_DET"].ToString();
                middleNameTB.Text = row["UserMiddleName_DET"].ToString();
                lastNameTB.Text = row["UserLastName_DET"].ToString();
                birthPlaceCB.SelectedValue = row["UserBirthPlaceID_DET"].ToString();
                birthDateDP.SelectedDate = Convert.ToDateTime(row["UserBirthDate_DET"]);
                genderCB.SelectedValue = row["UserGenderID_DET"].ToString();
                emailTB.Text = row["UserMail_MSTR"].ToString();
                phoneTB.Text = row["UserPhone_DET"].ToString();
                motherNameTB.Text = row["UserMotherName_DET"].ToString();
                addressCountriesCB.SelectedValue = row["UserCountryID_DET"].ToString();
                addressCitiesCB.SelectedValue = row["UserCityID_DET"].ToString();
                addressStreetTB.Text = row["UserStreet_DET"].ToString();
                addressAddressTB.Text = row["UserAddress_DET"].ToString();
                addressPostCodeTB.Text = row["UserPostCode_DET"].ToString();
                userRegistrationDateDP.Text = row["UserRegDate_DET"].ToString();
                userLastModifiedDateDP.Text = row["UserLastModifiedDate_DET"].ToString();
            }

            firstNameTB.Focus();
        }/// uploadControls

        private void saveProfileBtn_Click(object sender, RoutedEventArgs e) {
            saveProfil();
        } /// saveProfileBtn_Click

        public void saveProfil() {
            if (MessageBox.Show("Menthetem?", "Információ", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes) {
                DateTime? birthDate = birthDateDP.SelectedDate;

                commandCom.updateUserProfile(
                                _mainWindow.uid,
                                nickNameTB.Text,
                                firstNameTB.Text,
                                middleNameTB.Text,
                                lastNameTB.Text,
                                Convert.ToInt32(birthPlaceCB.SelectedValue),
                                birthDate.Value,
                                Convert.ToInt32(genderCB.SelectedValue),
                                emailTB.Text,
                                phoneTB.Text,
                                motherNameTB.Text,
                                Convert.ToInt32(addressCountriesCB.SelectedValue),
                                Convert.ToInt32(addressCitiesCB.SelectedValue),
                                addressStreetTB.Text,
                                addressAddressTB.Text,
                                addressPostCodeTB.Text);
                _mainWindow.activPageName = "HomePage";
                _mainWindow.MainFrame.Navigate(new HomePage());
            }            
        } /// sa

        private void cancelProfilBtn_Click(object sender, RoutedEventArgs e) {
            cancelProfil();
        } /// cancelProfilBtn_Click

        public void cancelProfil() {
            _mainWindow.activPageName = "HomePage";
            _mainWindow.MainFrame.Navigate(new HomePage());
        }
    }

}
