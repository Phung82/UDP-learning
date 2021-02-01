using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Yeu cau nguoi dung nhap ip cuar server
            Console.Write("Server IP address");
            var serverIpStr = Console.ReadLine();
            //chuyen chuoi ky tu thanh Ob thuoc kieu IP
            var serverIp = IPAddress.Parse(serverIpStr);
            //yeu cau nguoi dung nhap cong server
            Console.Write("Server port:");
            //chuyen kieu ky tu thanh int
            var serverPortStr = Console.ReadLine();
            var serverPort = int.Parse(serverPortStr);

            var serverEndPoint = new IPEndPoint(serverIp, serverPort);

            //kich thuoc bo nho diem
            var size = 1024;
            var receiBuffer = new byte[size];
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("# Text >> ");
                Console.ResetColor();
                var text = Console.ReadLine();

                //khoi tao Ob cua lop socket su dung Udp
                var socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
                //bien chuoi thanh mang byte
                var sendBuffer = Encoding.ASCII.GetBytes(text);
                socket.SendTo(sendBuffer, serverEndPoint);
                // endpoint dung de nhan du lieu
                EndPoint dummyEndPoint = new IPEndPoint(IPAddress.Any, 0);

                var length = socket.ReceiveFrom(receiBuffer, ref dummyEndPoint);
                var result = Encoding.ASCII.GetString(receiBuffer, 0, length);
                //xoa bo nho dem
                Array.Clear(receiBuffer, 0, size);
                //dong socket
                socket.Close();
                //in ket qua ra man hinh
                Console.WriteLine($" >> {result}");
            }
    }
}
