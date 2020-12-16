using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Plant;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Repository;
using IIMes.Services.Runtime.Model.Process;
using System.Linq;
using IIMes.Services.Runtime.Model.Product;
using System.Collections.Generic;
using IIMes.Infrastructure.Exception;

namespace IIMes.Services.Runtime.Services.Spec
{

    public class EquipmentMaterialPreparingCollectionBuilder : ICollectionSpecBuilder
    {
        private readonly IRepository<Terminal> _repTerminal;
        private readonly BopRepository _repBop;
        private readonly IRepository<TerminalParts> _repTerminalParts;
        private readonly IRepository<WoFsDetail> _repWoFsDetail;
        private readonly IRepository<WoFsSub> _repWoFsSub;
        private readonly IRepository<Part> _repPart;
        private readonly IRepository<Material> _repMaterial;
        public EquipmentMaterialPreparingCollectionBuilder(
            IRepository<Terminal> repTerminal,
            BopRepository repBop, IRepository<TerminalParts> repTerminalParts,
            IRepository<WoFsDetail> repWoFsDetail,
            IRepository<WoFsSub> repWoFsSub,
            IRepository<Part> repPart,
            IRepository<Material> repMaterial)
        {
            _repTerminal = repTerminal;
            _repBop = repBop;
            _repTerminalParts = repTerminalParts;
            _repWoFsDetail = repWoFsDetail;
            _repWoFsSub = repWoFsSub;
            _repPart = repPart;
            _repMaterial = repMaterial;
        }

        public ICollectionSpec Build(int terminalId, Bop bop)
        {
            var ret = new EquipmentMaterialPreparingCollectionSpec(_repTerminalParts);
            var terminal = _repTerminal.Get(terminalId);
            var bindingParts = _repTerminalParts.GetTerminalPartsByTerminalId(terminalId);
            if (bop.Fs == null)
            {
                throw new BizException("CHK028");
            }            
            var fsDetails = _repWoFsDetail.GetWoFsDetail(bop.Fs.Id);
            foreach (var fsDetail in fsDetails)
            {
                var fsSubs =  _repWoFsSub.Query().Where(p => p.WoFsDetailId == fsDetail.Id).ToList();
                var parts = new List<Part>();
                foreach (var fsSub in fsSubs)   
                {
                    var x = _repPart.Get(fsSub.SubPartId);
                    var part = new Part(){
                        Id = x.Id,
                        PartNo = x.PartNo,
                        PartName = x.PartName,
                    };
                    parts.Add(part);
                }
    
                var mainPart = _repPart.Get(fsDetail.ItemPartId);
                mainPart = new Part(){
                        Id = mainPart.Id,
                        PartNo = mainPart.PartNo,
                        PartName = mainPart.PartName,
                    };
                parts.Add(mainPart);

                // var values = bindingParts.Where(o => o.ItemId == fsDetail.Id);
                var equipmentId = bop.Fs.EquipmentId;
                var values = bindingParts.Where(o => o.EquipmentId == equipmentId && o.Station == fsDetail.Station);
                var ItemValues = (from value in values
                                  let itemValue = new TerminalPartsByBom
                                  {
                                      Id = value.Id,
                                      ItemId = value.ItemId,
                                      CandidateId = value.ScanPartId,
                                      Value = value.ScanNo,
                                      Sequence = value.Sequence,
                                      ScanPartNo = value.ScanPartNo,
                                      Station = value.Station,
                                      ItemPartId = value.ItemPartId,
                                      Amount = _repMaterial.GetPartByReel(value.ScanNo).Amount,
                                      LeftAmount = _repMaterial.GetPartByReel(value.ScanNo).LeftAmount
                                  }
                                  select itemValue).ToList();
                var item = new CollectionSpecItem
                {
                    Id = fsDetail.Id,
                    Location = fsDetail.Location,
                    MainCandidateId = fsDetail.ItemPartId,
                    Qty = fsDetail.ItemCount,
                    Station = fsDetail.Station,
                    Candidates = parts.ToList<ICollectionSpecItemCandidate>(),
                    Values = ItemValues.ToList<ICollectionSpecItemValue>(),
                    FeederType = fsDetail.FeederType
                };
                ret.AddItem(item);
            }
            ret.Wo = bop.Wo;
            ret.TerminalId = terminalId;
            return ret;
        }
    }
}