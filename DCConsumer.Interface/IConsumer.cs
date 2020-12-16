using System;
using System.Threading;

namespace Consumer.Interface
{
    public interface IConsumer : IDisposable
    {
        void StartConsume(CancellationToken stoppingToken);
    }
}
