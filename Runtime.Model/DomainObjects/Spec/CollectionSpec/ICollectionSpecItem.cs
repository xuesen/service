using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Process
{
    public interface ICollectionSpecItem
    {
        /// <summary>
        /// bop_process_bom.id
        /// </summary>
        /// <value></value>
        int Id { get; set; }

        /// <summary>
        /// bop_process_bom.qty
        /// </summary>
        /// <value></value>
        int Qty { get; set; }

        /// <summary>
        /// bop_process_bom.item_part_id
        /// </summary>
        /// <value></value>
        int MainCandidateId { get; set; }

        IList<ICollectionSpecItemCandidate> Candidates { get; set; }

        ICollectionSpecItemCandidate MainCandidate { get; }

        IList<ICollectionSpecItemValue> Values { get; set; }
    }
}

