using UnityEngine;

namespace CoreGameplay.Interaction
{
    public class DialogueInteractable : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interacted!!!");
        }
    }
}