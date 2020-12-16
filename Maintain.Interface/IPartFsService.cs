using System;
using System.Collections.Generic;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IPartFsService
    {
        [Operation]
        IList<PartFsDetailInfoDTO> GetPartFsDetail(PartFsDTO partfsdto);

        [Operation]
        object CheckPartWo(int partid);

        [Operation]
        bool CheckPartFs(PartFsDTO partfsdto);

        [Operation]
        int ImportPartFs(PartFsDTO partfsdto);
    }
}
