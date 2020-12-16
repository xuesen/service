using System;
using System.Collections.Generic;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IWoFsService
    {
        [Operation]
        IList<WoFsInfoDTO> GetWoFsInfo(WoFsRequestDTO wofsrequestdto);

        [Operation]
        bool CheckWoStatus(int wofsid);

        [Operation]
        IList<CommonWoFsDetailDTO> GetWoFsDetailInfo(int wofsid);

        [Operation]
        bool CheckWoFsData(ImportWoFsRequestDTO requestdto);

        [Operation]
        void ImportPartFs2WoFs(ImportWoFsRequestDTO requestdto);

        [Operation]
        void SaveWoFsDetailInfo(WoFsDetailRequestDTO wofsdetailrequest, int type);

        [Operation]
        IList<WoBomPartDTO> GetWoBomPart(string workorder);

        // [Operation]
        void DeleteWoFsDetail(int id);

        // [Operation]
        // void SaveWoFsSub(dynamic request);

        // [Operation]
        // void DeleteWoFsSub(dynamic request);
    }
}
