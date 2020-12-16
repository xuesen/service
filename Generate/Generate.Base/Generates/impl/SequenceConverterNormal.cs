using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IIMes.Services.Generate.Algorithm;

namespace IIMes.Services.Generate.impl
{
    public class SequenceConverterNormal : intf.AbsSequenceConverter
    {
        public SequenceConverterNormal(string charCollection, int bit, string maxNumber, string minNumber, char padChar)
            : base(charCollection, bit, maxNumber, minNumber, padChar)
        {
        }

        public override string Convert(object obj)
        {
            if (obj != null && obj is string)
                return (string)obj;
            return null;
        }
    }

    public class SequenceConverterMultiBit : intf.AbsSequenceConverter
    {
        public SequenceConverterMultiBit(string[] charCollections, int bit, string maxNumber, string minNumber, char padChar)
            : base(bit, padChar, new MathSequenceWithMultiCarryNumberRule(bit, charCollections, maxNumber, minNumber))
        {
        }

        public override string Convert(object obj)
        {
            if (obj != null && obj is string)
                return (string)obj;
            return null;
        }
    }
}
