using IIMes.Services.Runtime.Model.Process;
namespace IIMes.Services.Runtime.Model.Spec
{
    public interface IExecutionSpecBuilder
    {
        IExecutionSpec Build(int terminalId, Bop bop);
    }
}