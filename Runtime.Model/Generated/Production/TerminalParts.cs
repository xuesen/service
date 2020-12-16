using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.Production
{
    public partial class TerminalParts : DomainObject, IAggregateRoot
    {
        public virtual int TerminalId { get; set; }

        public virtual int? EquipmentId { get; set; }

        public virtual int StationId { get; set; }

        public virtual string Station { get; set; }

        public virtual int Sequence { get; set; }

        public virtual int ItemPartId { get; set; }

        public virtual string ItemPartNo { get; set; }

        public virtual int ScanPartId { get; set; }

        public virtual string ScanPartNo { get; set; }

        public virtual string ScanNo { get; set; }
    }
}
