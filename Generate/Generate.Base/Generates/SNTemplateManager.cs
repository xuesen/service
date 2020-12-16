using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Exception;

namespace IIMes.Services.Generate
{
    /// <summary>
    /// 序列号生成管理类
    /// </summary>
    public class SNTemplateManager
    {
        /// <summary>
        /// SN Generate模版緩存
        /// </summary>
        /// <remarks></remarks>
        private readonly IDictionary<string, IList<IConverter>> _keyAndArray = new Dictionary<string, IList<IConverter>>();

        #region "Singleton"

        private static SNTemplateManager _instance;

        private static readonly object locker = new object();

        public static SNTemplateManager GetInstance()
        {
            lock (locker)
            {
                if (_instance == null)
                    _instance = new SNTemplateManager();
            }

            return _instance;
        }

        private SNTemplateManager()
        {

        }

        #endregion

        /// <summary>
        /// Rules緩存賍位
        /// </summary>
        /// <remarks></remarks>
        private IDictionary<string, bool> _snRuleFileDirtyBits = new Dictionary<string, bool>();

        private readonly object _sycRoot = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="array"></param>
        public void Add(string key, IConverter[] array)
        {
            if (array == null || array.Length < 1 )
                throw new BizException("GEN025");

            lock (_sycRoot)
            {
                IList<IConverter> stem;
                try
                {
                    stem = _keyAndArray[key];
                    stem = Convert(array); //Update
                }
                catch (KeyNotFoundException)
                {
                    stem = Convert(array);
                    _keyAndArray.Add(key, stem);  //Insert
                }
            }
        }

        public IConverter[] Gets(string key)
        {
            lock (_sycRoot)
            {
                List<IConverter> stem = null;
                try
                {
                    stem = (List<IConverter>)_keyAndArray[key];
                }
                catch (KeyNotFoundException)
                { }

                if (stem == null)
                {
                    IList<string> param = new List<string>();
                    param.Add(key);
                    throw new BizException("GEN026", param.ToArray());
                }
                else
                    return Convert(stem);
            }
        }

        public void SetDirtyBit(string snGenType, bool bValue)
        {
            lock (_sycRoot)
            {
                if (!_snRuleFileDirtyBits.ContainsKey(snGenType))
                    _snRuleFileDirtyBits.Add(snGenType, true); //Initialize the dirty bit.
                else
                    _snRuleFileDirtyBits[snGenType] = bValue;
            }
        }

        public bool GetDirtyBit(string snGenType)
        {
            lock (_sycRoot)
            {
                if (!_snRuleFileDirtyBits.ContainsKey(snGenType))
                {
                    _snRuleFileDirtyBits.Add(snGenType, true); //Initialize the dirty bit.
                    return true;
                }
                else
                {
                    return _snRuleFileDirtyBits[snGenType];
                }
            }
        }

        private List<IConverter> Convert(IConverter[] array)
        {
            return array.ToList();
        }

        private IConverter[] Convert(List<IConverter> list)
        {
            return list.ToArray();
        }

    }
}
