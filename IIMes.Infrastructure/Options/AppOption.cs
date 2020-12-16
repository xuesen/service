using Microsoft.Extensions.Options;

namespace IIMes.Infrastructure.Options
{
    public class AppOption : IOptions<AppOption>
    {
        public AppOption Value => this;

        public string AppName { get; set; }

        public string Language { get; set; }
    }
}
