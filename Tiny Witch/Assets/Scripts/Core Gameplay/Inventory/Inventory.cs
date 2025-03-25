using System;
using System.Collections.Generic;
using Utils.Save;

namespace CoreGameplay.Inventory
{
    public class Inventory
    {
        protected Dictionary<string, ItemAmount> itemNameToItemAmountDict = new();
        public event Action<ItemAmount> OnItemAmountChanged;

        public void AddItem(Item item)
        {
            if(itemNameToItemAmountDict.TryGetValue(item.Id, out ItemAmount itemAmount))
            {
                itemAmount.Amount++;
                return;
            }

            itemNameToItemAmountDict.Add(item.Id,
                new ItemAmount
                {
                    Item = item,
                    Amount = 1
                });

            OnItemAmountChanged?.Invoke(itemAmount);
        }

        public ItemAmount TakeAllOfItem(Item item)
        {
            if (!itemNameToItemAmountDict.TryGetValue(item.Id, out ItemAmount itemAmount))
            {
                return null;
            }

            itemNameToItemAmountDict.Remove(item.Id);
            OnItemAmountChanged?.Invoke(new ItemAmount { Item = itemAmount.Item, Amount = 0});

            return itemAmount;
        }

        public Item TakeItem(Item item)
        {
            if (!itemNameToItemAmountDict.TryGetValue(item.Id, out ItemAmount itemAmount))
            {
                return null;
            }

            itemAmount.Amount--;
            if(itemAmount.Amount < 0)
            {
                itemNameToItemAmountDict.Remove(item.Id);
            }

            OnItemAmountChanged?.Invoke(itemAmount);

            return itemAmount.Item;
        }
    }
}