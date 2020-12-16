using System.Collections.Generic;

namespace IIMes.Services.Runtime.Model.Process
{
    public interface ICollectionSpec
    {
        IList<ICollectionSpecItem> CollectionSpecItems { get; set; }
        ICollectionSpecItem GetItem(int itemId);
        void AddItem(ICollectionSpecItem item);

        void ClearItem(int itemId);
        int CollectSpecItem(int itemId, int candidateId, object value, string editor, string station, int? equipmentId);
        void RemoveItem(int itemId, int valueId);
    }
}