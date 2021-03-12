using System.Collections.Concurrent;

namespace NetCoreWithDIServer
{
    public class MessageQueue : IMessageQueue
    {
        private readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        public string Dequeue()
        {
            if (_queue.TryDequeue(out string message)) return message;
            return string.Empty;
        }

        public void Enqueue(string message) => _queue.Enqueue(message);
    }
}
