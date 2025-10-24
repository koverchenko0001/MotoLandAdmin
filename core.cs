using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MotoLandAdmin {
    public class Core {

        private MainWindow _mainWindow;
        private double appWidth, appHeight;

        public void setNewDimensions(string dimension) {

            _mainWindow = (MainWindow)Application.Current.MainWindow;
            //User user = new User();

            if (dimension == "login") {
                _mainWindow.activPageName = "LoginPage";
                appWidth = 300;
                appHeight = 500;
                _mainWindow.MainMenu.Visibility = Visibility.Hidden;
                _mainWindow.statusBar.Visibility = Visibility.Hidden;
                _mainWindow.MainFrame.Navigate(new LoginPage());
                _mainWindow.ResizeMode = ResizeMode.NoResize;
            } else if (dimension == "menu") {
                _mainWindow.activPageName = "HomePage";
                appWidth = 1000;
                appHeight = 600;
                _mainWindow.MainMenu.Visibility = Visibility.Visible;
                _mainWindow.statusBar.Visibility = Visibility.Visible;
                _mainWindow.MainFrame.Navigate(new HomePage());
                _mainWindow.ResizeMode = ResizeMode.CanResize;
            }

            _mainWindow.Width = appWidth;
            _mainWindow.Height = appHeight;


        } ///private void setNewDimensions
    }
 }

