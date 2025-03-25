using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Utils.Save;

namespace CoreGameplay.Inventory
{
    public class PlayerInventoryService : Inventory
    {
        private InventorySaveData saveData;

        public static async UniTask<PlayerInventoryService> Create(SaveService saveService)
        {
            var inventorySaveData = saveService.GetSavedData<InventorySaveData>() as InventorySaveData;
            var playerInventory = new PlayerInventoryService(inventorySaveData);

            await Addressables.LoadAssetsAsync<Item>(inventorySaveData.itemAmounts.Values, OnItemLoaded);

            return playerInventory;

            void OnItemLoaded(Item item)
            {
                playerInventory.AddItem(item);
            }
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