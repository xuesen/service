using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IIMes.Services.Generate.impl
{
    public class YearConverterNormal : intf.AbsYearConverter
    {
        public YearConverterNormal(int bit, char padChar, string fieldKey)
            : base(bit, padChar, fieldKey)
        {

        }

        public override string Convert(object obj)
        {
            var dt = GetValue(obj);
            string sYear = dt.Year.ToString();
            return base.Convert(sYear);
        }

    }
}
