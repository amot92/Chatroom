using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Net;

class Server
{
    Socket handler;
    Socket listener;

    public Server()
    {
        // Establish the local endpoint for the socket.  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 14821);

        // Create a TCP/IP socket.  
        listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and   
        // listen for incoming connections. 
        listener.Bind(localEndPoint);
        listener.Listen(10);
    }
    public void StartConnection()
    {
        // listen for incoming connections.  
        try
        {
            // Program is suspended while waiting for an incoming connection.  
            Console.WriteLine("Waiting for a connection...");
            handler = listener.Accept();
            Console.WriteLine("Connected to {0} on socket 14821", handler.RemoteEndPoint);
        }
        catch (SocketException se)
        {
            Console.WriteLine("SocketException : {0}", se.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }
    }

    public void send(String msg)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(msg);
        handler.Send(bytes);
        Console.WriteLine("Text Sent: " + msg);
    }

    public String StartListening()
    {
        try
        {
            byte[] bytes = new Byte[1024];
            
            // Program is suspended while waiting for message.  
            Console.WriteLine("Waiting for bytes...");
            int bytesRec = handler.Receive(bytes);
            String data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            Console.WriteLine("Text received : {0}", data);
            return data;
        }
        catch (SocketException se)
        {
            Console.WriteLine("SocketException : {0}", se.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("Unexpected exception : {0}", e.ToString());
        }
        return null;
    }

    public void CloseConnection()
    {
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
    }
}
