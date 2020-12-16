using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Interface.DTO;
using ProtoBuf.Grpc.Configuration;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Infrastructure.Aspect;

namespace IIMes.Services.Runtime.Interface
{
    [Service]
    public interface IFeedingService
    {
        [Operation]
        object ConfirmFeeding(FeedingRequestDTO requestDTO);

        [Operation]
        object QueryFsStatus(string wo, int terminalId);

        [Operation]
        object UpdateFsStatus(FeedingRequestDTO requestDTO);

        [Operation]
        bool Check(FeedingRequestDTO requestDTO);

        [Operation]
        object UnfeedingForSMTShow(FeedingRequestDTO requestDTO);

        [Operation]
        object UnfeedingForBomShow(FeedingRequestDTO requestDTO);

        [Operation]
        void Unfeeding(TerminalPartsBySMT requestDTO);

        [Operation]
        bool CheckWoBom(FeedingRequestDTO requestDTO);
    }
}
