using System;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IBopProcessBomService : IService<SBopProcessBom, SBopProcessBomDTO>
    {
        object Search(BopProcessRequestDTO requestDto);
    }
}
