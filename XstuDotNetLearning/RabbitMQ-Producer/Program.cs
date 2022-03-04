//创建连接工厂
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory
{
    UserName = "admin",
    Password = "xiongjie520",
    HostName = "localhost"
};

//创建连接
var connection = factory.CreateConnection();
//创建通道
var channel = connection.CreateModel();

//声明一个队列
channel.QueueDeclare("MockQueue", false, false, false, null);

Console.WriteLine("RabbitMQ连接成功，请输入消息，输入exit退出！");

string input;
do
{
    input = Console.ReadLine();

    var sendBytes = Encoding.UTF8.GetBytes(input);
    //发布消息
    channel.BasicPublish("", "MockQueue", null, sendBytes);

} while (input.Trim().ToLower() != "exit");
channel.Close();
connection.Close();