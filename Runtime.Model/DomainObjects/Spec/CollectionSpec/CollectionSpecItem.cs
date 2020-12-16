using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Product;

namespace IIMes.Services.Runtime.Model.Process
{
    public class CollectionSpecItem : DomainObject, ICollectionSpecItem
    {
        // public virtual int Id { get; set}
        public virtual int Qty { get; set; }
        public virtual string Location { get; set; }
        public virtual string Station { get; set; }
        public virtual string FeederType { get; set; }
        public virtual string LeftAmount { get; set; }

        public virtual IList<ICollectionSpecItemCandidate> Candidates { get; set; }

        public virtual IList<ICollectionSpecItemValue> Values { get; set; }

        public virtual int MainCandidateId{get; set;}

        public virtual ICollectionSpecItemCandidate MainCandidate { get { return Candidates.Where(o => o.Id == MainCandidateId).FirstOrDefault(); } }
        
    }
}

