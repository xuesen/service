using System;
using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Infrastructure.Exception;
using IIMes.Services.Runtime.Model.Product;
using IIMes.Services.Runtime.Model.Production;
using IIMes.Services.Runtime.Repository;

namespace IIMes.Services.Runtime.Model.Process
{
    public class EquipmentMaterialPreparingCollectionSpec : BaseCollectionSpec
    {
        private IRepository<TerminalParts> _repTerminalParts;
        public virtual string Wo { get; set; }
        public virtual int TerminalId { get; set; }

        public EquipmentMaterialPreparingCollectionSpec(IRepository<TerminalParts> repTerminalParts)
        {
            _repTerminalParts = repTerminalParts;
        }

        public override int CollectSpecItem(int itemId, int candidateId, object value, string editor, string station, int? equipmentId)
        {
            var item = (CollectionSpecItem)GetItem(itemId);
            //check

            //save
            var maxSquence = 0;
            if (item.Values.Count > 0)
            {
                maxSquence = item.Values.Select(o => o.Sequence).Max();
            }
            var newTerminalParts = new TerminalParts
            {
                TerminalId = TerminalId,
                // Wo = Wo,
                Editor = editor,
                ItemId = itemId,
                Sequence = maxSquence + 1,
                ItemPartId = item.MainCandidateId,
                ItemPartNo = item.MainCandidate.MatchPattern,
                ScanPartId = candidateId,
                ScanPartNo = item.Candidates.Where(o => o.Id == candidateId).FirstOrDefault().MatchPattern,
                ScanNo = (string)value,
                Station = station,
                EquipmentId = equipmentId

            };
            _repTerminalParts.Add(newTerminalParts);
            return newTerminalParts.Id;
        }

        public override void ClearItem(int itemId)

        {
            var item = GetItem(itemId);
            var ids = item.Values.Select(o => o.Id).ToList();
            _repTerminalParts.DeleteByIds(ids);
        }

        public override void RemoveItem(int itemId, int valueId)
        {
            _repTerminalParts.DeleteById(valueId);
        }
    }
}