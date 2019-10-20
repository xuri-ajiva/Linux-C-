using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace ConsoleApplication1 {
    internal class Program {
        private const string MESSAGE       = "Test message \r\n";
        private const int    BUFFER_LENGTH = 20000;
        private const int    PORT          = 1123;

        private static readonly Thread Server  = new Thread( ServerVoid );
        private static readonly Thread Client = new Thread( ClientVoid );

        public static void Main(string[] args) {
            try {
                //start server
                Server.Start();
                
                //start clinet
                Client.Start();
                
                Console.ReadLine();
            } catch (Exception e) {
                Console.WriteLine( e );
            }
        }

        private static void ServerVoid() {
            Console.WriteLine( "0" );
            var s = new Socket( SocketType.Stream, ProtocolType.Tcp );
            Console.WriteLine( "1" );
            s.Bind( new IPEndPoint( IPAddress.Any, PORT ) );

            Console.WriteLine( "2" );
            s.Listen( 111 );
            var sc = s.Accept();
            
            
            Console.WriteLine( "6" );
            sc.Send( Encoding.UTF8.GetBytes( MESSAGE ) );
        }

        private static void ClientVoid() {
            Console.WriteLine( "3" );
            var c = new Socket( SocketType.Stream, ProtocolType.Tcp );
            Console.WriteLine( "4" );
            c.Connect( IPAddress.Parse( "127.0.0.1" ), PORT );

            Console.WriteLine( "5" );
            //Thread.Sleep( 1000 );

            var buffer = new byte[BUFFER_LENGTH];

            c.Receive( buffer );
            Console.WriteLine( "7" );
            Console.WriteLine( Encoding.UTF8.GetString( buffer ) );

        }
    }
}