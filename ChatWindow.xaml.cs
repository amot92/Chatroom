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
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public App myApp;   // Used to have a reference to the App object in the Window.
        public Client client;
        public String echoPassword;
        public String userName;

        public ChatWindow(App myApp, Client client, String userName)
        {
            InitializeComponent();
            this.client = client;
            this.myApp = myApp;
            nameLabel.Content = userName;
            this.userName = userName;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            send();
        }

        public void send()
        {
            if (sendBox.Text.Equals(""))
                return;

            String message = userName + ':' + sendBox.Text;
            String toSend = echoPassword + '|' + message;

            //send message
            //& block until response is available
            String response = client.SendMessage(toSend);
            response += '\n';
            chatBox.Text += response;
            sendBox.Text = "";
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            String message = "Logout";

            //send message
            //& block until response is available
            String response = client.SendMessage(message);

            LoginWindow theWindow = new LoginWindow();
            theWindow.Show();
            this.Close();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                send();
            }
        }
    }
}
