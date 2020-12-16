using System.Collections.Generic;

namespace IIMes.Services.Runtime.Model.Spec
{
    public abstract class BaseExecutionSpec
    {
        protected abstract object BeforeExecute(int termialId, string container, object data);
        protected abstract object AfterExecute(int termialId, string container, object data);
        public virtual object Execute(int termialId, string container, object data)
        {
            var ret = new List<object>();
            BeforeExecute(termialId , container, data);
            Execute(termialId , container, data);
            AfterExecute(termialId , container, data);
            return ret;
        }
    }
}

