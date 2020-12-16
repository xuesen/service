///<summary>
/// INVENTEC corporation (c)2009 all rights reserved. 
/// Description: 有進位數學Bits數規則类, 不同位的有效字符范围和进制都可以不同
///             
/// Update: 
/// Date       Name                  Reason 
///========== ===================== =====================================
/// 2009-10-15  Liu Dong(eB1-4)         Create 
///</summary>

using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Exception;

namespace IIMes.Services.Generate.Algorithm
{
    /// <summary>
    /// 有進位數學Bits數規則
    /// </summary>
    public class MathSequenceWithMultiCarryNumberRule : IMathSequenceWithCarryNumberRule
    {
        private readonly string[] _charCollections;
        private string _maxNumber;
        private string _minNumber;

        /// <summary>
        /// 最大位数
        /// </summary>
        public int iBits { get; private set; }

        /// <summary>
        /// 步进值
        /// </summary>
        public int Step { get; private set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_maxNumber))
                {
                    foreach (var charCollection in _charCollections)
                    {
                        var charMax = charCollection.Substring(charCollection.Length - 1, 1);
                        _maxNumber += charMax;
                    }
                }
                return _maxNumber;
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public string MinNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_minNumber))
                {
                    foreach (var charCollection in _charCollections)
                    {
                        var charMin = charCollection.Substring(0, 1);
                        _minNumber += charMin;
                    }
                }
                return _minNumber;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="charCollections"></param>
        public MathSequenceWithMultiCarryNumberRule(int bits, string[] charCollections)
        {
            //輸入參數檢查：長度、字符集等等
            if (bits < 1) 
            {
                throw new BizException("GEN001");
            }
            if (charCollections == null || charCollections.Length != bits) 
            {
                throw new BizException("GEN002");
            }
            foreach (var charCollection in charCollections)
            {
                if (charCollection.Length < 2)
                {
                    throw new BizException("GEN004");
                }
            }

            Step = 1;
            iBits = bits;
            _charCollections = charCollections;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="charCollections"></param>
        /// <param name="maxNumber"></param>
        /// <param name="minNumber"></param>
        public MathSequenceWithMultiCarryNumberRule(int bits, string[] charCollections, string maxNumber, string minNumber) : this(bits, charCollections)
        {
            _maxNumber = maxNumber;
            _minNumber = minNumber;

            //輸入參數檢查：長度、字符集等等
            if (!string.IsNullOrEmpty(maxNumber) && maxNumber.Length != bits)
            {
                var keyValue = new[] { bits.ToString() };
                throw new BizException("GEN007", keyValue);
            }
            if (!string.IsNullOrEmpty(minNumber) && minNumber.Length != bits)
            {
                var keyValue = new[] { bits.ToString() };
                throw new BizException("GEN010", keyValue.ToString());
            }
            if (!string.IsNullOrEmpty(maxNumber) && !InCharCollection(maxNumber))
            {
                var keyValue = new[] { _charCollections.ToString() };
                throw new BizException("GEN011", keyValue);
            }
            if (!string.IsNullOrEmpty(minNumber) && !InCharCollection(minNumber))
            {
                var keyValue = new[] { _charCollections.ToString() };
                throw new BizException("GEN012", keyValue);
            }
            if (!string.IsNullOrEmpty(minNumber) && !string.IsNullOrEmpty(maxNumber))
            {
                var max = ToNumber(maxNumber);
                var min = ToNumber(minNumber);
                if (max < min)
                {
                    throw new BizException("GEN013");
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bits"></param>
        /// <param name="charCollections"></param>
        /// <param name="maxNumber"></param>
        /// <param name="minNumber"></param>
        /// <param name="step"></param>
        public MathSequenceWithMultiCarryNumberRule(int bits, string[] charCollections, string maxNumber, string minNumber, int step) : this(bits, charCollections, maxNumber, minNumber)
        {
            Step = step;
        }

        private int ToNumber(string numberString)
        {
            if (string.IsNullOrEmpty(numberString))
            {
                return 0;
            }

            var ret = 0;
            for (var i = 0; i < iBits; i++)
            {
                var charValue = _charCollections[i].IndexOf(numberString[i]);
                var multiplier = GetBitMutiplier(i);
                ret = ret + charValue * multiplier;
            }
            return ret;
        }

        private string ToString(int number)
        {
            var ret = string.Empty;
            var remainer = number;
            for (var i = 0; i < iBits; i++)
            {
                var divisor = GetBitMutiplier(i);
                var bitValue = remainer / divisor;
                remainer = remainer % divisor;
                if (bitValue >= _charCollections[i].Length)
                {
                    throw new BizException("GEN027");
                }
                ret += _charCollections[i][bitValue].ToString();
            }
            return ret;
        }

        private int GetBitMutiplier(int i)
        {
            var ret = 1;
            for (int j = i + 1; j < iBits; j++)
            {
                ret *= _charCollections[j].Length;
            }
            return ret;
        }

        /// <summary>
        /// 得到一個數步進幾次的值
        /// </summary>
        /// <param name="numberStart">起始數</param>
        /// <param name="count">步進次數</param>
        /// <returns>所得值</returns>
        /// <remarks></remarks>
        public string IncreaseToNumber(string numberStart, int count)
        {
            //輸入參數檢查：長度、字符集等等

            #region Check

            if (numberStart == null)
            {
                throw new BizException("GEN014");
            }
            numberStart = numberStart.Trim();
            if (numberStart == String.Empty) 
            {
                throw new BizException("GEN015");
            }

            if (numberStart.Length != iBits)
            {
                var keyValue = new[] { iBits.ToString() };
                throw new BizException("GEN016", keyValue);
            }

            if (! InCharCollection(numberStart))
            {
                var keyValue = new[] { _charCollections[0] };
                throw new BizException("GEN017", keyValue);
            }

            if (String.CompareOrdinal(numberStart, MaxNumber) > 0)
            {
                var keyValue = new[] { MaxNumber };
                throw new BizException("GEN018", keyValue);
            }

            if (string.CompareOrdinal(numberStart, MinNumber) < 0)
            {
                var keyValue = new[] { MinNumber };
                throw new BizException("GEN019", keyValue);
            }

            if (count < 0)
            {
                throw new BizException("GEN020");
            }
            #endregion

            if (count == 0)
                return numberStart;
            var number = ToNumber(numberStart);
            number += count * Step;
            return ToString(number);
        }

        public IList<string> GetRage(string numberStart, int count)
        {
            var ret = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var item = IncreaseToNumber(numberStart, i);
                ret.Add(item);
            }
            return ret;
        }

        /// <summary>
        /// 计算2个数之间的差
        /// </summary>
        /// <param name="numberStart"></param>
        /// <param name="numberEnd"></param>
        /// <returns></returns>
        public long CalculateDifference(string numberStart, string numberEnd)
        {
            #region Check

            if (numberStart == null)
            {
                throw new BizException("GENK01");
            }
            numberStart = numberStart.Trim();
            if (numberStart == String.Empty)
            {
                throw new BizException("GENK02");
            }

            if (numberStart.Length != iBits)
            {
                throw new BizException("GENK03", iBits.ToString());
            }

            if (!InCharCollection(numberStart))
            {
                throw new BizException("GENK04", _charCollections[0]);
            }

            if (String.CompareOrdinal(numberStart, MaxNumber) > 0)
            {
                throw new BizException("GENK05", MaxNumber);
            }

            if (string.CompareOrdinal(numberStart, MinNumber) < 0)
            {
                throw new BizException("GENK06", MinNumber);
            }

            //--------------------------

            if (numberEnd == null)
            {
                throw new BizException("GENK07");
            }
            numberEnd = numberEnd.Trim();
            if (numberEnd == String.Empty)
            {
                throw new BizException("GENK08");
            }

            if (numberEnd.Length != iBits)
            {
                throw new BizException("GENK09", iBits.ToString());
            }

            if (!InCharCollection(numberEnd))
            {
                throw new BizException("GENK10", _charCollections[0]);
            }

            if (String.CompareOrdinal(numberEnd, MaxNumber) > 0)
            {
                throw new BizException("GENK11", MaxNumber);
            }

            if (string.CompareOrdinal(numberEnd, MinNumber) < 0)
            {
                throw new BizException("GENK12", MinNumber);
            }

            #endregion

            if (numberEnd.Equals(numberStart))
                return 0;
            var endValue = ToNumber(numberEnd);
            var startValue = ToNumber(numberStart);
            return endValue - startValue;
        }

        /// <summary>
        /// 判斷一個數的字符是否在指定字符集中
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool InCharCollection(string str)
        {
            for (int i = 0; i < iBits; i++)
            {
                if (!_charCollections[i].Contains(str[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
