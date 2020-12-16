using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Interface.DTO;
using ProtoBuf.Grpc.Configuration;
using IIMes.Services.Runtime.Model.Plant;

namespace IIMes.Services.Runtime.Interface
{
    [Service]
    public interface ITerminalService : IService<Terminal, TerminalDTO>
    {
    }
}
