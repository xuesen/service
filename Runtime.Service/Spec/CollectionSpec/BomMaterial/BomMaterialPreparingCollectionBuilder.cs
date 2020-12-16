using System;
using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Plant;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Repository;
using IIMes.Services.Runtime.Model.Process;
using IIMes.Services.Runtime.Model.Product;

namespace IIMes.Services.Runtime.Services.Spec
{

    public class BomMaterialPreparingCollectionBuilder : ICollectionSpecBuilder
    {
        private readonly IRepository<Terminal> _repTerminal;
        private readonly BopRepository _repBop;
        private readonly IRepository<TerminalParts> _repTerminalParts;
        private readonly IRepository<Material> _repMaterial;
        private readonly IRepository<Part> _repPart;

        public BomMaterialPreparingCollectionBuilder(
            IRepository<Terminal> repTerminal,
            BopRepository repBop, 
            IRepository<TerminalParts> repTerminalParts,
            IRepository<Part> repPart,
            IRepository<Material> repMaterial)
        {
            _repTerminal = repTerminal;
            _repBop = repBop;
            _repTerminalParts = repTerminalParts;
             _repMaterial = repMaterial;
             _repPart = repPart;
        }

        public ICollectionSpec Build(int terminalId, Bop bop)
        {
            var ret = new BomMaterialPreparingCollectionSpec(_repTerminalParts);
            var terminal = _repTerminal.Get(terminalId);
            var bindingParts = _repTerminalParts.GetTerminalPartsByTerminalId(terminalId);
            var processBopBom = bop.BopBom.Where(o => o.ProcessId == terminal.Process.Id);
            var processWoBom = bop.WoBom.Where(o => o.ProcessId == terminal.Process.Id);
            foreach (var bopBomItem in processBopBom)
            {
                var woBomItem = processWoBom.Where(o => o.SubPart.Id == bopBomItem.ItemPartId).FirstOrDefault();
                var parts = processWoBom.Where(o => o.SubPartGroup == woBomItem.SubPartGroup).Select(o => o.SubPart).ToList();
                parts = (from part in parts 
                        let x = new Part{
                            Id = part.Id,
                            PartNo = part.PartNo,
                            PartName = part.PartName
                        }
                        select x).ToList();

                var isExists = parts.Exists(p => p.Id == bopBomItem.ItemPartId);
                if (!isExists)
                {
                    var mainPart = _repPart.Get(bopBomItem.ItemPartId);
                    mainPart = new Part(){
                            Id = mainPart.Id,
                            PartNo = mainPart.PartNo,
                            PartName = mainPart.PartName,
                        };
                    parts.Add(mainPart);               
                }

                // var values = bindingParts.Where(o => o.ItemId == bopBomItem.Id);
                var values = bindingParts.Where(o => o.ItemPartId == bopBomItem.ItemPartId).OrderByDescending(o => o.Cdt);
                var ItemValues = (from value in values
                                  let itemValue = new TerminalPartsByBom
                                  {
                                      Id = value.Id,
                                      ItemId = value.ItemId,
                                      CandidateId = value.ScanPartId,
                                      Value = value.ScanNo,
                                      Sequence = value.Sequence,
                                      ScanPartNo = value.ScanPartNo,
                                      Amount = _repMaterial.GetPartByReel(value.ScanNo).Amount,
                                      LeftAmount = _repMaterial.GetPartByReel(value.ScanNo).LeftAmount
                                  }
                                  select itemValue).ToList();

                var item = new CollectionSpecItem
                {
                    Id = bopBomItem.Id,
                    Location = bopBomItem.Location,
                    MainCandidateId = bopBomItem.ItemPartId,
                    Qty = bopBomItem.Qty,
                    Station = null,
                    Candidates = parts.ToList<ICollectionSpecItemCandidate>(),
                    Values = ItemValues.ToList<ICollectionSpecItemValue>(),
                    LeftAmount = null,
                };
                ret.AddItem(item);
            }
            ret.Wo = bop.Wo;
            ret.TerminalId = terminalId;
            return ret;
        }
    }
}