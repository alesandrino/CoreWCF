using Contract;

namespace NetCoreWithDIServer
{

    public class EchoService : Contract.IEchoService
    {
        private readonly IMessageQueue _messageQueue;

        public EchoService(IMessageQueue messageQueue)
        {
            _messageQueue = messageQueue;
        }

        public string Echo(string text)
        {
            System.Console.WriteLine($"Received {text} from client!");
            string message = _messageQueue.Dequeue();
            System.Console.WriteLine($"The message before was {message}!");
            _messageQueue.Enqueue(text);
            return text;
        }

        public string ComplexEcho(EchoMessage text)
        {
            System.Console.WriteLine($"Received {text.Text} from client!");
            string message = _messageQueue.Dequeue();
            System.Console.WriteLine($"The message before was {message}!");
            _messageQueue.Enqueue(text.Text);
            return text.Text;
        }
    }
}
