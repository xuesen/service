using System;
using IIMes.Services.Generate;

namespace Generate.Interface
{
    public class GeneratePrinciple : AbsGeneratePrinciple//, IReferedBox
    {
        public override ISubGeneratingChannel Current
        {
            get { throw new NotImplementedException(); }
        }

        private const string Prefix = "MemMaxSN_";

        public string GetMaxSN(string key)
        {
            return (string)GetValue(Prefix + key);
        }

        public void SetMaxSN(string key, string sn)
        {
            AddValue(Prefix + key, sn);
        }
    }
}
