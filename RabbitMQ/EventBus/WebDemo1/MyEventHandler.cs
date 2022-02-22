using Zack.EventBus;

namespace WebDemo1
{
    public class MyEventHandler : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string eventData)
        {
            throw new NotImplementedException();
        }
    }
}