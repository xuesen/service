using System;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class WoBom : DomainObject
    {
        public virtual int WoBaseId { get; set; }

        public virtual WoBase WoBase { get; set; }

        public virtual int ProcessId { get; set; }

        public virtual SProcess SProcess { get; set; }

        public virtual int MainPartId { get; set; }

        public virtual SPart SMainPart { get; set; }

        public virtual int SubPartId { get; set; }

        public virtual SPart SSubPart { get; set; }

        public virtual int? SubPartQty { get; set; }

        public virtual string SubPartGroup { get; set; }

        public virtual string Version { get; set; }

        public virtual string Location { get; set; }
    }
}
