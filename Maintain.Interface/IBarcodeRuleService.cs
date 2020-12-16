using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IBarcodeRuleService : IService<SBarcodeRule, SBarcodeRuleDTO>
    {
        [Operation]
        IList<SBarcodeRuleDTO> GetAll(string ruletype);
    }
}
