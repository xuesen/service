namespace IIMes.Infrastructure.BaseController
{
    public class BaseResult<T>
    {
        public string Result { get; set; }

        public string ErrMsg { get; set; }

        public T Data { get; set; }

        public static BaseResult<T> CreateOkResult(T data)
        {
            var ok = new BaseResult<T>
            {
                Result = "ok",
                ErrMsg = "",
                Data = data
            };

            return ok;
        }

        public static BaseResult<T> CreateFailResult(T data, string errMessage)
        {
            var ok = new BaseResult<T>
            {
                Result = "fail",
                ErrMsg = errMessage,
                Data = data
            };

            return ok;
        }
    }
}