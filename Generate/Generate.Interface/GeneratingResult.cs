using IIMes.Services.Generate;

namespace Generate.Interface
{
    /// <summary>
    /// Result[0]: start sn
    /// Result[1]: end sn
    /// Result[3]: all sn joined with '|'
    /// </summary>
    public class GeneratingResult : IGeneratingResult
    {
        public GeneratingResult(string[] result)
        {
            Result = result;
        }

        public string[] Result { get; private set; }
    }
}
