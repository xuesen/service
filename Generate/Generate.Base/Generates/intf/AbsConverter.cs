
using IIMes.Infrastructure.Exception;

namespace IIMes.Services.Generate.intf
{
    public abstract class AbsConverter<T> : IConverter
    {
        protected int _maxBit = 1;
        protected int _minBit = 0;
        protected char _padChar = '0';
        protected string _fieldKey = null;

        protected AbsConverter(int maxBit, char padChar, string fieldKey, int minBit = 0)
        {
            if (maxBit < 1)
                throw new BizException("GEN024");
            _maxBit = maxBit;
            _minBit = minBit;
            _padChar = padChar;
            _fieldKey = fieldKey;
        }

        public virtual string Convert(object obj)
        {
            string sobj = obj.ToString();
            if (_maxBit != int.MaxValue)
            {
                if (_maxBit <= sobj.Length)
                    return sobj.Substring(sobj.Length - _maxBit);
                else
                    return sobj.PadLeft(_maxBit, _padChar);
            }
            else
            {
                if (_minBit > sobj.Length)
                    return sobj.PadLeft(_minBit, _padChar);
                else
                    return sobj;
            }
        }

        public virtual bool IsHaveDefautValue()
        {
            return false;
        }

        public virtual object GetDefautValue()
        {
            return null;
        }

        protected T GetValue(object obj)
        {
            if (obj is IValueProvider)
            {
                return (T)((IValueProvider)obj).GetValue(_fieldKey);
            }
            else
                return default(T);
        }
    }
}
