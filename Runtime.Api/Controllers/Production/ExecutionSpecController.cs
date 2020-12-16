using System.Collections.Generic;
using IIMes.Infrastructure.BaseController;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Repository;
using IIMes.Services.Runtime.Services;
using Microsoft.AspNetCore.Mvc;
using IIMes.Services.Runtime.Model.Spec;

namespace IIMes.Services.Runtime.Controllers
{
    public class ExecutionSpecController : BaseController
    {
        private readonly BopRepository _repBop;
        private readonly SpecManager _specmanager;

        public ExecutionSpecController(BopRepository bopRepository, SpecManager specmanager)
        {
            _repBop = bopRepository;
            _specmanager = specmanager;
        }

        [HttpGet]
        public virtual ActionResult GetSpec(int terminalId, string wo)
        {
            var specList = GetSpecList(terminalId, wo);
            return Ok(specList);
        }

        [HttpPost]
        public virtual ActionResult Execute(ExecutionSpecDTO value)
        {
            var ret = new List<object>();
            var specList = GetSpecList(value.TerminalId, value.Wo);
            foreach (var spec in specList)
            {
                spec.Execute(value.TerminalId, value.Container, value.Values);

            }
            return Ok(ret);
        }

        [HttpGet("{specType}")]
        public virtual ActionResult GetSpec([FromRoute]string specType, int terminalId, string wo)
        {
            var specList = GetSpecList(terminalId, wo);
            return Ok(specList);
        }

        [HttpPost("{specType}")]
        public virtual ActionResult Execute([FromRoute]string specType, ExecutionSpecDTO value)
        {
            var ret = new List<object>();
            var specList = GetSpecList(value.TerminalId, value.Wo);
            foreach (var spec in specList)
            {
                spec.Execute(value.TerminalId, value.Container, value.Values);

            }
            return Ok(ret);
        }
        private IList<IExecutionSpec> GetSpecList(int terminalId, string wo)
        {
            var bop = _repBop.GetBopByWo(wo);
            var specList = _specmanager.GetExecutionSpec(terminalId, bop);
            return specList;
        }
        private IExecutionSpec GetSpecByType(string specType, int terminalId, string wo)
        {
            var bop = _repBop.GetBopByWo(wo);
            var spec = _specmanager.GetExecutionSpec(specType, terminalId, bop);
            return spec;
        }
    }
}