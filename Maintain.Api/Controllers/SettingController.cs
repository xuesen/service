using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IIMes.Infrastructure.BaseController;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Query;
using IIMes.Services.Core.Model;
using IIMes.Services.Maintain.Interface;
using IIMes.Services.Maintain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIMes.Services.Maintain.Controllers
{
    public class SettingController : BaseController<SSetting, SSettingDTO>
    {
        private IRepository<SSetting> _repository;

        public SettingController(ILogger<SSetting> logger, ISettingService settingService, IRepository<SSetting> repository)
        : base(logger, settingService)
        {
            _repository = repository;
        }

        [Route("query/{program}")]
        [HttpPost]
        public virtual ActionResult Program([FromRoute] string program)
        {
            var ret = SettingRepositoryExension.Program(_repository, program);

            var list = new List<SSettingDTO>();
            foreach (var item in ret)
            {
                var dto = new SSettingDTO();
                dto.Id = item.Id;
                dto.Name = item.Name;
                list.Add(dto);
            }

            return Ok(list);
        }
    }
}