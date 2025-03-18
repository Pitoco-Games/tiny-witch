using CoreGameplay.Controls.Interaction;
using CoreGameplay.Controls.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CoreGameplay.Controls
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MovementController movementController;
        [SerializeField] private Interactor interactor;

        [Header("Controls")]
        [SerializeField] private InputActionAsset actionAsset;
        private InputAction moveAction;
        private InputAction interactAction;

        private Vector2 lastMoveInput;
        private Camera mainCamera;

        public Vector2 LastMoveDirection { get; private set; }

        public InputAction MoveAction => moveAction;

        private void Awake()
        {
            mainCamera = Camera.main;

            InputActionMap playerActionMap = actionAsset.FindActionMap("Player");
            moveAction = playerActionMap.FindAction("Move");
            interactAction = playerActionMap.FindAction("Interact");

            interactAction.performed += Interact;
        }

        private void OnEnable()
        {
            actionAsset.Enable();
        }

        private void Update()
        {
            lastMoveInput = moveAction.ReadValue<Vector2>();

            if (lastMoveInput != Vector2.zero)
            {
                LastMoveDirection = lastMoveInput;
            }
        }

        private void FixedUpdate()
        {
            movementController.Move(lastMoveInput);
        }

        private void Interact(InputAction.CallbackContext context)
        {
            interactor.Interact();
        }
    }
}