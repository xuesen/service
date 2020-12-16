using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using IIMes.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IIMes.Infrastructure.Exception
{
    public class ResourceMessageProvider : IMessageProvider
    {
        private readonly IDictionary<string, string> _messages = new Dictionary<string, string>();

        public ResourceMessageProvider(Assembly resourceAssembly, IOptions<AppOption> option)
        {
            var language = option.Value.Language;
            var fileName = $"{resourceAssembly.GetName().Name}.{language}.json";
            var names = resourceAssembly.GetManifestResourceNames();
            if (names != null && names.Contains(fileName))
            {
                using var stream = resourceAssembly.GetManifestResourceStream(fileName);
                using var sR = new StreamReader(stream);
                var text = sR.ReadToEnd();
                sR.Close();
                var objs = JsonConvert.DeserializeObject<JArray>(text);
                foreach (var obj in objs)
                {
                    var code = obj["code"]?.ToString();
                    if (!string.IsNullOrEmpty(code))
                    {
                        _messages[code] = obj["message"].ToString();
                    }
                }
            }
        }

        public string GetMessage(string errCode)
        {
            if (_messages.TryGetValue(errCode, out string message))
            {
                return message;
            }

            return string.Format("No message for {0}", errCode);
        }
    }
}
