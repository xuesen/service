using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Core.Model
{
    public partial class LabelSerial : DomainObject, IAggregateRoot
    {
        public virtual string Type { get; set; }

        public virtual int RuleId { get; set; }

        public virtual string SerialSno { get; set; }
    }
}
