using System.ComponentModel.Composition;
using System;
using System.Data;
using Newtonsoft.Json;
using Generate.Interface;
using System.Linq;
using System.Collections.Generic;
using IIMes.Infrastructure;
using IIMes.Infrastructure.Hibernate;
using IIMes.Services.Generate;
using IIMes.Services.Generate.impl;
using IIMes.Infrastructure.Hibernate.Data;
using IIMes.Infrastructure.Data.Interface;
using Generate.Model;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace Generate.Barcode
{
    public class GenerateModule : BaseGenerate
    {
        public class BarcodeContext
        {
            public string SerialLength { get; set; }
            public string Step { get; set; }
            public string SerialType { get; set; }
            public string CustomizeInfo { get; set; }
            public string Startnumber { get; set; }
            public string Reset { get; set; }

        }

        private readonly IRepository<GenerateBarcodeRule> _rep;

        public GenerateModule(
                                IRepository<GenerateBarcodeRule> rep)
        {
            //Type = GeneratingType.Barcode;
            _rep = rep;
            SubTypes =
            new BaseSubGenerate[]
            {
                new Barcode()
            };
        }

        protected override PlainSNComposerEvent GetWait4SequenceCompute()
        {
            return Wait4SequenceCompute;
        }

        protected override PlainSNComposerEvent GetWhileSequenceCompute()
        {
            return WhileSequenceCompute;
        }

        void Wait4SequenceCompute(SNComposer cmp, NumberElement currNe, AbsGeneratePrinciple context)
        {
        }

        new void WhileSequenceCompute(SNComposer cmp, NumberElement currNe, AbsGeneratePrinciple context)
        {
            base.WhileSequenceCompute(cmp, currNe, context);
        }

        public override AbsGeneratePrinciple Prepare(int ruleid, int qty, string workorder, string partno)
        {
            var input = new GeneratePrinciple();
            var res = _rep.Query().Where(i => i.Id == ruleid).FirstOrDefault();
            if (res != null)
            {
                BarcodeContext barcodeInfo = JsonConvert.DeserializeObject<BarcodeContext>(res.SerialJson);

                input.AddValue(GeneratesConstants.ValueKey.SerialLength, barcodeInfo.SerialLength);
                input.AddValue(GeneratesConstants.ValueKey.Step, barcodeInfo.Step);
                input.AddValue(GeneratesConstants.ValueKey.SerialType, barcodeInfo.SerialType);
                input.AddValue(GeneratesConstants.ValueKey.CustomizeInfo, barcodeInfo.CustomizeInfo);
                input.AddValue(GeneratesConstants.ValueKey.Startnumber, barcodeInfo.Startnumber);
                input.AddValue(GeneratesConstants.ValueKey.Reset, barcodeInfo.Reset);
                input.AddValue(GeneratesConstants.ValueKey.Prefix, res.Prefix);
                input.AddValue(GeneratesConstants.ValueKey.Suffix, res.Suffix);
                //SP
                if (res.Prefix.IndexOf("<") != -1)
                {
                    string strPrefix = res.Prefix.Substring(1, res.Prefix.Length - 2);
                    string strprefix = ConverParamsbyCallSP(strPrefix, workorder, partno);
                    input.AddValue(GeneratesConstants.ValueKey.PrefixSP, strprefix);
                }
                if (res.Suffix.IndexOf("<") != -1)
                {
                    string strSuffix = res.Suffix.Substring(1, res.Suffix.Length - 2);
                    string strsuffix = ConverParamsbyCallSP(strSuffix, workorder, partno);
                    input.AddValue(GeneratesConstants.ValueKey.SuffixSP, strsuffix);
                }

                input.AddValue(GeneratesConstants.ValueKey.DateTime, DateTime.Now);
                input.AddValue(GeneratesConstants.ValueKey.SubType, GeneratesConstants.SubTypes.Barcode);


                input.AddValue(GeneratesConstants.ValueKey.Rule, ruleid);
                input.AddValue(GeneratesConstants.ValueKey.Qty, qty);

                input.Qty = Convert.ToInt32(barcodeInfo.Step);
            }

            return input;
        }


        //前缀和后缀支持存储过程设定
        private string ConverParamsbyCallSP(string spname, string workorder, string partno)
        {
            var ret = "";
            var rep = _rep;

            //modify ITC-1744-0060
            var paramsArray = new object[2] { workorder, partno };
            var param = new string[2];
            param[0] = workorder;
            param[1] = partno;
            // 暂时不支持SP调用， 2020.9.8
            // var res = rep.CallSPForQueryNoParamName(spname, param);
            // if (res != null && res.Rows != null && res.Rows.Count > 0)
            // {
            //     foreach (DataRow row in res.Rows)
            //     {
            //         ret = row[0].ToString();
            //     }
            // }
            return ret;
        }

    }

    class Barcode : BaseSubGenerate
    {
        public Barcode()
            : base(GeneratesConstants.SubTypes.Barcode)
        {

        }
/*        
        protected static readonly IConverter[] ConverterList =
            new IConverter[]
            {
                new CommonConverter(int.MaxValue, '0', GeneratesConstants.ValueKey.Prefix),
                new SequenceConverterNormal("0123456789ABCDEFGHJKLMNPRSTVWXYZ", 3, "ZZZ", "000", '0'),
                new CommonConverter(int.MaxValue, '0', GeneratesConstants.ValueKey.Suffix)
            };

        public override IConverter[] Converters(AbsGeneratePrinciple context)
        {
            string prefix = (string)context.GetValue(GeneratesConstants.ValueKey.Prefix);

            return ConverterList;
        }
*/

        public override IConverter[] Converters(AbsGeneratePrinciple context)
        {

            IConverter[] _converters = new IConverter[0]; 
            IConverter[] nullList = new IConverter[0]; 
            List<IConverter> temp = nullList.ToList();

            string prefix = (string)context.GetValue(GeneratesConstants.ValueKey.Prefix);
            string suffix = (string)context.GetValue(GeneratesConstants.ValueKey.Suffix);
            int serialLength = Convert.ToInt32(context.GetValue(GeneratesConstants.ValueKey.SerialLength));
            string serialType = (string)context.GetValue(GeneratesConstants.ValueKey.SerialType);
            string startNumber = (string)context.GetValue(GeneratesConstants.ValueKey.Startnumber);
            string customizeInfo = (string)context.GetValue(GeneratesConstants.ValueKey.CustomizeInfo);

            //modify ITC-1744-0151
            if (prefix.Length >= 1)
            {
                //SP
                if (prefix.IndexOf("<") != -1)
                {
                    temp.Add(new CommonConverter(int.MaxValue, '0', GeneratesConstants.ValueKey.PrefixSP));
                }
                else
                {
                    string[] sPrefixArray = prefix.Split('+');
                    for (int i = 0; i < sPrefixArray.Length; i++)
                    {
                        if (sPrefixArray[i] == "YY")
                        {
                            temp.Add(new YearConverterNormal(2, '0', GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sPrefixArray[i] == "YYYY")
                        {
                            temp.Add(new YearConverterNormal(4, '0', GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sPrefixArray[i] == "M")
                        {
                            temp.Add(new MonthConverterOneBitCode(GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sPrefixArray[i] == "MM")
                        {
                            temp.Add(new MonthConverterNormal(2, '0', GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sPrefixArray[i] == "D")
                        {
                            temp.Add(new DayConverterOneBitCode("123456789ABCDEFGHIJKLMNOPQRSTUV", GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sPrefixArray[i] == "DD")
                        {
                            temp.Add(new DayConverterNormal(2, '0', GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sPrefixArray[i] == "W")
                        {
                            temp.Add(new WeekConverterNormal(1, '0', GeneratesConstants.ValueKey.DateTime, "01X"));
                        }
                        else if (sPrefixArray[i] == "WW")
                        {
                            temp.Add(new WeekConverterNormal(2, '0', GeneratesConstants.ValueKey.DateTime, "01X"));
                        }
                        else
                        {
                            if (sPrefixArray[i].IndexOf("\"") != -1)
                            {
                                sPrefixArray[i] = sPrefixArray[i].Replace("\"", "");
                                temp.Add(new CommonConverter(int.MaxValue, '0', sPrefixArray[i]));
                            }
                            
                        }
                    }
                }
            }
            if (serialLength > 0)
            {
                string maxstr = "";
                string minstr = "";
                if (serialType == "Decimal")
                {
                    string startnum = startNumber;
                    if (startnum == "")
                    {
                        //modify ITC-1744-0029
                        for (int j = 0; j < serialLength - 1; j++)
                        {
                            minstr += "0";
                        }
                        minstr += "1";
                    }
                    else
                    {
                        minstr = startnum;
                    }

                    for (int j = 0; j < serialLength; j++)
                    {
                        maxstr += "9";
                    }

                    temp.Add(new SequenceConverterNormal("0123456789", serialLength, maxstr, minstr, '0'));
                }
                else if (serialType == "Hexadecima")
                {
                    string startnum = startNumber;
                    if (startnum == "")
                    {
                        for (int j = 0; j < serialLength - 1; j++)
                        {
                            minstr += "0";
                        }
                        minstr += "1";
                    }
                    else
                    {
                        minstr = startnum;
                    }

                    for (int j = 0; j < serialLength; j++)
                    {
                        maxstr += "F";
                    }
                    temp.Add(new SequenceConverterNormal("0123456789ABCDEF", serialLength, maxstr, minstr, '0'));

                }
                else if (serialType == "Customize")
                {
                    string startnum = startNumber;
                    if (startnum == "")
                    {
                        string startno = customizeInfo.Substring(0, 1);

                        for (int j = 0; j < serialLength; j++)
                        {
                            minstr += startno;
                        }
                    }
                    else
                    {
                        minstr = startnum;
                    }

                    for (int j = 0; j < serialLength; j++)
                    {
                        maxstr += customizeInfo.Substring(customizeInfo.Length - 1, 1);
                    }
                    temp.Add(new SequenceConverterNormal(customizeInfo, serialLength, maxstr, minstr, '0'));
                }
            }
            if (suffix.Length >= 1)
            {
                //SP
                if (suffix.IndexOf("<") != -1)
                {
                    temp.Add(new CommonConverter(int.MaxValue, '0', GeneratesConstants.ValueKey.SuffixSP));
                }
                else
                {
                    string[] sSuffixArray = suffix.Split('+');
                    for (int i = 0; i < sSuffixArray.Length; i++)
                    {
                        if (sSuffixArray[i] == "YY")
                        {
                            temp.Add(new YearConverterNormal(2, '0', GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sSuffixArray[i] == "YYYY")
                        {
                            temp.Add(new YearConverterNormal(4, '0', GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sSuffixArray[i] == "M")
                        {
                            temp.Add(new MonthConverterOneBitCode(GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sSuffixArray[i] == "MM")
                        {
                            temp.Add(new MonthConverterNormal(2, '0', GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sSuffixArray[i] == "D")
                        {
                            temp.Add(new DayConverterOneBitCode("123456789ABCDEFGHIJKLMNOPQRSTUV", GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sSuffixArray[i] == "DD")
                        {
                            temp.Add(new DayConverterNormal(2, '0', GeneratesConstants.ValueKey.DateTime));
                        }
                        else if (sSuffixArray[i] == "W")
                        {
                            temp.Add(new WeekConverterNormal(1, '0', GeneratesConstants.ValueKey.DateTime, "01X"));
                        }
                        else if (sSuffixArray[i] == "WW")
                        {
                            temp.Add(new WeekConverterNormal(2, '0', GeneratesConstants.ValueKey.DateTime, "01X"));
                        }
                        else
                        {
                            if (sSuffixArray[i].IndexOf("\"") != -1)
                            {
                                sSuffixArray[i] = sSuffixArray[i].Replace("\"", "");
                                temp.Add(new CommonConverter(int.MaxValue, '0', sSuffixArray[i]));
                            }
                            
                        }
                    }
                }

            }
            _converters = temp.ToArray();

            return _converters;
        }

        public override string TypeInDB
        {
            get { return "Barcode"; }
        }
    }

}
