using System.Collections.Generic;
using System;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Services.Generate;
using IIMes.Services.Generate.intf;
using IIMes.Infrastructure.Hibernate.Data;
using Generate.Model;
using Generate.Interface;
using Generate.Barcode;

namespace Generate.Service
{
    public class GenerateBarcodeManager
    {

        private readonly IRepository<GenerateBarcodeRule> _repBarcodeRule;
        private readonly IRepository<GenerateLabelSerial> _repLabelSerial;

        public GenerateBarcodeManager(
                                IRepository<GenerateBarcodeRule> repBarcodeRule,
                                IRepository<GenerateLabelSerial> repLabelSerial)
        {
            _repBarcodeRule = repBarcodeRule;
            _repLabelSerial = repLabelSerial;
        }

        [Transactional]
        // 生成BarCode
        public IList<string> GenerateBarCode(int ruleid, int qty, string workorder, string partno)
        {

            IList<string> result = new List<string>();
            List<IGeneratingResult> igr = new List<IGeneratingResult>();
            BaseGenerate _module = new GenerateModule(_repBarcodeRule);
            var prcpl = _module.Prepare(ruleid, qty, workorder, partno);
            igr = _module.Generate(prcpl, _repBarcodeRule, _repLabelSerial);

            if (igr == null || igr.Count == 0)
                throw new BizException("GEN026");

            for (int i = 0; i < igr.Count; i++ )
            {
                string ret = "";
                List<string> strlist = new List<string>();
                if (igr[i].Result[2].IndexOf('|') != -1)
                {
                    strlist = igr[i].Result[2].Split('|').ToList();
                    //modify ITC-1744-0057
                    ret = strlist[strlist.Count - 1];                       
                }
                else
                {
                    ret = igr[i].Result[2];
                }
                
                result.Add(ret);
            }

            return result;

        }

    }
}
