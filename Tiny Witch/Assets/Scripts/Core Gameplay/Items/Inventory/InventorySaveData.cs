using System.Collections.Generic;
using Utils;
using Utils.Save;

namespace CoreGameplay.Items.Inventory
{
    public class InventorySaveData : SaveData
    {
        public Dictionary<string, int> itemAmounts = new();

        public void UpdateInventorySaveData(ItemAmount currentItemAmount)
        {
            if (currentItemAmount.Amount == 0)
            {
                itemAmounts.Remove(currentItemAmount.Item.Id);
            }
            else if (itemAmounts.TryGetValue(currentItemAmount.Item.Id, out int amount))
            {
                amount = currentItemAmount.Amount;
            }
            else
            {
                itemAmounts.Add(currentItemAmount.Item.Id, currentItemAmount.Amount);
            }
            
            ServicesLocator.Get<SaveService>().Save(this);
            base.UpdateSaveData();
        }
    }
}