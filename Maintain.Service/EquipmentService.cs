// using System.Net.Cache;
// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using IIMes.Services.Core.Model;
// using IIMes.Services.Maintain.Model;
// using MassTransit;
// using MassTransit.Util;
// using IIMes.Services.Core.Message;
using AutoMapper;
using IIMes.Infrastructure.Aspect;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Infrastructure.Service;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;

namespace IIMes.Services.Maintain.Services
{
    public partial class EquipmentService : BaseMaintainService<SEquipment, EquipmentDTO>, IEquipmentService
    {
        public EquipmentService(IRepository<SEquipment> rep, IMapper mapper)
        : base(rep, mapper)
        {
        }
    }
}
