using System;
using IIMes.Infrastructure.DomainObject;
using IIMes.Services.Runtime.Model.Production;

namespace IIMes.Services.Runtime.Model.Product
{
    public partial class WoBom : DomainObject
    {
        public virtual int WoBaseId { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual Part SubPart { get; set; }

        public virtual int? SubPartQty { get; set; }

        public virtual string SubPartGroup { get; set; }

        public virtual string Version { get; set; }

        public virtual string Location { get; set; }
    }
}
