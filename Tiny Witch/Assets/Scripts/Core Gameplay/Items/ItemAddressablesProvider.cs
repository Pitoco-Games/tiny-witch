
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AddressableAssets;

namespace CoreGameplay.Items
{
    public static class ItemAddressablesProvider
    {
        private const string ITEM_ADDRESSABLE_PATH = "Assets/ScriptableObject/Items/";
        private const string ITEM_ADDRESSABLE_SUFFIX = ".asset";
        private const string ITEM_ADDRESSABLE_PREFIX = "Item_";

        public static async UniTask<Item> LoadItemAddressable(string name)
        {
            return await Addressables.LoadAssetAsync<Item>(GetAddressableKey(name)).Task;
        }

        public static async UniTask<List<Item>> LoadItemAddressables(string[] names)
        {
            List<string> addressableKeys = names.ToList();
            List<Item> items = new();

            if (names.Length > 0)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    addressableKeys[i] = GetAddressableKey(names[i]);
                }

                await Addressables.LoadAssetsAsync<Item>(addressableKeys, OnItemLoaded, Addressables.MergeMode.Union);
            }

            return items;

            void OnItemLoaded(Item item)
            {
                items.Add(item);
            }
        }

        private static string GetAddressableKey(string name)
        {
            return ITEM_ADDRESSABLE_PATH + ITEM_ADDRESSABLE_PREFIX + name + ITEM_ADDRESSABLE_SUFFIX;
        }
    }
}