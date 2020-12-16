using System;
using System.Collections.Generic;
using IIMes.Services.Generate;
using IIMes.Services.Generate.Algorithm;

namespace Generate.Interface
{
    public abstract class BaseSubGenerate : ISubGeneratingChannel
    {
        protected BaseSubGenerate(string type)
        {
            Type = type;
        }

        public abstract IConverter[] Converters(AbsGeneratePrinciple context);

        public string Type { get; private set; }

        //public string GetMaxNumber();

        public string TypeStrip
        {
            get { return StripPrefixBeforeUnderline(Type); }
        }

        private static string StripPrefixBeforeUnderline(string orig)
        {
            int iSta = orig.IndexOf("_", StringComparison.Ordinal);
            if (iSta > -1)
                return orig.Substring(iSta + 1);
            return orig;
        }

        public virtual string[] UnlikeSuffixArr
        {
            get { return new string[]{ }; }
        }

        public virtual string TypeInDB
        {
            get { return Type; }
        }

        protected static SequencialNumberSegment[] ComposeShield(char[] pre, string postBeg, string postEnd, IMathSequenceWithCarryNumberRule rule)
        {
            var ret = new List<SequencialNumberSegment>();
            foreach (var unused in pre)
            {
                ret.Add(new SequencialNumberSegment(rule) { NumberBegin = pre + postBeg, NumberEnd = pre + postEnd });
            }
            return ret.ToArray();
        }
    }
}
