using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IIMes.Services.Generate;

namespace IIMes.Services.Generate.impl
{
    public class CommonConverter : intf.AbsCommonConverter
    {
        public CommonConverter(int maxBit, char padChar, string fieldKey, int minBit = 0)
            : base(maxBit, padChar, fieldKey, minBit)
        {

        }

        public override string Convert(object obj)
        {
            var str = GetValue(obj);
            return base.Convert(str);
        }
    }
}
