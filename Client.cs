using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client
{
    Socket client;

    public Client()
    {
        StartClient();
    }

    // Send data to connected remote device
    public String SendMessage(String str)
    {
        // Encode the data string into a byte array.
        byte[] msg;
        msg = Encoding.ASCII.GetBytes(str);

        // Send the data through the socket.  
        int bytesSent = client.Send(msg);

        // Data buffer for incoming data.  
        byte[] bytes = new byte[1024];

        // Receive the response from the remote device.  
        int bytesRec = client.Receive(bytes);

        // encode & return response
        String response = Encoding.ASCII.GetString(bytes, 0, bytesRec);
        return response;
    }

    // Connect to a remote device.  
    public void StartClient()
    {
        try
        {
            // Establish the remote endpoint for the socket.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 14821);

            // Create a TCP/IP  socket.  
            client = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                client.Connect(remoteEP);
                Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
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
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}