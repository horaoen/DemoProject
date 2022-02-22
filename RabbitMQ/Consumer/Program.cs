using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var connFactory = new ConnectionFactory();
connFactory.HostName = "127.0.0.1";
connFactory.DispatchConsumersAsync = true;
var connection = connFactory.CreateConnection();
var exchangeName = "exchange1";
var queueName = "queue1";
var routingKey = "key1";
using var channel = connection.CreateModel();
channel.ExchangeDeclare(exchangeName, "direct");
channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queueName, exchangeName, routingKey);

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.Received += Consumer_Received;
channel.BasicConsume(queueName, autoAck: false, consumer: consumer);
System.Console.WriteLine("按回车键推出");
System.Console.ReadLine();
async Task Consumer_Received(object sender, BasicDeliverEventArgs _event)
{
    try 
    {
        byte[] bytes = _event.Body.ToArray();
        string text = Encoding.UTF8.GetString(bytes);
        System.Console.WriteLine(DateTime.Now.ToString() + "收到消息:" + text);
        channel.BasicAck(_event.DeliveryTag, multiple: false);
        await Task.Delay(1000);
    }
    catch(Exception exception) 
    {
        System.Console.WriteLine(exception);
        channel.BasicReject(_event.DeliveryTag, true);
    } 

}