using IIMes.Infrastructure.DomainObject;

namespace IIMes.Infrastructure.Exception
{
    public class ExceptionMessage : DomainObject.DomainObject
    {
        public virtual string Code { get; set; }

        public virtual string Message { get; set; }

        public virtual string AppName { get; set; }

        public virtual string LanguageCode { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var t = obj as ExceptionMessage;
            if (t == null) return false;
            if (Code == t.Code
             && AppName == t.AppName
             && LanguageCode == t.LanguageCode)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hash = GetType().GetHashCode();
            hash = (hash * 397) ^ Code.GetHashCode();
            if (AppName != null)
                hash = (hash * 397) ^ AppName.GetHashCode();
            if (LanguageCode != null)
                hash = (hash * 397) ^ LanguageCode.GetHashCode();

            return hash;
        }
    }
}
