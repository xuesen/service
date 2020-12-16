namespace IIMes.Infrastructure.Exception
{
    public interface IMessageProvider
    {
        string GetMessage(string errCode);
    }
}
