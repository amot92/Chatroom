using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SyncServer
{
    static Server server;
    static String echoPassword = "";
    static Authenticator auth;

    private static Random random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static int Main(String[] args)
    {
        auth = new Authenticator("./users.txt");
        server = new Server();
        echoPassword = RandomString(12);

        while (true)
        {
            //1. form connection
            server.StartConnection();//this method relies on an incoming connection

            //2. listen for message & process it --- then do it again, forever
            while (true)
            {
                String request = server.StartListening();
                String[] words = request.Split('|');
                if (words[0].Equals("Login"))
                {
                    login(words[1]);
                }
                else if (words[0].Equals("Register"))
                {
                    register(words[1]);
                }
                else if (words[0].Equals(echoPassword))
                {
                    echo(words[1]);
                }
                else if (words[0].Equals("Logout"))
                {
                    logout();
                    break;
                }
                else
                {
                    server.send("invalid request");
                }
            }
        }
        return 0;
    }

    public static void logout()
    {
        server.send("logout");
        server.CloseConnection();
    }

    public static void login(String loginString)
    {
        if (server != null)
        {
            if (auth.authenticate(loginString))
            {
                server.send(echoPassword);
            }
            else
            {
                server.send("no match found!");
            }
        }
    }

    public static void register(String registerString)
    {
        if (server != null)
        {
            if (!auth.check(registerString))
            {
                auth.register(registerString);
                server.send("success");
            }
            else
            {
                server.send("fail");
            }
        }
    }

    public static void echo(String echoString)
    {
        server.send(echoString);
    }
}