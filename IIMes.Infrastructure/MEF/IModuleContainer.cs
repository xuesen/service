using System;
using System.Collections.Generic;

namespace IIMes.Infrastructure.MEF
{
    public interface IModuleContainer
    {
        IList<T> GetModule<T>(IList<KeyValuePair<string, string>> conditions, string type)
            where T : IMEFModule;

        T CreateModule<T>(IList<KeyValuePair<string, string>> conditions, string type)
            where T : IMEFModule;

        IList<Tuple<T, int>> GetModuleAndPriority<T>(IList<KeyValuePair<string, string>> conditions, string type)
            where T : IMEFModule;
    }
}
