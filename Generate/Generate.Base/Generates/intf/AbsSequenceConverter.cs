using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IIMes.Services.Generate;
using IIMes.Services.Generate.intf;
using IIMes.Services.Generate.Algorithm;

namespace IIMes.Services.Generate.intf
{
    public abstract class AbsSequenceConverter : AbsConverter<string>
    {
        /// <summary>
        /// 進位規則
        /// </summary>
        /// <remarks></remarks>
        protected IMathSequenceWithCarryNumberRule _mathSequenceWithCarryNumberRule = null;

        public IMathSequenceWithCarryNumberRule NumberRule
        {
            get
            {
                return _mathSequenceWithCarryNumberRule;
            }
        }

        public AbsSequenceConverter(string charCollection, int bit, string maxNumber, string minNumber, char padChar)
            : base(bit, padChar, null)
        {
            _mathSequenceWithCarryNumberRule = new MathSequenceWithCarryNumberRule(bit, charCollection, maxNumber, minNumber);
        }

        public AbsSequenceConverter(int bit, char padChar, IMathSequenceWithCarryNumberRule mathSequenceWithCarryNumberRule)
            : base(bit, padChar, null)
        {
            _mathSequenceWithCarryNumberRule = mathSequenceWithCarryNumberRule;
        }
    }
}
