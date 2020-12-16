using System.Collections.Generic;
using IIMes.Services.Runtime.Model.Spec;

namespace IIMes.Services.Runtime.Model.Process
{
    public class SpecManager
    {
        private readonly IEnumerable<ICollectionSpecBuilder> _collectionSpecBiulders;
        private readonly IEnumerable<IExecutionSpecBuilder> _executionSpecBiulders;

        public SpecManager(IEnumerable<ICollectionSpecBuilder> collectionSpecBiulders, IEnumerable<IExecutionSpecBuilder> executionSpecBiulders)
        {
            _collectionSpecBiulders = collectionSpecBiulders;
            _executionSpecBiulders = executionSpecBiulders;
        }
        public IList<ICollectionSpec> GetCollectionSpec(int terminalId, Bop bop)
        {
            var ret = new List<ICollectionSpec>();
            foreach (ICollectionSpecBuilder builder in _collectionSpecBiulders)
            {
                var spec = builder.Build(terminalId, bop);
                if (spec != null)
                {
                    ret.Add(spec);
                }
            }
            return ret;
        }
        public ICollectionSpec GetCollectionSpec(string specType, int terminalId, Bop bop)
        {
            foreach (var builder in _collectionSpecBiulders)
            {
                if (builder.GetType().Name.StartsWith(specType))
                {
                    var spec = builder.Build(terminalId, bop);
                    return spec;
                }
            }
            return null;
        }
        public IList<IExecutionSpec> GetExecutionSpec(int terminalId, Bop bop)
        {
            var ret = new List<IExecutionSpec>();
            foreach (IExecutionSpecBuilder builder in _executionSpecBiulders)
            {
                var spec = builder.Build(terminalId, bop);
                if (spec != null)
                {
                    ret.Add(spec);
                }
            }
            return ret;
        }
        public IExecutionSpec GetExecutionSpec(string specType, int terminalId, Bop bop)
        {
            foreach (var builder in _executionSpecBiulders)
            {
                if (builder.GetType().Name.StartsWith(specType))
                {
                    var spec = builder.Build(terminalId, bop);
                    return spec;
                }
            }
            return null;
        }
    }
}