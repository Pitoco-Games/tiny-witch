using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Movement movementController;

    [Header("Controls")]
    [SerializeField] private InputActionAsset actionAsset;
    private InputAction moveAction;
    private InputAction lookAction;

    private Vector2 lastMoveInput;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

        InputActionMap playerActionMap = actionAsset.FindActionMap("Player");
        moveAction = playerActionMap.FindAction("Move");
    }

    private void OnEnable()
    {
        actionAsset.Enable();
    }

    private void Update()
    {
        lastMoveInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        movementController.Move(lastMoveInput);
    }
}
