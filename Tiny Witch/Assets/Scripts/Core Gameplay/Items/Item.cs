using UnityEngine;

namespace CoreGameplay.Items
{
    [CreateAssetMenu(fileName = "Item_ItemName", menuName = "ScriptableObjects/Inventory/Item")]
    public class Item : ScriptableObject
    {
        public string Id;
        public string Description;
        public Sprite Icon;
    }
}

