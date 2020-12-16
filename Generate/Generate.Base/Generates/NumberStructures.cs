using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IIMes.Services.Generate.Algorithm;

namespace IIMes.Services.Generate
{
    public class ContinuousNumbers
    {
        public string NumberBegin = String.Empty;
        public string NumberEnd = String.Empty;
    }

    public class SequencialNumberSegment : ContinuousNumbers
    {
        public string NextAvailableNumber { get; private set; }

        IMathSequenceWithCarryNumberRule _rule = null;
        public SequencialNumberSegment(IMathSequenceWithCarryNumberRule rule)
            : this()
        {
            _rule = rule;
        }

        SequencialNumberSegment() { iStep = 1; }
        public int iStep { get; set; }

        public bool Contains(string number)
        {
            var beg = _rule.CalculateDifference(NumberBegin, number);
            var end = _rule.CalculateDifference(NumberEnd, number);
            if (beg <= 0 && end >= 0)
            {
                NextAvailableNumber = _rule.IncreaseToNumber(NumberEnd, 1);
                return true;
            }
            else
            {
                NextAvailableNumber = NumberBegin;
                return false;
            }
        }
    }

    public class SequencialNumberRanges
    {
        public SequencialNumberSegment[] Ranges { get; set; } //Should Be By Sequence, Could Be Neither Closed To Nor OverLapped!

        public string NextAvailableNumber { get; private set; }

        public bool Contains(string number)
        {
            if (null == Ranges)
                return false;

            foreach (var range in Ranges)
            {
                if (range.Contains(number))
                {
                    NextAvailableNumber = range.NextAvailableNumber;
                    return true;
                }
            }
            return false;
        }
    }
}
