using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IWorkflowService : IService<SWorkflow, SWorkflowDTO>
    {
        [Operation]
        object SaveWorkflow(dynamic request);

        [Operation]
        void DeleteWorkflowAndWorkflowStep(int id);

        [Operation]
        object CopyList();

        [Operation]
        object CheckBeforePublish(SWorkflowBaseDTO request);

        [Operation]
        void Publish(WorkflowRequestDTO request);

        [Operation]
        object CheckBeforeAdd(dynamic request);

        [Operation]
        IList<CommonDTO> GetProcessList(int workflowid);
    }
}
