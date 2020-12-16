namespace IIMes.Services.Runtime.Model.Process
{
    public interface ICollectionSpecBuilder
    {
        ICollectionSpec Build(int terminalId, Bop bop);
    }
}