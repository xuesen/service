using System;
using System.Collections.Generic;
using IIMes.Infrastructure.Service;
using IIMes.Infra.Privilege.Model;
using ProtoBuf.Grpc.Configuration;

namespace IIMes.Infra.Privilege.Interface
{
    [Service]
    public interface IEmployeeService : IService<Employee, EmployeeDTO>
    {
        LoginDTO GetEmployee(LoginRequestDTO request); 

        [Operation]
        void ModifyPassword(int id, EmployeeRequestDTO request);  

        [Operation]
        IList<EmployeeDTO> GetEmployeeByManager(string manager);     
    }
}
