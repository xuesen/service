using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace IIMes.Infra.Privilege.Api.Options
{
    public class GrpcServiceOptions : IOptions<GrpcServiceOptions>
    {
        public GrpcServiceOptions Value => this;
        public IDictionary<string, string> Address { get; set; }
    }
}
