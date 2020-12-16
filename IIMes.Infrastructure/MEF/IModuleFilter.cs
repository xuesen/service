using System.Collections.Generic;

namespace IIMes.Infrastructure.MEF
{
    public interface IModuleFilter
    {
        IDictionary<string, int> Filt(IList<KeyValuePair<string, string>> conditions, string type);
    }
}
