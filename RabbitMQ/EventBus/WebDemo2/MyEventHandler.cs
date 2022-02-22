using Zack.EventBus;

namespace WebDemo2
{
    [EventName("OrderCreated")]
    public class MyEventHandler : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string eventData)
        {
            System.Console.WriteLine(eventData);
            return Task.CompletedTask;
        }
    }
}