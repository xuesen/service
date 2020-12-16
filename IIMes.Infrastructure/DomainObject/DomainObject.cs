using System;

namespace IIMes.Infrastructure.DomainObject
{
    public abstract class DomainObject : GenericDomainObject<int>
    {
        public static string GetStr(string str, string title, string endstr)
        {
            if (str == null)
                str = string.Empty;
            str = str.Trim();
            title = title.Trim();
            endstr = endstr.Trim();

            if (str.IndexOf(title, StringComparison.Ordinal) < 0 || str.IndexOf(endstr, StringComparison.Ordinal) < 0)
            {
                return string.Empty;
            }
            else
            {
                var beg = str.IndexOf(title, StringComparison.Ordinal) + title.Length;
                var end = str.IndexOf(endstr, beg, StringComparison.Ordinal);
                return str[beg..end];
            }
        }

        public static string GetINIValue(string str, string key)
        {
            return GetStr(str, key + "=", "~");
        }

        public static string GetXmlValue(string xmlString, string tagName)
        {
            string begin = "<" + tagName + ">";
            string end = "</" + tagName + ">";
            return GetStr(xmlString, begin, end);
        }
    }
}
