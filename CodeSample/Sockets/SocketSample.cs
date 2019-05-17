using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CodeSample.Sockets
{
    public static class SocketSample
    {
        public static void Case()
        {
            Socket tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.AcceptAsync();
        }
    }
}
