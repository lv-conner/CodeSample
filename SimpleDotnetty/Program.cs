using System;
using DotNetty.Handlers.Streams;
using DotNetty;
using DotNetty.Buffers;
using DotNetty.Transport;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Common;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace SimpleDotnetty
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerBootstrap bootstrap = new ServerBootstrap();
            var boss = new MultithreadEventLoopGroup(1);
            var worker = new MultithreadEventLoopGroup();
            bootstrap.Group(boss, worker);
            bootstrap.Channel<TcpServerSocketChannel>();
            Console.WriteLine("Hello World!");
        }
    }
}
