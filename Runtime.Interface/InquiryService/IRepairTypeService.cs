using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Interface.DTO;
using ProtoBuf.Grpc.Configuration;
using IIMes.Services.Runtime.Model.Quality;

namespace IIMes.Services.Runtime.Interface
{
    [Service]
    public interface IRepairTypeService : IService<RepairType, CommonDTO>
    {
    }
}
