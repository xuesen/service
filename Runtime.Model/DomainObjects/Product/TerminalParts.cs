using System;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Process;

namespace IIMes.Services.Runtime.Model.Production
{
    public partial class TerminalParts : ICollectionSpecItemValue
    {
        public virtual int CandidateId { get => ScanPartId; set => ScanPartId = value; }
        public virtual string Value { get => ScanNo; set => ScanNo = value; }
        public virtual int ItemId { get => StationId; set => StationId = value; }
        public virtual int Amount { get; set; }
        public virtual int LeftAmount { get; set; }
    }
}
