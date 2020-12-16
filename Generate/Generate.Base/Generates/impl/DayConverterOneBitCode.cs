using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IIMes.Services.Generate.impl
{
    public class DayConverterOneBitCode : intf.AbsDayConverter
    {
        private char[] _rule;

        public DayConverterOneBitCode(string rule, string fieldKey)
            : base(1, '0', fieldKey)
        {
            _rule = rule.ToCharArray();
        }

        public override string Convert(object obj)
        {
            //ex.
            //D：一位日，123456789A BCDEFGHJKL MNPRSTVWXY Z
            //D：一位日，123456789A BCDEFGHIJK LMNOPQRSTU V

            var dt = GetValue(obj);
            return System.Convert.ToString(_rule[dt.Day - 1]);
        }

    }
}
