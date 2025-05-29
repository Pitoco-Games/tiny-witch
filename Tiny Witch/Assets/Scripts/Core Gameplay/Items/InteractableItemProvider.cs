using CoreGameplay.Controls.Interaction;
using CoreGameplay.Items.Inventory;
using UnityEngine;
using Utils;

namespace CoreGameplay.Items
{
    public class InteractableItemProvider : MonoBehaviour, IInteractable
    {
        [SerializeField] private Item item;
        [SerializeField] private int amount;

        public void Interact()
        {
            ServicesLocator.Get<PlayerInventoryService>().AddItem(item, amount);
        }
    }
}