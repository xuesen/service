using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Infra.Privilege.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Infra.Privilege.Interface
{
    [Service]
    public interface IDepartmentService : IService<Department, DepartmentDTO>
    {
        [Operation]
        IList<DepartmentDTO> GetDepartment(string departmentname);
    }
}
