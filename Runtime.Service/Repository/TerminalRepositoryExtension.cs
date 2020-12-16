using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Plant;

namespace IIMes.Services.Runtime.Repository
{
    public static class TerminalRepositoryExtension
    {
        public static Terminal GetTerminal(this IRepository<Terminal> repository, string terminalcode)
        {
            return repository.Query().Where(p => p.Code == terminalcode).FirstOrDefault();
        }

        public static Terminal GetTerminalByProcessCode(this IRepository<Terminal> repository, string processcode)
        {
            return repository.Query().Where(p => p.Process.Code == processcode).FirstOrDefault();
        }
    }
}
