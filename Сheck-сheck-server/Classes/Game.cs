using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Сheck_сheck_server.Classes
{
    public class Game
    {
        public string RoomName { get; }
        public Socket Player1 { get; private set; }
        public Socket Player2 { get; private set; }
        public bool IsPlayer1Turn { get; set; }
        public bool IsFull => Player1 != null && Player2 != null;

        public Game(string roomName)
        {
            RoomName = roomName;
            IsPlayer1Turn = true;
        }

        public bool TryAddPlayer(Socket player)
        {
            if (Player1 == null)
            {
                Player1 = player;
                return true;
            }
            else if (Player2 == null)
            {
                Player2 = player;
                return true;
            }
            return false;
        }

        public GameRoom StartGameRoom(string port)
        {
            GameRoom newGemeRoom = new GameRoom(port, Player1, Player2);
            return newGemeRoom;
        }
    }
}
