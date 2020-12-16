using System;

namespace IIMes.Services.Core.Model
{
    public class ModuleId
    {
        public virtual string Name { get; set; }

        public virtual string Type { get; set; }

        public String GetName()
        {
            return this.Name;
        }

        public new String GetType()
        {
            return this.Type;
        }

        public override bool Equals(object other)
        {
            if (this == other)
                return true;
            ModuleId rhs = other as ModuleId;
            if (rhs == null)
                return false; // null or not a ModuleId
            return Name.Equals(rhs.Name) && Type.Equals(rhs.Type);
        }

        public override int GetHashCode()
        {
            return GetName().GetHashCode() ^ GetType().GetHashCode();
            // return Name.GetHashCode() ^ Type.GetHashCode();
        }

        public override string ToString()
        {
            return Name + "|" + Type;
        }
    }
}
