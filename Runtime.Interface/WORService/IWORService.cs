using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Runtime.Model;
using IIMes.Services.Runtime.Interface.DTO;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Runtime.Interface
{
    [Service]
    public interface IWORService
    {
        [Operation]
        bool SaveWos(string wo); // 开工，更新工单状态

        [Operation]
        bool CheckSMTFirstProcess(WorRequestDTO request);// 检查当前站为SMT IN（SMT段首站）,进行检查

        [Operation]
        bool CheckWorkflowStep(WorRequestDTO request);

        // 获取工单报工进度表信息
        WorStatusDTO GetWorStatus(WorRequestDTO request);

        [Operation]
        int SaveWor(WorRequestDTO request);

        [Operation]
        bool CheckLastProcess(WorRequestDTO request);// 检查当前站是否为最后一个工序,进行检查

        [Operation]
        void CheckWorRepair(string sn);
    }
}
