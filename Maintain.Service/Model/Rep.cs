using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Maintain.Model
{
    public class Rep : DomainObject
    {
        public virtual string Sno { get; set; }

        public virtual string Wc { get; set; }

        public virtual string Symptom { get; set; }

        public virtual string Data { get; set; }

        public virtual string Type { get; set; }

        public virtual string Status { get; set; }

        public virtual string Pdline { get; set; }

        public virtual string DiagDefect { get; set; }

        public virtual int? OriginalId { get; set; }
    }
}