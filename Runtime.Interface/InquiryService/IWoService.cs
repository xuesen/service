using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Interface.DTO;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Runtime.Interface
{
    [Service]
    public interface IWoService
    {
        [Operation]
        WoDTO GetWo(string wo, string statusname);

        [Operation]
        IList<WoDTO> Query(WoRequestDTO wostatus);
    }
}
