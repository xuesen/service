//ITC-1652-0106
using System.Collections.Generic;
using System;
using System.Linq;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Services.Generate;
using IIMes.Services.Generate.intf;
using IIMes.Infrastructure.Hibernate.Data;
using Generate.Model;
using Generate.Interface;


namespace Generate.Interface
{
    public abstract class BaseGenerate : IGenerate
    {
        //readonly IRepository<SerialNumberIni> _repSni = RepositoryFactory.GetInstance.GetRepository<SerialNumberIni>();
        private IRepository<GenerateLabelSerial> _repLabelSerial; 

        private IRepository<GenerateBarcodeRule> _repBarcodeRule;
       
        protected BaseSubGenerate[] SubTypes = null;
        protected AbsGeneratePrinciple Context { get; set; }
        private static readonly object SyncObj = new object();

        public GeneratingType Type { get; protected set; }

        public abstract AbsGeneratePrinciple Prepare(int ruleid, int qty, string workorder, string partno);

        List<IGeneratingResult> Results = new List<IGeneratingResult>();
        List<string> maxs = new List<string>();
        Tuple<string, bool> CurrentMax = null;

        public virtual List<IGeneratingResult> Generate(AbsGeneratePrinciple context, IRepository<GenerateBarcodeRule> repBarcodeRule,
                                IRepository<GenerateLabelSerial> repLabelSerial)
        {
            _repBarcodeRule = repBarcodeRule;
            _repLabelSerial = repLabelSerial;
            GeneratingResult result;
            var len = (int)context.GetValue(GeneratesConstants.ValueKey.Qty);
            CurrentMax = GetMaxSerialNumber(GetKeyInDB((string)context.GetValue(GeneratesConstants.ValueKey.SubType)), context, (int)context.GetValue(GeneratesConstants.ValueKey.Rule));
            maxs.Clear();
            Results.Clear();
            //Results = new GeneratingResult[] {};
            Context = context;
            for (var i = 0; i < (int)context.GetValue(GeneratesConstants.ValueKey.Qty); i++)
            {
                var cp = Prepare(context);
                cp.Wait4SequenceCompute += GetWait4SequenceCompute();
                cp.WhileSequenceCompute += GetWhileSequenceCompute();
                cp.AfterSequenceCompute += AfterSequenceCompute;

                lock (SyncObj)
                {
                    cp.PreCalculate();
                    var endNumber = cp.Calculate();
                    var begNumber = cp.CalculatePost();
                    var summaryNumber = cp.CalculateSummary();
                    //第一次生成序号时需要从起始序号生成
                    var res = GetLabelSerial(context); 
                    if (res == null)
                    {
                        if (summaryNumber.Count == 0)
                        {
                            summaryNumber.Add(begNumber);                           
                        }
                        summaryNumber.Add(begNumber);
                                             
                        result = new GeneratingResult(new[] { begNumber, endNumber, string.Join("|", summaryNumber) });
                        context.Result = result;
                    }
                    else
                    {
                        result = new GeneratingResult(new[] { begNumber, endNumber, string.Join("|", summaryNumber) });
                        context.Result = result;
                    }

                    if (i == 0)
                    {
                        Commit(context);
                    }
                    
                    maxs.Add(context.IntermediaResult[2]);
                    Results.Add(result);
                }
            }

            return Results;
        }

        private void GetDefaultDateCode(out string year , out string month, out string day)
        {
            var now = DateTime.Now;
            year = now.Year.ToString();
            month = now.Month.ToString();
            day = now.Day.ToString();
        }

        protected virtual void Commit(AbsGeneratePrinciple context)
        {
            string year, month, day;
            //GetDefaultDateCode(out year, out month, out day);
            var key = GetKeyInDB((string)context.GetValue(GeneratesConstants.ValueKey.SubType));
            var res = GetLabelSerial(context); 
            if (res == null)
            {
                res = new GenerateLabelSerial()
                {
                    Type = key,
                    RuleId = (int)context.GetValue(GeneratesConstants.ValueKey.Rule),
                    SerialSno = context.IntermediaResult[1]
                };
                //context.Result.Result[1];
                _repLabelSerial.Add(res);

                //序号已经使用，将不允许修改。
                var barcoderule = _repBarcodeRule.Query().Where(o => o.Id == (int)context.GetValue(GeneratesConstants.ValueKey.Rule)).FirstOrDefault();
                barcoderule.Status = "1";
            }
            else
            {
                res.SerialSno = context.IntermediaResult[1];//context.Result.Result[1];
                //_repSni.Update(res);
            }
        }

