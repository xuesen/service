using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IWoService : IService<WoBase, WoDTO>
    {
        [Operation]
        IList<WoDTO> Query(WoRequestDTO woRequest);

        [Operation]
        int AddWo(WoBaseRequestDTO woBaseRequest);

        [Operation]
        int SaveWo(WoBaseRequestDTO woBaseRequest);

        [Operation]
        void UpdateWoStatus(WoStatusRequestDTO woRequest);

        [Operation]
        void SortWo(WoSortRequestDTO woRequest);
    }
}
