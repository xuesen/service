using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Interface.DTO;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Runtime.Interface
{
    [Service]
    public interface IWORRepairService
    {
        [Operation]
        WorRepairInfoDTO InputSN(WorRepairRequestDTO request);  
        
        [Operation]
        WorRepairItemDTO SaveRepair(WorRepairItemInfoDTO request);    

        [Operation]
        string FinishRepair(WorRepairFinishRequestDTO request);  

        [Operation]
        IList<WorDefectInfoDTO> SaveDefect(WorDefectRequestDTO request); 

        [Operation]
        IList<PartCheckDTO> GetDefectKeyParts(WorDefectKeypartsRequestDTO request); 

        [Operation]
        void SaveNewKeyParts(WorDefectKeypartsSaveRequestDTO request); 

        [Operation]
        IList<WorRepairHistoryDTO> GetRepairHistory(string sn); 

        [Operation]
        IList<WorKeyPartsItemDTO> GetKeyPartsHistory(string sn);

        // [Operation]
        void DeleteDefect(int id);

        // [Operation]
        void DeleteRepairInfo(int id);
    }
}
