using System;
using System.Collections.Generic;
using System.IO;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IBomService : IService<SBom, SBomDTO>
    {
        [Operation]
        object ImportBomExcel(Stream stream);
    }
}
