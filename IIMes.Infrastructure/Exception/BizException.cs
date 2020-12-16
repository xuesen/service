using System;
using System.Runtime.Serialization;

namespace IIMes.Infrastructure.Exception
{
    /// <summary>
    /// 系统自定义异常类.
    /// </summary>
    [Serializable]
    public class BizException : System.Exception
    {
        private string _message;

        public Level Level { get; set; } = Level.Error;

        public string ErrCode { get; set; }

        public object[] Args { get; set; }

        public override string Message
        {
            get => _message;
        }

        public BizException(string errCode, params object[] args)
            : base()
        {
            ErrCode = errCode;
            Args = args;
        }

        public BizException(Level level, string message)
        {
            Level = level;
            _message = message;
        }

        public void SetMessage(string message)
        {
            _message = message;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ErrCode", ErrCode);
            info.AddValue("Level", (int)Level);
            info.AddValue("ErrMessage", Message);
            base.GetObjectData(info, context);
        }

        protected BizException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrCode = info.GetString("errCode");
            _message = info.GetString("message");
            Level = (Level)info.GetInt32("level");
        }
    }
}
