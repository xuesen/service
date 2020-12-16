using System;
using System.Collections.Generic;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Plant;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Repository;

namespace IIMes.Services.Runtime.Model.Process
{

    public class QcDefectCollectionBuilder : ICollectionSpecBuilder
    {
        private readonly IRepository<Terminal> _repTerminal;
        private readonly BopRepository _repBop;
        private readonly IRepository<TerminalParts> _repTerminalParts;
        public QcDefectCollectionBuilder(
            IRepository<Terminal> repTerminal,
            BopRepository repBop, IRepository<TerminalParts> repTerminalParts)
        {
            _repTerminal = repTerminal;
            _repBop = repBop;
            _repTerminalParts = repTerminalParts;
        }

        public ICollectionSpec Build(int terminalId, Bop bop)
        {
            var ret = new QcDefectCollectionSpec(_repTerminalParts);
            var terminal = _repTerminal.Get(terminalId);
            var bindingParts = _repTerminalParts.GetTerminalPartsByTerminalId(terminalId);
            var processBopBom = bop.BopBom.Where(o => o.ProcessId == terminal.Process.Id);
            var processWoBom = bop.WoBom.Where(o => o.ProcessId == terminal.Process.Id);
            foreach (var bopBomItem in processBopBom)
            {
                var woBomItem = processWoBom.Where(o => o.SubPart.Id == bopBomItem.ItemPartId).FirstOrDefault();
                var parts = processWoBom.Where(o => o.SubPartGroup == woBomItem.SubPartGroup).Select(o => o.SubPart).ToList();
                var values = bindingParts.Where(o => o.ItemId == bopBomItem.Id);
                var ItemValues = (from value in values
                                  let itemValue = new TerminalParts
                                  {
                                      Id = value.Id,
                                      ItemId = value.ItemId,
                                      CandidateId = value.ScanPartId,
                                      Value = value.ScanNo,
                                      Sequence = value.Sequence
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
                    Values = ItemValues.ToList<ICollectionSpecItemValue>()
                };
                ret.AddItem(item);
            }
            return ret;
        }
    }
}