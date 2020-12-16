using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IIMes.Services.Generate.intf;

namespace IIMes.Services.Generate.intf
{
    public abstract class AbsMonthConverter : AbsTimeConverter
    {
        public AbsMonthConverter(int bit, char padChar, string fieldKey)
            : base(bit, padChar, fieldKey)
        {

        }

    }
}
