using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IIMes.Services.Generate.impl
{
    public class DayConverterNormal : intf.AbsDayConverter
    {
        public DayConverterNormal(int bit, char padChar, string fieldKey)
            : base(bit, padChar, fieldKey)
        {

        }

        public override string Convert(object obj)
        {
            var dt = GetValue(obj);
            string sDay = dt.Day.ToString();
            return base.Convert(sDay);
        }

    }
}
