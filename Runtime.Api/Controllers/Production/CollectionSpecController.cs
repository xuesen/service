using IIMes.Infrastructure.BaseController;
using IIMes.Services.Runtime.Interface;
using IIMes.Services.Runtime.Interface.DTO;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Repository;
using IIMes.Services.Runtime.Services;
using Microsoft.AspNetCore.Mvc;

namespace IIMes.Services.Runtime.Controllers
{
    public class CollectionSpecController : BaseController
    {
        private readonly BopRepository _repBop;
        private readonly SpecManager _specmanager;

        public CollectionSpecController(BopRepository bopRepository, SpecManager specmanager)
        {
            _repBop = bopRepository;
            _specmanager = specmanager;
        }

        [HttpGet("{specType}")]
        public virtual ActionResult GetSpec([FromRoute]string specType, int terminalId, string wo, int? equipmentId)
        {
            var spec = GetSpecByType(specType, terminalId, wo, equipmentId);
            return Ok(spec);
        }

        [HttpPost("{specType}")]
        public virtual ActionResult CollectSpecItemValue([FromRoute]string specType, CollectionSpecItemValueDTO value)
        {
            var spec = GetSpecByType(specType, value.TerminalId, value.Wo, value.EquipmentId);
            var ret = spec.CollectSpecItem(value.ItemId.Value, value.CandidateId, value.Value, value.Editor, value.Station, value.EquipmentId);
            return Ok(ret);
        }

        [HttpDelete("{specType}")]
        public virtual ActionResult RemoveSpecItemValue([FromRoute]string specType, CollectionSpecItemValueDTO value)
        {
            var spec = GetSpecByType(specType, value.TerminalId, value.Wo, value.EquipmentId);

            if (value.ValueId == null && value.ItemId == null)
            {
                foreach (var item in spec.CollectionSpecItems)
                {
                    spec.ClearItem(item.Id);
                }
            }
            else if (value.ValueId == null)
            {
                spec.ClearItem(value.ItemId.Value);
            }
            else
            {
                spec.RemoveItem(value.ItemId.Value, value.ValueId.Value);
            }

            return Ok();
        }

        private ICollectionSpec GetSpecByType(string specType, int terminalId, string wo, int? equipmentId)
        {
            Bop bop;
            // SMT根据wo，equipment; 工单根据wo
            if (specType == "BomMaterialPreparingCollection")
            {
                bop = _repBop.GetBopByWo(wo);
            }
            else
            {
                bop = _repBop.GetBopByWoAndEq(wo, equipmentId.Value);
            }

            var spec = _specmanager.GetCollectionSpec(specType, terminalId, bop);
            return spec;
        }
    }
}