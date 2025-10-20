using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MotoLandAdmin {
    internal class core {

        private MainWindow _mainWindow;
        private double appWidth, appHeight;

        public void setNewDimensions(string dimension) {

            _mainWindow = (MainWindow)Application.Current.MainWindow;

            if (dimension == "login") {
                appWidth = 300;
                appHeight = 500;
                _mainWindow.MainFrame.Navigate(new LoginPage());
            } else if (dimension == "menu") {
                appWidth = 1000;
                appHeight = 600;
                _mainWindow.MainFrame.Navigate(new MenuPage(_mainWindow));
            }

            _mainWindow.Width = appWidth;
            _mainWindow.Height = appHeight;

        } ///private void setNewDimensions

    }
}
