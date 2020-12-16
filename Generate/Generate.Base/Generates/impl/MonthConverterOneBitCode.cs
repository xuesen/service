using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IIMes.Services.Generate.impl
{
    public class MonthConverterOneBitCode : intf.AbsMonthConverter
    {
        public MonthConverterOneBitCode(string fieldKey) : base(1, '0', fieldKey)
        {

        }

        public override string Convert(object obj)
        {
              var dt = GetValue(obj);

              #region
              //Month	Month Code
              // 1	    1
              // 2	    2
              // 3	    3
              // 4	    4
              // 5	    5
              // 6	    6
              // 7	    7
              // 8	    8
              // 9	    9
              //10        A   65
              //11        B   66
              //12        C   67
              #endregion

              if (dt.Month < 10)
                  return dt.Month.ToString();
              else
                  return System.Convert.ToChar(dt.Month + 55).ToString();
        }
    }
}
