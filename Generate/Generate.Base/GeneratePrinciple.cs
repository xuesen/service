using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IIMes.Services.Generate
{
    public abstract class AbsGeneratePrinciple : IGeneratePrinciple, IValueProvider
    {
        public virtual int Qty { get; set; }
        public virtual string[] IntermediaResult { get; set; }
        public virtual IGeneratingResult Result { get; set; }

        Hashtable _values = new Hashtable();

        public virtual object GetValue(string key)
        {
            if (_values.ContainsKey(key))
                return _values[key];
            //针对找不到value的key,前后缀无法放到GeneratesConstants.ValueKey中的值，2019.6.1
            return key;
        }

        public virtual void AddValue(string key, object value)
        {
            if (_values.ContainsKey(key))
                _values[key] = value;
            else
                _values.Add(key, value);
        }

        public virtual void RemoveValue(string key)
        {
            if (_values.ContainsKey(key))
                _values.Remove(key);
        }

        public abstract ISubGeneratingChannel Current
        {
            get;
        }

        public virtual SNComposer Composer
        {
            get
            {
                return (SNComposer)GetValue(SNComposer.GlobalName);
            }
        }
    }
}
