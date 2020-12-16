using System.Collections.Generic;
using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.System
{
    public partial class Module : DomainObject, IAggregateRoot
    {
        public Module()
        {
            ModuleConditions = new HashSet<ModuleCondition>();
        }

        public virtual ModuleId BizKey { get; set; }

        public virtual int Priority { get; set; }

        public virtual ModuleType Type { get; set; }

        public virtual ISet<ModuleCondition> ModuleConditions { get; set; }
    }
}
