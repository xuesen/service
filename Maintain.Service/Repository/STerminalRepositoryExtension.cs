using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Core.Model;

namespace IIMes.Services.Maintain.Repository
{
    public static class STerminalRepositoryExtension
    {
        public static STerminal GetTerminal(this IRepository<STerminal> repository, string terminalcode)
        {
            return repository.Query().Where(p => p.Code == terminalcode).FirstOrDefault();
        }
    }
}