        protected abstract PlainSNComposerEvent GetWait4SequenceCompute();

        protected abstract PlainSNComposerEvent GetWhileSequenceCompute();

        private void AfterSequenceCompute(SNComposer cmp, NumberElement currNe, AbsGeneratePrinciple context)
        {
            //by yang.wei-hua: SNComposor.CalculatePost需要获取的是起始序号
            //currNe.Obj = Context.IntermediaResult[1];
            currNe.Obj = Context.IntermediaResult[0];
        }

        protected virtual SNComposer Prepare(AbsGeneratePrinciple context)
        {
            InitTemplates(context);
            var tmpl = GetTemplate(context.GetValue(GeneratesConstants.ValueKey.Rule).ToString());
            return MakeComposer(tmpl, context);
        }

        protected virtual void AddTemplatesOfAType(string type, IConverter[] val)
        {
            var snTemplatesManager = SNTemplateManager.GetInstance();
            var key = GetKey(type);
            snTemplatesManager.Add(key, val);
        }

        protected virtual void InitTemplates(AbsGeneratePrinciple context)
        {
            foreach (var st in SubTypes)
                AddTemplatesOfAType(context.GetValue(GeneratesConstants.ValueKey.Rule).ToString(), st.Converters(context));
        }

        protected virtual SNComposer MakeComposer(IConverter[] tmpl, IValueProvider obj)
        {
            var resSnC = new SNComposer(Context);

            foreach (var item in tmpl)
                resSnC.Add(new NumberElement(item, obj));

            return resSnC;
        }

        protected virtual string GetKey(string subType)
        {
            return GetSNType() + "." + subType;
        }

        protected virtual string GetKeyInDB(string subType)
        {
            foreach (var st in SubTypes)
            {
                if (st.Type == subType)
                {
                    return st.TypeInDB;
                }
            }
            // return GetKey(subType);
            return "Barcode";
            
        }

        protected virtual ISubGeneratingChannel GetSubGen(string subType)
        {
            foreach (var st in SubTypes)
            {
                if (st.Type == subType)
                {
                    return st;
                }
            }
            return null;
        }

        protected virtual string GetSNType()
        {
            // return EnumTranslater.GetName(Type);
            return "Barcode";
        }


        protected virtual IConverter[] GetTemplate(string subType)
        {
            try
            {
                var snTemplatesManager = SNTemplateManager.GetInstance();
                var cvts = snTemplatesManager.Gets(GetKey(subType));

                if (cvts != null && cvts.Length < 1)
                    throw new KeyNotFoundException();
                return cvts;
            }
            catch (KeyNotFoundException)
            {
                var keyValue = new[] { subType, subType };
                throw new BizException("GEN023", keyValue );
            }
        }

