using UnityEngine;

namespace CoreGameplay.Controls.Interaction
{
    public class DialogueInteractable : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("Interacted!!!");
        }
    }
}