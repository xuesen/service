
using System.Collections.Generic;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Generate;
using Generate.Model;

namespace Generate.Interface
{
    public interface IGenerate
    { 
        List<IGeneratingResult> Generate(AbsGeneratePrinciple context, IRepository<GenerateBarcodeRule> repBarcodeRule,
                                IRepository<GenerateLabelSerial> repLabelSerial);

        AbsGeneratePrinciple Prepare(int ruleid, int qty, string workorder, string partno);

        GeneratingType Type { get; }

    }
}