        protected void WhileSequenceCompute(SNComposer cmp, NumberElement currNe, AbsGeneratePrinciple context)
        {
            var seq = new[] { "", "", "" };
            var quantity = Context.Qty;
            var polderCnt = 0;
            var jumpNum = 1;
            var offset = 0;
            //var len = cmp.TotalLength;
            //var maxNumbersInMemory = (IReferedBox)_context;
            //var key = cmp.CurrentResult;
            var seqCvt = (AbsSequenceConverter)currNe.Cvt;//(AbsSequenceConverter)_context.Current;

            //==========================================================

            //var keyInMem = len.ToString() + "," + key;
            var seqSeg = string.Empty;//maxNumbersInMemory.GetMaxSN(keyInMem);
            if (string.IsNullOrEmpty(seqSeg))
            {
                Tuple<string, bool> currMax = null;
                if (maxs != null)
                {
                    if (Results.Count > 0)
                    {
                        currMax = new Tuple<string, bool>(maxs[maxs.Count - 1], true);
                    }
                }

                //ITC-1652-0106
                //var currMax = GetMaxSerialNumber(GetKeyInDB((string)context.GetValue(GeneratesConstants.ValueKey.SubType)), cmp.CurrentResultPreStr, context, (int)context.GetValue(GeneratesConstants.ValueKey.Rule));
                CheckResetSerialNumber(GetKeyInDB((string)context.GetValue(GeneratesConstants.ValueKey.SubType)), (int)context.GetValue(GeneratesConstants.ValueKey.Rule), (string)context.GetValue(GeneratesConstants.ValueKey.Reset));
                if (currMax == null || string.IsNullOrEmpty(currMax.Item1))
                {
                    if (CurrentMax != null)
                    {
                        seqSeg = CurrentMax.Item1;
                    }
                    else
                    {
                        seqSeg = seqCvt.NumberRule.MinNumber;
                    }
                    
                }
                else
                {
                    if (currMax.Item2)
                        seqSeg = currMax.Item1;
                    else
                        seqSeg = cmp.CutOutSeq(currMax.Item1);
                }
            }

            //==========================================================
            //第一次生成序号时需要从起始序号生成
            var res = GetLabelSerial(context);
            if (res == null)
            {
                var newSeqSeg = seqCvt.NumberRule.IncreaseToNumber(seqSeg, offset);

                seq[0] = newSeqSeg;

                var cnt = (int)context.GetValue(GeneratesConstants.ValueKey.Qty) - 1;

                newSeqSeg = seqCvt.NumberRule.IncreaseToNumber(seqSeg, offset + jumpNum * quantity * (polderCnt + 1) * cnt);
                seq[1] = newSeqSeg;

                newSeqSeg = seqCvt.NumberRule.IncreaseToNumber(seqSeg, offset);
                seq[2] = newSeqSeg;

                Context.IntermediaResult = seq;
                currNe.Obj = seq[1];
            }
            else
            {
                var newSeqSeg = seqCvt.NumberRule.IncreaseToNumber(seqSeg, offset + 1);

                seq[0] = newSeqSeg;

                var cnt = (int)context.GetValue(GeneratesConstants.ValueKey.Qty);

                newSeqSeg = seqCvt.NumberRule.IncreaseToNumber(seqSeg, offset + jumpNum * quantity * (polderCnt + 1) * cnt);
                seq[1] = newSeqSeg;

                newSeqSeg = seqCvt.NumberRule.IncreaseToNumber(seqSeg, offset + jumpNum * quantity * (polderCnt + 1));
                seq[2] = newSeqSeg;

                Context.IntermediaResult = seq;
                currNe.Obj = seq[1];
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="preStr"></param>
        /// <param name="context"></param>
        /// <returns>Item1: MaxValue; Item2: If Sheer Only Sequence Part</returns>
        protected virtual Tuple<string, bool> GetMaxSerialNumber(string type, AbsGeneratePrinciple context, int ruleid)
        {
            Tuple<string, bool> ret = null;
            //var dt = (DateTime)context.GetValue(GeneratesConstants.ValueKey.DateTime);
            string year, month, day;
            GetDefaultDateCode(out year, out month, out day);
            var res = _repLabelSerial.Query().Where(i => i.Type == type && i.RuleId == ruleid).ToList();//&& i.Value.IsLike(preStr, MatchMode.Exact));
            if (res != null && res.Count > 0)
                ret = new Tuple<string,bool>(res[0].SerialSno, true);
            return ret;
        }

        protected virtual void CheckResetSerialNumber(string type, int ruleid, string reset)
        {
            var res = _repLabelSerial.Query().Where(i => i.Type == type && i.RuleId == ruleid).ToList();
            if (res != null && res.Count > 0)
            {
                DateTime dt1 = Convert.ToDateTime(res[0].Cdt);
                DateTime dt2 = System.DateTime.Now;
                int Year = dt2.Year - dt1.Year;
                int Month = (dt2.Year - dt1.Year) * 12 + (dt2.Month - dt1.Month);
                TimeSpan ts = dt2 - dt1;
                int Day = ts.Days;
                if (reset == "年")
                {
                    if (Year > 0)
                    {
                        if (res != null)
                        {
                            foreach (var entry in res)
                                _repLabelSerial.Delete(entry);
                        }
                    }
                }
                else if (reset == "月")
                {
                    if (Month > 0)
                    {
                        if (res != null)
                        {
                            foreach (var entry in res)
                                _repLabelSerial.Delete(entry);
                        }
                    }
                }
                else if (reset == "日")
                {
                    if (Day > 0)
                    {
                        if (res != null)
                        {
                            foreach (var entry in res)
                                _repLabelSerial.Delete(entry);
                        }
                    }
                }
            }

        }

        protected virtual GenerateLabelSerial GetLabelSerial(AbsGeneratePrinciple context)
        {
            var key = GetKeyInDB((string)context.GetValue(GeneratesConstants.ValueKey.SubType));
            var res = _repLabelSerial.Query().Where(o => o.Type == key && o.RuleId == (int)context.GetValue(GeneratesConstants.ValueKey.Rule)).FirstOrDefault();
            return res;
        }
    }
}
