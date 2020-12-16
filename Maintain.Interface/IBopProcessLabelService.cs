using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IBopProcessLabelService : IService<SBopProcessLabel, SBopProcessLabelDTO>
    {
        object Search(BopProcessRequestDTO requestDto);
    }
}
