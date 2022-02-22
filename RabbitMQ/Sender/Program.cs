using System.Text;
using RabbitMQ.Client;

var connFactory = new ConnectionFactory();
connFactory.HostName = "127.0.0.1";
connFactory.DispatchConsumersAsync = true;
var connection = connFactory.CreateConnection();
var exchangName = "exchange1";
while(true)
{
    using var channel = connection.CreateModel();
    var prop = channel.CreateBasicProperties();
    prop.DeliveryMode = 2;
    channel.ExchangeDeclare("exchange1", "direct");
    byte[] bytes = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
    channel.BasicPublish(exchangName, routingKey: "key1", mandatory: true, basicProperties: prop, body: bytes);
    System.Console.WriteLine("ok" + DateTime.Now.ToString());
    Thread.Sleep(1000);
}