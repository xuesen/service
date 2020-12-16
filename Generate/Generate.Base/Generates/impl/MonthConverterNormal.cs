using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IIMes.Services.Generate.impl
{
    public class MonthConverterNormal : intf.AbsMonthConverter
    {
        public MonthConverterNormal(int bit, char padChar, string fieldKey)
            : base(bit, padChar, fieldKey)
        {

        }

        public override string Convert(object obj)
        {
              var dt = GetValue(obj);
              string sMonth = dt.Month.ToString();
              return base.Convert(sMonth);

              #region
              //Month	Month Code
              // 1	    01
              // 2	    02
              // 3	    03
              // 4	    04
              // 5	    05
              // 6	    06
              // 7	    07
              // 8	    08
              // 9	    09
              //10        10
              //11        11
              //12        12
              #endregion
        }
    }
}
