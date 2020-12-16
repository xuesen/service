///<summary>
/// INVENTEC corporation (c)2009 all rights reserved. 
/// Description: 有進位數學Bits數規則类
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
    public class MathSequenceWithCarryNumberRule : IMathSequenceWithCarryNumberRule
    {
        private int _iBits  = 1;
        private string _charCollection = string.Empty;
        private string _maxNumber = string.Empty;
        private string _minNumber = string.Empty;

        /// <summary>
        /// 最大位数
        /// </summary>
        public int iBits
        {
            get { return _iBits; }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxNumber
        {
            get{return _maxNumber;}
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public string MinNumber
        {
            get{return _minNumber;}
        }

        private string _generalMaxNumber = string.Empty;
        private string _generalMinNumber = string.Empty;

        private int _iStep = 1;

        /// <summary>
        /// 步进值
        /// </summary>
        public int iStep
        {
            get{return _iStep;}
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iBits"></param>
        /// <param name="charCollection"></param>
        public MathSequenceWithCarryNumberRule(int iBits, string charCollection)
        {
            //輸入參數檢查：長度、字符集等等

            if (iBits < 1) 
            {
                throw new BizException("GEN001");
            }

            if (charCollection == null) 
            {
                throw new BizException("GEN002");
            }
            charCollection = charCollection.Trim();
            if (charCollection == string.Empty)
            {
                throw new BizException("GEN003");
            }

            if (charCollection.Length < 2)
            {
                throw new BizException("GEN004");
            }

            _iBits = iBits;
            _charCollection = charCollection;

            _maxNumber = GetGeneralMaxNumber();
            _minNumber = GetGeneralMinNumber();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iBits"></param>
        /// <param name="charCollection"></param>
        /// <param name="maxNumber"></param>
        /// <param name="minNumber"></param>
        public MathSequenceWithCarryNumberRule(int iBits, string charCollection, string maxNumber, string minNumber) : this(iBits, charCollection)
        {
            //輸入參數檢查：長度、字符集等等

            if (maxNumber == null)
            {
                throw new BizException("GEN005");
            }

            maxNumber = maxNumber.Trim();
            if (maxNumber == string.Empty)
            {
                throw new BizException("GEN006");
            }

            if (maxNumber.Length != _iBits)
            {
                var keyValue = new[] { _iBits.ToString() };
                throw new BizException("GEN007", keyValue);
            }

            if (minNumber == null)
            {
                throw new BizException("GEN008");
            }
            minNumber = minNumber.Trim();
            if (minNumber == string.Empty)
            {
                throw new BizException("GEN009");
            }

            if (minNumber.Length != _iBits)
            {
                var keyValue = new[] { _iBits.ToString() };
                throw new BizException("GEN010", keyValue);
            }

            if (! InCharCollection(maxNumber))
            {
                var keyValue = new[] { _charCollection.ToString() };
                throw new BizException("GEN011", keyValue);
            }

            if (! InCharCollection(minNumber))
            {
                var keyValue = new[] { _charCollection.ToString() };
                throw new BizException("GEN012", keyValue);
            }

            if (Cmp(maxNumber, minNumber) == -1)
            {
                throw new BizException("GEN013");
            }

            //不會發生以下2個問題, 故注掉
            //If Me.Cmp(maxNumber, Me.GetGeneralMaxNumber()) = 1 Then
            //    Throw New FisException(String.Format("The customized max number exceeds the Max limit! [{0}] ", Me.GetGeneralMaxNumber()))
            //End If

            //If Me.Cmp(minNumber, Me.GetGeneralMinNumber()) = -1 Then
            //    Throw New FisException(String.Format("The customized min number exceeds the Min limit! [{0}] ", Me.GetGeneralMinNumber()))
            //End If

            _maxNumber = maxNumber;
            _minNumber = minNumber;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iBits"></param>
        /// <param name="charCollection"></param>
        /// <param name="maxNumber"></param>
        /// <param name="minNumber"></param>
        /// <param name="iStep"></param>
        public MathSequenceWithCarryNumberRule(int iBits, string charCollection, string maxNumber, string minNumber, int iStep) : this(iBits, charCollection, maxNumber, minNumber)
        {
            _iStep = iStep;
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

            if (numberStart.Length != _iBits)
            {
                var keyValue = new[] { _iBits.ToString() };
                throw new BizException("GEN016", keyValue.ToString());
            }

            if (! InCharCollection(numberStart))
            {
                var keyValue = new[] { _charCollection.ToString() };
                throw new BizException("GEN017", keyValue);
            }

            if (Cmp(numberStart, GetGeneralMaxNumber()) == 1)
            {
                var keyValue = new[] { GetGeneralMaxNumber() };
                throw new BizException("GEN018", keyValue);
            }

            if (Cmp(numberStart, GetGeneralMinNumber()) == -1)
            {
                var keyValue = new[] { GetGeneralMinNumber() };
                throw new BizException("GEN019", keyValue);
            }

            #endregion

            if (count < 0)
            {
                throw new BizException("GEN020");
            }

            if (count == 0)
                return numberStart;

            char[] chsNumberStart = numberStart.ToCharArray();

            Array.Reverse(chsNumberStart);

            char[] result = (char[])(chsNumberStart.Clone());

            //每位上需要加的位數
            int[] addNumberBits = new int[chsNumberStart.Length];
            for (int t = 0; t <= addNumberBits.Length - 1; t++)
            {
                addNumberBits[t] = 0;
            }

            int div = (_iStep * count) / _charCollection.Length;
            int remain = (_iStep * count) % _charCollection.Length;

            int j = 0; //記錄需要加數的最高位數
            int i;
            for (i = 0 ; i <= chsNumberStart.Length - 1; i++) //Step 1
            {
                addNumberBits[i] = remain;
                j = j + 1;

                if (div == 0)
                {
                    break;
                }
                else if( i == chsNumberStart.Length - 1 )
                {
                    ThrowExceedGeneralMaxException();
                }

                int divNew;
                divNew = div / _charCollection.Length;
                remain = div % _charCollection.Length;
                div = divNew;
            }

            bool isCarry = false;
            for (i = 0; i <= chsNumberStart.Length - 1; i++) //Step 1
            {
                if (i == j)
                {
                    break;
                }

                char newChar = Increase(chsNumberStart[i], addNumberBits[i]);

                result[i] = newChar;
                // 2009-06-22  Liu Dong(eB1-4)         Modify BUG: 進位使高一位的加數9變成0,沒有再進位
                isCarry = (Cmp(newChar, chsNumberStart[i]) == -1) || addNumberBits[i].Equals(_charCollection.Length); //是否進位

                if (isCarry)
                {
                    if (i < chsNumberStart.Length - 1)
                    {
                        addNumberBits[i + 1] = addNumberBits[i + 1] + 1;

                        if (j == i + 1)
                        {
                            j = j + 1;   //需要加數的最高位數因爲進位而增一
                        }
                    }
                    else
                    {
                        ThrowExceedGeneralMaxException();
                    }
                }
            }

            string sResult = Convert(result);

            CheckAndThrowExceedGeneralMaxException(sResult);

            if (_maxNumber != string.Empty && Cmp(sResult, _maxNumber) == 1)
            {
                var keyValue = new[] { _maxNumber.ToString() };
                throw new BizException("GEN021", keyValue);
            }
            return sResult;
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
            long ret = 0;

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

            if (numberStart.Length != _iBits)
            {
                throw new BizException("GENK03", _iBits.ToString());
            }

            if (!InCharCollection(numberStart))
            {
                throw new BizException("GENK04", _charCollection);
            }

            if (Cmp(numberStart, GetGeneralMaxNumber()) == 1)
            {
                throw new BizException("GENK05", GetGeneralMaxNumber());
            }

            if (Cmp(numberStart, GetGeneralMinNumber()) == -1)
            {
                throw new BizException("GENK06", GetGeneralMinNumber());
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

            if (numberEnd.Length != _iBits)
            {
                throw new BizException("GENK09", _iBits.ToString());
            }

            if (!InCharCollection(numberEnd))
            {
                throw new BizException("GENK10", _charCollection);
            }

            if (Cmp(numberEnd, GetGeneralMaxNumber()) == 1)
            {
                throw new BizException("GENK11", GetGeneralMaxNumber());
            }

            if (Cmp(numberEnd, GetGeneralMinNumber()) == -1)
            {
                throw new BizException("GENK12", GetGeneralMinNumber());
            }

            #endregion

            if (numberEnd.Equals(numberStart))
                return 0;

            char[] chsNumberStart = numberStart.ToCharArray();
            char[] chsNumberEnd = numberEnd.ToCharArray();

            //Array.Reverse(chsNumberStart);
            //Array.Reverse(chsNumberEnd);

            //每位上的差
            int[] diffNumbersByBit = new int[chsNumberStart.Length];
            bool isMinus = false;
            bool isSetMinus = false;//是否找到第一个不为0的位差
            for (int t = 0; t <= diffNumbersByBit.Length - 1; t++)
            {
                diffNumbersByBit[t] = Substract(chsNumberEnd[t], chsNumberStart[t]);
                if (isSetMinus == false && diffNumbersByBit[t] != 0)
                {
                    isMinus = diffNumbersByBit[t] < 0;
                    isSetMinus = true;
                }
                if (isSetMinus && isMinus)
                {
                    diffNumbersByBit[t] = 0 - diffNumbersByBit[t];
                }
                if (isSetMinus && diffNumbersByBit[t] < 0)
                {
                    int i = t;
                    do
                    {
                        diffNumbersByBit[i] = _charCollection.Length + diffNumbersByBit[i];
                        diffNumbersByBit[i - 1] = diffNumbersByBit[i - 1] - 1;
                        i = i - 1;
                    }
                    while (i > 0 && diffNumbersByBit[i] < 0);//一直借位
                }
            }

            Array.Reverse(diffNumbersByBit);

            for (int p = 0; p <= diffNumbersByBit.Length - 1; p++)
            {
                if (diffNumbersByBit[p] != 0)
                {
                    ret = ret + System.Convert.ToInt64(diffNumbersByBit[p] * Math.Pow(_charCollection.Length, p));
                }
            }
            return isMinus ? 0 - ret : ret;
        }

        #region Inner

        /// <summary>
        /// 一位字符增一個Step
        /// </summary>
        /// <param name="bit"></param>
        /// <returns>得到的步進一次結果</returns>
        /// <remarks></remarks>
        private char Increase(char bit)
        {
            int now = _charCollection.IndexOf(bit);
            if (now + _iStep > _charCollection.Length - 1)
            {
                return char.Parse(_charCollection.Substring(now + _iStep - _charCollection.Length, 1));
            }
            else
            {
                return char.Parse(_charCollection.Substring(now + _iStep, 1));
            }
        }

        /// <summary>
        /// 一位字符增指定次數
        /// </summary>
        /// <param name="bit"></param>
        /// <returns>得到的步進一次結果</returns>
        /// <remarks></remarks>
        private char Increase(char bit, int count)
        {
            int now = _charCollection.IndexOf(bit);
            if (now + count > _charCollection.Length - 1)
            {
                return char.Parse(_charCollection.Substring(now + count - _charCollection.Length, 1));
            }
            else
            {
                return char.Parse(_charCollection.Substring(now + count, 1));
            }
        }

        ///// <summary>
        ///// 一位字符减指定次數
        ///// </summary>
        ///// <param name="bit"></param>
        ///// <returns></returns>
        ///// <remarks></remarks>
        //private char Decrease(char bit, int count)
        //{
        //    int now = this._charCollection.IndexOf(bit);
        //    if (now - count < 0)
        //    {
        //        return char.Parse(this._charCollection.Substring(now - count + this._charCollection.Length, 1));
        //    }
        //    else
        //    {
        //        return char.Parse(this._charCollection.Substring(now - count, 1));
        //    }
        //}

        /// <summary>
        /// 比較2個字符在charCollection中的大小關係
        /// </summary>
        /// <param name="bit1"></param>
        /// <param name="bit2"></param>
        /// <returns>1: 大於,0: 等於, -1: 小於</returns>
        /// <remarks></remarks>
        private int Cmp(char bit1, char bit2)
        {
            return _charCollection.IndexOf(bit1).CompareTo(_charCollection.IndexOf(bit2));
        }

        /// <summary>
        /// 比較2個數的大小關係
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns>1: 大於,0: 等於, -1: 小於</returns>
        /// <remarks></remarks>
        private int Cmp(string num1, string num2)
        {
            if (num1.Equals(num2))
            {
                return 0;
            }

            if (num1.Length > num2.Length)
            {
                return 1;
            }

            if (num1.Length < num2.Length)
            {
                return -1;
            }

            int i;
            for (i = 0; i <= num1.Length - 1; i++)
            {
                int resCmp = Cmp(num1.ToCharArray()[i], num2.ToCharArray()[i]);
                if (resCmp == 1)
                {
                    return 1;
                }
                else if (resCmp == -1)
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// bit1 - bit2
        /// </summary>
        /// <param name="bit1"></param>
        /// <param name="bit2"></param>
        /// <returns>差</returns>
        private int Substract(char bit1, char bit2)
        {
            return _charCollection.IndexOf(bit1) - _charCollection.IndexOf(bit2);
        }

        private string Convert(char[] chars)
        {
            string ret = string.Empty;
            foreach (char chrct in chars)
            {
                ret = chrct.ToString() + ret;
            }
            return ret;
        }

        /// <summary>
        /// 判斷一個數的字符是否在指定字符集中
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool InCharCollection(string str)
        {
            foreach (char s in str.ToCharArray())
            {
                if (_charCollection.IndexOf(s) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 獲得現有bit和charCollection的最大數
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetGeneralMaxNumber()
        {
            if (_generalMaxNumber == string.Empty)
            {
                char charMax = char.Parse(_charCollection.Substring(_charCollection.Length - 1, 1));
                string ret = charMax.ToString();
                _generalMaxNumber = ret.PadLeft(_iBits, charMax);
            }
            return _generalMaxNumber;
        }

        /// <summary>
        /// 獲得現有bit和charCollection的最小數
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetGeneralMinNumber()
        {
            if (_generalMinNumber == string.Empty)
            {
                char charMin = char.Parse(_charCollection.Substring(0, 1));
                string ret = charMin.ToString();
                _generalMinNumber = ret.PadLeft(_iBits, charMin);
            }
            return _generalMinNumber;
        }

        private void CheckAndThrowExceedGeneralMaxException(string str)
        {
            if (Cmp(str, GetGeneralMaxNumber()) == 1)
            {
                ThrowExceedGeneralMaxException();
                //Throw New FisException(String.Format("The result number exceeds the general Max limit! [{0}] ", Me.GetGeneralMaxNumber()))
            }
        }

        private void ThrowExceedGeneralMaxException()
        {
            var keyValue = new[] { GetGeneralMaxNumber() };
            throw new BizException("GEN022", keyValue);
        }

        #endregion
    }
}
