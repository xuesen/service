using System.Collections.Generic;

namespace IIMes.Services.Runtime.Model.Spec
{
    public interface IExecutionSpec
    {
        object Execute(int termialId, string container, object data);
    }
}

