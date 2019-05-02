using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class LoginWindow : Window
    {
        public App myApp;   // Used to have a reference to the App object in the Window.
        public Client client;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if(client == null)
                client = new Client();

            //check textbox for contents
            String userString = userNameTextBox.Text + "," + passwordBox.Password;
            String loginString = "Login|" + userString;

            //will create connection
            //send message
            //& block until response is available
            String response = client.SendMessage(loginString);

            //check response
            if (response.Equals("no match found!"))
            {
                MessageBox.Show(response);
                return;
            }
            openChatWindow(response, userNameTextBox.Text);
        }
        
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            //check textbox for contents

            String userName = userNameTextBox.Text;
            String loginString = "Register|" + userName + "," + passwordBox.Password;

            //send message
            //& block until response is available
            String response = client.SendMessage(loginString);

            if (response.Equals("success"))
            {
                MessageBox.Show("user added succesfully\nPlease login now.");
            }
            else
            {
                MessageBox.Show("user already exists!");
            }
        }

        public void openChatWindow(String echoPassword, String userName)
        {
            ChatWindow theWindow = new ChatWindow(myApp, client, userName);
            theWindow.echoPassword = echoPassword;
            theWindow.Show();
            this.Close();
        }
    }
}
