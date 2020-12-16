using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IIMes.Services.Generate.impl
{
    public class DirectCertainValueConverter : intf.AbsCommonConverter
    {
        public DirectCertainValueConverter(string fieldKey)
            : base(1, '0', fieldKey)
        {

        }

        public override string Convert(object obj)
        {
            var str = GetValue(obj);
            return str;
        }


    }
}
