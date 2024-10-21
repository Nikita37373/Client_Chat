using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Finalka
{
    
    public partial class MainWindow : Window
    {
        IPEndPoint serverEndPoint;
        UdpClient client;
        ObservableCollection<MessageInfo> messages = new ObservableCollection<MessageInfo>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = messages;
            client = new UdpClient();
            string Address = ConfigurationManager.AppSettings["ServerAddress"]!;
            short Port = short.Parse(ConfigurationManager.AppSettings["ServerPort"]!);
            serverEndPoint = new IPEndPoint(IPAddress.Parse(Address), Port);
        }

        private void LeaveBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private  void JoinBtnClick(object sender, RoutedEventArgs e)
        {
            string message = "$<join>";
            SendMessage(message);
            Listen();
        }

        private  void SendBtnClick(object sender, RoutedEventArgs e)
        {
            
            string message = msgText.Text;
            SendMessage(message);
            //MessageBox.Show("kkjsdhjsdg", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

        }
        private async void SendMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                byte[] date = Encoding.UTF8.GetBytes(message);
                await client.SendAsync(date, serverEndPoint);
            }
        }
        private async void Listen()
        {
            while (true)
            {
                var data = await client.ReceiveAsync();
                string mess = Encoding.UTF8.GetString(data.Buffer);
                messages.Add(new MessageInfo(mess, DateTime.Now));
            }
            
        }
      
    }
}
