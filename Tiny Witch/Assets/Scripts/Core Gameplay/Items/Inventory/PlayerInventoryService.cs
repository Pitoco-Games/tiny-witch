using CoreGameplay.Items;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Utils.Save;

namespace CoreGameplay.Items.Inventory
{
    public class PlayerInventoryService : Inventory
    {
        private InventorySaveData saveData;

        public static async UniTask<PlayerInventoryService> Create(SaveService saveService)
        {
            var inventorySaveData = saveService.GetSavedData<InventorySaveData>() as InventorySaveData;
            var playerInventory = new PlayerInventoryService(inventorySaveData);

            List<Item> items = await ItemAddressablesProvider.LoadItemAddressables(inventorySaveData.itemAmounts.Keys.ToArray());
            foreach(Item item in items)
            {
                int savedItemAmount = inventorySaveData.itemAmounts[item.Id];
                playerInventory.AddItem(item, savedItemAmount);
            }

            return playerInventory;
        }
        
        public PlayerInventoryService(InventorySaveData saveData)
        {
            this.saveData = saveData;
            base.OnItemAmountChanged += saveData.UpdateInventorySaveData;
        }

        ~PlayerInventoryService()
        {
            base.OnItemAmountChanged -= saveData.UpdateInventorySaveData;
        }
    }
}