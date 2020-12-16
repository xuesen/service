namespace IIMes.Services.Generate
{
    public class NumberElement
    {
        public NumberElement(IConverter cvt, object obj)
        {
            _cvt = cvt;
            _obj = obj;
        }

        private IConverter _cvt = null;
        private object _obj = null;
        public IConverter Cvt
        {
            get
            {
                return _cvt;
            }
        }

        public object Obj
        {
            get
            {
                return _obj;
            }
            set
            {
                _obj = value;
            }
        }
    }
}
