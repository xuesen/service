using IIMes.Infrastructure.DomainObject;

namespace Generate.Model
{
    public partial class GenerateLabelSerial : DomainObject, IAggregateRoot
    {
        public virtual string Type { get; set; }

        public virtual int RuleId { get; set; }

        public virtual string SerialSno { get; set; }
    }
}
