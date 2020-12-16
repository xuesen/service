using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IIMes.Services.Generate.Algorithm
{
    public interface IMathSequenceWithCarryNumberRule
    {
        string IncreaseToNumber(string numberStart, int count);

        IList<string> GetRage(string numberStart, int count);

        long CalculateDifference(string numberStart, string numberEnd);

        int iBits { get; }

        string MaxNumber { get; }

        string MinNumber { get; }
    }
}
