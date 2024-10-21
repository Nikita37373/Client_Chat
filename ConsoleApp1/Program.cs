using System.Data;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp1
{
    class serverChat
    {
        const short port = 4040;
        const string JOIN_CMD = "$<join>";
        UdpClient server ;
        List<IPEndPoint> members ;
        IPEndPoint clientEndpoint = null;
        public serverChat()
        {
            server = new UdpClient(port);
            members = new List<IPEndPoint>();

        }
        private void AddMember()
        {
            members.Add(clientEndpoint);
            Console.WriteLine("Member was added");
        }
        private void SendToAll(byte[]data )
        { 
            foreach (var member in members) 
            {
                server.SendAsync(data, data.Length, member);
            }
        }
        public void Start()
        {
           
            while (true)
            {
                byte[] data = server.Receive(ref clientEndpoint);
                string mess = Encoding.UTF8.GetString(data);
                Console.WriteLine($"{mess} from {clientEndpoint}. Date {DateTime.Now}");
                switch (mess)
                {
                    case JOIN_CMD:
                        AddMember();
                        break;
                    default:
                        SendToAll(data);
                        break;
                }
            }
        }
    }
    internal class Program
    {
        
        static void Main(string[] args)
        {
            serverChat chat = new serverChat();
            chat.Start();
            
        }
    }
}