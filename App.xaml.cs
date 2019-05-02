using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public List<Window> myWindows;
        public Boolean doingCloseAll = false;

        public App()
        {
            myWindows = new List<Window>();

            // I am taking over the creation of the main window here in the App
            // This means I had to modify the App.xaml so on startup it doesn't
            // create a window.

            // Create main application window, starting minimized if specified
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.myApp = this;  // In the window have a pointer to the App object so the App methods can be called from the window.
            myWindows.Add(loginWindow);  // Add the main window to the list of windows.
            loginWindow.Show();
        }

        public void AddWindow(Window myWindow)
        {
            // put the window in the list of windows
            myWindows.Add(myWindow);
        }
    }
}
