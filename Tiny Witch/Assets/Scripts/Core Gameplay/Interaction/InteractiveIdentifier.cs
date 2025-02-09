using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEditor;

namespace CoreGameplay.Interaction
{
    public class InteractiveIdentifier : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float detectionRadius;

        public event Action OnNewInteractableDetected;

        private IInteractable interactableInRange;
        private Transform thisTransform;

        public IInteractable InteractableInRange => interactableInRange;

        private void Awake()
        {
            thisTransform = transform;
        }

        private void Update()
        {
            CheckForInteractables();
        }

        private void CheckForInteractables()
        {
            Vector2 detectionRangeCenter = GetDetectionRadiusCenter(playerController.LastMoveDirection);

            Collider2D[] collisions = Physics2D.OverlapCircleAll(detectionRangeCenter, detectionRadius);

            foreach (Collider2D collision in collisions)
            {
                IInteractable interactable = collision.GetComponent<IInteractable>();

                if(interactable != null)
                {
                    interactableInRange = interactable;
                    return;
                }
            }

            interactableInRange = null;
        }

        private Vector2 GetDetectionRadiusCenter(Vector2 directionToCheck)
        {
            return (Vector2)thisTransform.position + directionToCheck * detectionRadius*2;
        }

        public bool GetHasInteractibleInRange()
        {
            return interactableInRange != null;
        }

        #region debug
        public void OnDrawGizmosSelected()
        {
            thisTransform = transform;
            Vector2 direction = playerController.LastMoveDirection == Vector2.zero ? Vector2.down : playerController.LastMoveDirection;

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(GetDetectionRadiusCenter(direction), detectionRadius);
        }
        #endregion
    }
}