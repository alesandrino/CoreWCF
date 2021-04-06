namespace NetCoreWithDIServer
{
    public interface IMessageQueue
    {
        void Enqueue(string message);
        string Dequeue();
    }
}
