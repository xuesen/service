using Generate.Interface;
using IIMes.Services.Generate;
using System;
using System.ComponentModel.Composition;

namespace Generate.Default
{
    public class GenerateModule : BaseGenerate
    {
        public override AbsGeneratePrinciple Prepare(int ruleid, int qty, string workorder, string partno)
        {
            throw new NotImplementedException();
        }

        protected override PlainSNComposerEvent GetWait4SequenceCompute()
        {
            throw new NotImplementedException();
        }

        protected override PlainSNComposerEvent GetWhileSequenceCompute()
        {
            throw new NotImplementedException();
        }

    }
}
