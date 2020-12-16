using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Process;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class Part : ICollectionSpecItemCandidate
    {
        public virtual string MatchPattern { get => PartNo; set => PartNo = value; }
    }
}
