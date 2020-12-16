using System;

namespace IIMes.Services.Generate.intf
{
    public abstract class AbsTimeConverter : AbsConverter<DateTime>
    {
        public AbsTimeConverter(int bit, char padChar, string fieldKey)
            : base(bit, padChar, fieldKey)
        {

        }
    }
}
