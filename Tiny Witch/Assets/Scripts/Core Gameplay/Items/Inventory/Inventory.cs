using CoreGameplay.Items;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace CoreGameplay.Items.Inventory
{
    public class Inventory
    {
        protected Dictionary<string, ItemAmount> itemNameToItemAmountDict = new();
        public event Action<ItemAmount> OnItemAmountChanged;

        public async UniTask AddItem(string name, int amount)
        {
            Item item = await ItemAddressablesProvider.LoadItemAddressable(name);
            AddItem(item, amount);
        }

        public void AddItem(Item item, int amount)
        {
            if(itemNameToItemAmountDict.TryGetValue(item.Id, out ItemAmount itemAmount))
            {
                itemAmount.Amount += amount;
            }
            else
            {
                itemAmount = new ItemAmount
                {
                    Item = item,
                    Amount = amount
                };

                itemNameToItemAmountDict.Add(item.Id, itemAmount);
            }

            OnItemAmountChanged?.Invoke(itemAmount);
        }

        public ItemAmount TakeAllOfItem(string itemName)
        {
            if (!itemNameToItemAmountDict.TryGetValue(itemName, out ItemAmount itemAmount))
            {
                return null;
            }

            itemNameToItemAmountDict.Remove(itemName);
            OnItemAmountChanged?.Invoke(new ItemAmount { Item = itemAmount.Item, Amount = 0});

            return itemAmount;
        }

        public Item TakeItem(string itemName)
        {
            if (!itemNameToItemAmountDict.TryGetValue(itemName, out ItemAmount itemAmount))
            {
                return null;
            }

            itemAmount.Amount--;
            if(itemAmount.Amount <= 0)
            {
                itemNameToItemAmountDict.Remove(itemName);
            }

            OnItemAmountChanged?.Invoke(itemAmount);

            return itemAmount.Item;
        }
    }
}