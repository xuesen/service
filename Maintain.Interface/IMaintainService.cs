using System;
using System.Collections.Generic;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IMaintainService
    {
        [Operation]
        IList<CommonDTO> GetCommonInfo(string tableName);

        [Operation]
        IList<PartsDTO> GetParts();

        [Operation]
        IList<PartMarketDTO> GetMarketParts();

        [Operation]
        IList<PartMarketDTO> GetCatchParts();

        [Operation]
        IList<CommonDTO> GetSetting(string program);

        [Operation]
        IList<CommonDTO> GetEquipmentbyLine(int lineid);
    }
}
