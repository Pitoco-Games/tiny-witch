using UnityEngine;

namespace CoreGameplay.Controls.Interaction
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private InteractiveIdentifier interactiveIdentifier;

        public void Interact()
        {
            if(!interactiveIdentifier.GetHasInteractibleInRange())
            {
                return;
            }

            IInteractable interactable = interactiveIdentifier.InteractableInRange;
            interactable.Interact();
        }
    }
}