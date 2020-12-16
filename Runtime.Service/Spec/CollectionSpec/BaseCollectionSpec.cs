using System.Collections.Generic;
using System.Linq;

namespace IIMes.Services.Runtime.Model.Process
{
    public abstract class BaseCollectionSpec : ICollectionSpec
    {
        public virtual IList<ICollectionSpecItem> CollectionSpecItems { get; set; } = new List<ICollectionSpecItem>();
        public virtual ICollectionSpecItem GetItem(int itemId)
        {
            var ret = CollectionSpecItems.Where(o => o.Id == itemId).FirstOrDefault();
            return ret;
        }

        public virtual void AddItem(ICollectionSpecItem item)
        {
            CollectionSpecItems.Add(item);
        }

        public abstract void ClearItem(int itemId);
        public abstract int CollectSpecItem(int itemId, int candidateId, object value, string editor, string station, int? equipmentId);
        public abstract void RemoveItem(int itemId, int valueId);
    }
}

