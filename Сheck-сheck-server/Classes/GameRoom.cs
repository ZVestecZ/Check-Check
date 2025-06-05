using System.Net.Sockets;

namespace Сheck_сheck_server.Classes
{
    public class GameRoom
    {
        private string port { get; }
        public Socket Player1 { get; private set; }
        public Socket Player2 { get; private set; }
        public GameRoom(string port, Socket player1, Socket player2)
        {
            this.port = port;
            this.Player1 = player1;
            this.Player2 = player2;
        }
    }
}
