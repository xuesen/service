using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Services.Maintain.Interface
{
    [Service]
    public interface IBopBaseService : IService<SBopBase, SBopBaseDTO>
    {
        [Operation]
        object GetPart();

        [Operation]
        object GetLastestVersionWorkflow(int wordkflowid);

        [Operation]
        object GetBomVersion(int partid);

        [Operation]
        object GetBomData(dynamic request);

        [Operation]
        object GetPartInofo(dynamic request);

        [Operation]
        object CallBopBaseSP(BopBaseRequestDTO request);

        [Operation]
        object CheckBeforePublish(int bopId);

        [Operation]
        object Publish(BopBaseRequestDTO request);

        [Operation]
        string ExportBopContent(int bopBaseId);

        [Operation]
        void UpdateUdt(int id);

        [Operation]
        object GetKeyPart(int familyId);
    }
}
