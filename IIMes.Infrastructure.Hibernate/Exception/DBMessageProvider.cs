using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace IIMes.Infrastructure.Hibernate.Exception
{
    public class DBMessageProvider : IMessageProvider
    {
        private readonly IRepository<ExceptionMessage> _rep;

        private readonly IOptions<AppOption> _option;

        public DBMessageProvider(IRepository<ExceptionMessage> rep, IOptions<AppOption> option)
        {
            _rep = rep;
            _option = option;
        }

        public string GetMessage(string errCode)
        {
            var appName = _option.Value.AppName;
            var language = _option.Value.Language;

            var messages = _rep.Query().Where(o => o.Code == errCode).ToList();
            if (messages == null || messages.Count == 0)
            {
                return $"No message for {errCode}";
            }

            // 如果配置文件里配置了appName，language则需要ErrorMessage表也存在对应字段
            if (!string.IsNullOrEmpty(appName))
            {
                messages = messages.Where(o => o.AppName == appName).ToList();
            }

            if (!string.IsNullOrEmpty(language))
            {
                messages = messages.Where(o => o.LanguageCode == language).ToList();
            }

            var message = messages.FirstOrDefault();
            if (message == null)
            {
                return $"No message for {errCode}";
            }

            return message.Message;
        }
    }
}
