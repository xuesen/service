using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IIMes.Services.Generate.Algorithm;

namespace IIMes.Services.Generate.impl
{
    public class WeekConverterNormal : intf.AbsWeekConverter
    {
        public WeekConverterNormal(int bit, char padChar, string fieldKey, string weekRule)
            : base(bit, padChar, fieldKey, weekRule)
        {

        }

        public override string Convert(object obj)
        {
            var dt = GetValue(obj);
            WeekRuleResult wrr = WeekRuleEngine.Calculate(_weekRule, dt);
            if (wrr != null)
            {
                var sWeek = wrr.Week.ToString();
                return base.Convert(sWeek);
            }
            else
                return null;
        }

    }
}
