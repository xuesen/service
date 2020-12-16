using System.Collections.Generic;
using System.Text;
using IIMes.Services.Generate.intf;

namespace IIMes.Services.Generate
{
    /// <summary>
    /// 序列号生成器
    /// </summary>
    public class SNComposer
    {
        private IList<NumberElement> _content;
        private IDictionary<int, NumberElement> _content4Seq;
        private readonly AbsGeneratePrinciple _context;
        public static string GlobalName = "SNComposer";

        public SNComposer(AbsGeneratePrinciple context)
        {
            _context = context;
            _context.AddValue(GlobalName, this);
        }

        private readonly IList<string> _currentResultAsArr = new List<string>();
        private string _currentResult = string.Empty;
        private string _currentTemplate = string.Empty;
        public string CurrentResult
        {
            get { return _currentResult; }
        }

        private string _currentResultPreStr = string.Empty;
        public string CurrentResultPreStr
        {
            get { return _currentResultPreStr; }
        }

        public string CurrentResult4Sql
        {
            get
            {
                string ret = _currentTemplate;
                for (int i = 0; i < _content4Seq.Count; i++)
                    ret = ret.Replace("{" + i + "}", "%");
                return ret;
            }
        }

        private string CurrentResultWithStrip
        {
            get
            {
                string ret = _currentTemplate;
                for (int i = 0; i < _content4Seq.Count; i++)
                    ret = ret.Replace("{" + i + "}", "");
                return ret;
            }
        }

        public string this[int index]
        {
            get
            {
                return _currentResultAsArr[index];
            }
        }

        public int TotalLength
        {
            get
            {
                int ret = CurrentResultWithStrip.Length;
                foreach (var item in _content4Seq)
                    ret += ((AbsSequenceConverter)item.Value.Cvt).NumberRule.iBits;
                return ret;
            }
        }

        public string CutOutSeq(string wholeNumber) //All Seqs as a Seq
        {
            StringBuilder ret = new StringBuilder();
            int i = 0;
            int j = 0;
            foreach (NumberElement item in _content)
            {
                if (item.Cvt is AbsSequenceConverter)
                {
                    int len = ((AbsSequenceConverter)item.Cvt).NumberRule.iBits;
                    ret.Append(wholeNumber.Substring(i, len));
                    i += len;
                }
                else
                {
                    i += _currentResultAsArr[j].Length;
                }
                j++;
            }
            return ret.ToString();
        }

        /// <summary>
        /// Add a number element
        /// </summary>
        /// <param name="ne">The number element to add</param>
        /// <remarks></remarks>
        public void Add(NumberElement ne)
        {
            if (_content == null)
                _content = new List<NumberElement>();
            if (_content4Seq == null)
                _content4Seq = new Dictionary<int, NumberElement>();

            _content.Add(ne);
            int i = 0;
            if (ne.Cvt is AbsSequenceConverter)
                _content4Seq.Add(i, ne);
        }

        public string PreCalculate()
        {
            _currentResult = string.Empty;
            _currentTemplate = string.Empty;
            _currentResultAsArr.Clear();

            if (_content != null && _content.Count > 0)
            {
                foreach (NumberElement elem in _content)
                {
                    string result = string.Empty;
                    int i = 1;
                    if (elem.Cvt is AbsSequenceConverter)
                    {
                        Wait4SequenceCompute(this, elem, _context);
                        result = string.Format("{{0}}", i++);
                        _currentResultPreStr += "%";
                    }
                    else
                    {
                        result = elem.Cvt.Convert(elem.Obj);
                        _currentResultPreStr += result;
                    }
                    if (result != null)
                    {
                        _currentTemplate += result;
                        _currentResultAsArr.Add(result);
                    }
                }
                return _currentResult = _currentTemplate;
            }
            return null;
        }
        /// <summary>
        /// Calculate the value of the number that is represented by the content
        /// </summary>
        /// <returns>The SN without the sequence tail</returns>
        /// <remarks></remarks>
        public string Calculate()
        {
            _currentResult = _currentTemplate;
            foreach (var elem in _content4Seq)
            {
                WhileSequenceCompute(this, elem.Value, _context);
                _currentResult = _currentResult.Replace("{" + elem.Key + "}", elem.Value.Cvt.Convert(elem.Value.Obj));
            }
            return _currentResult;
        }

        public string CalculatePost()
        {
            _currentResult = _currentTemplate;
            foreach (var elem in _content4Seq)
            {
                AfterSequenceCompute(this, elem.Value, _context);
                _currentResult = _currentResult.Replace("{" + elem.Key + "}", elem.Value.Cvt.Convert(elem.Value.Obj));
            }
            return _currentResult;
        }

        public IList<string> CalculateSummary()
        {
            var ret = new List<string>();
            foreach (var elem in _content4Seq)
            {
                AfterSequenceCompute(this, elem.Value, _context);
                if (elem.Value.Cvt is AbsSequenceConverter)
                {
                    var values = ((AbsSequenceConverter) elem.Value.Cvt).NumberRule.GetRage(elem.Value.Cvt.Convert(elem.Value.Obj),
                        _context.Qty);
                    foreach (var value in values)
                    {
                        var item = _currentTemplate.Replace("{" + elem.Key + "}", value);
                        ret.Add(item);
                    }
                }
            }
            //var begin = _context.IntermediaResult[0];
            return ret;
        }

        public event PlainSNComposerEvent Wait4SequenceCompute;
        public event PlainSNComposerEvent WhileSequenceCompute;
        public event PlainSNComposerEvent AfterSequenceCompute;

        /// <summary>
        /// Modify a number element
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="ne"></param>
        /// <remarks></remarks>
        public void Modify(int idx, NumberElement ne)
        {
            if (_content == null)
                _content = new List<NumberElement>();

            if (idx < _content.Count)
                _content[idx] = ne;
        }
    }

    public delegate void PlainSNComposerEvent(SNComposer cmp, NumberElement currNe, AbsGeneratePrinciple context);
}
