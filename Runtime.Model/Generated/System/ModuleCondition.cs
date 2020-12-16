using IIMes.Infrastructure.DomainObject;

namespace IIMes.Services.Runtime.Model.System
{
    public partial class ModuleCondition : DomainObject, IAggregateRoot
    {
        public virtual string Plant { get; set; }

        public virtual string Customer { get; set; }

        public virtual string Family { get; set; }

        public virtual string Model { get; set; }

        public virtual string Site { get; set; }

        public virtual string Process { get; set; }

        public virtual string Wc { get; set; }

        public virtual string Line { get; set; }

        public virtual string Module { get; set; }

        public virtual string ModuleType { get; set; }

        public string GetValue(string name)
        {
            switch (name)
            {
                case "Plant":
                        return Plant;
                case "Customer":
                        return Customer;
                case "Family":
                        return Family;
                case "Model":
                        return Model;
                case "Site":
                        return Site;
                case "Process":
                        return Process;
                case "Wc":
                        return Wc;
                case "Line":
                        return Line;
            }

            return string.Empty;
        }
    }
}
