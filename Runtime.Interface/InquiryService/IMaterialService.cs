using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Interface.DTO;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Runtime.Interface
{
    [Service]
    public interface IMaterialService
    {
        [Operation]
        MaterialDTO GetPartByReel(string inputno);

        [Operation]
        MaterialDTO GetPartByPart(string inputno);
    }
}
