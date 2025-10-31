using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window {

        CommandCom command = new CommandCom();
        private bool isChanged = false;

        public UserDetails() {
            InitializeComponent();

            DataTable typeDT = command.getType();

            userTypeCB.ItemsSource = typeDT.DefaultView;
            userTypeCB.SelectedValuePath = "UserTypeID_MSTR";
            userTypeCB.DisplayMemberPath = "UserTypeType_MSTR";

            DataTable flagDT = command.getFlag();

            userFlagCB.ItemsSource = flagDT.DefaultView;
            userFlagCB.SelectedValuePath = "FlagID_MSTR";
            userFlagCB.DisplayMemberPath = "FlagFlag_MSTR";


            userTypeCB.SelectionChanged += userTypeCB_SelectionChanged;
            userFlagCB.SelectionChanged += userFlagCB_SelectionChanged;

            isChanged = false;

        }
        public void setDetailRow(List<object> detailRow) {
            userRegDateTB.Text = detailRow[0].ToString();
            userLastDateTB.Text = detailRow[1].ToString();
            userTypeCB.SelectedValue = detailRow[2];
            userFlagCB.SelectedValue = detailRow[3];
            userIDTB.Text = detailRow[4].ToString();
            userNameL.Content = detailRow[5].ToString();
        }

        private void saveDetails() {
            command.updateUserDetails(userIDTB.Text, userTypeCB.SelectedValue.ToString(), userFlagCB.SelectedValue.ToString());
            MessageBox.Show("Frissítve!", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
            isChanged = false;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) 
                Close();

            if (e.Key == Key.F2)
                saveDetails();

            e.Handled = true;
        }

        private void userDetailsSaveBtn_Click(object sender, RoutedEventArgs e) {
            saveDetails();
            Close();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (isChanged) {
                MessageBoxResult res =
                    MessageBox.Show("A tartalom megváltozott!\nSzeretnéd menteni?",
                                    "Figyelem!",
                                    MessageBoxButton.YesNoCancel,
                                    MessageBoxImage.Question,
                                    MessageBoxResult.Cancel);
                if (res == MessageBoxResult.Yes) {
                    saveDetails();
                } else if (res == MessageBoxResult.No) {
                    e.Cancel = false;
                } else {
                    e.Cancel = true;
                }
            }
        }


        private void userTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.Source is ComboBox) {
                isChanged = true;
            }
        }

        private void userFlagCB_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (e.Source is ComboBox) {
                isChanged = true;
            }
        }
    }
}
