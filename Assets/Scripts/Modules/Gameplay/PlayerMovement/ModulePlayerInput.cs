using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
    MORE_OPTIONS,
    INVENTORY,
    INTERACT
}

public class ModulePlayerInput : Module
{
    public ModuleMovement Movement;
    public ModulePlayerInventory Inventory;
    private PlayerAction _playerActionSelected;

    private int _actionIndex;

    private void Awake()
    {
        if (Movement == null)
            Movement = this.GetComponent<ModuleMovement>();
        if (Inventory == null)
            Inventory = this.GetComponent<ModulePlayerInventory>();
    }

    private void Start()
    {
        _playerActionSelected = PlayerAction.INTERACT;
        _actionIndex = (int)_playerActionSelected;
        GameEvents.PlayerActionSelected(_playerActionSelected);
    }

    private void Update()
    {
        if (UIController.Instance.IsUIActive())
            return;
        MoveInputs();
        OtherInputs();
    }

    private void MoveInputs()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Movement.SetTargetMovement(Vector3.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Movement.SetTargetMovement(Vector3.down);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Movement.SetTargetMovement(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Movement.SetTargetMovement(Vector3.right);
        }
    }

    private void OtherInputs()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            GameEvents.PlayAudio(AudioNames.CLICK_01);
            GameEvents.SwapTopInfo();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameEvents.PlayAudio(AudioNames.CLICK_01);
            CycleActions();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameEvents.PlayAudio(AudioNames.CLICK_01);
            ApplyAction();
        }
    }

    private void CycleActions()
    {
        var eActions = (PlayerAction[])System.Enum.GetValues(typeof(PlayerAction));
        _actionIndex++;
        if (_actionIndex >= eActions.Length)
            _actionIndex = 0;
        _playerActionSelected = eActions[_actionIndex];
        GameEvents.PlayerActionSelected(_playerActionSelected);
    }

    private void ApplyAction()
    {
        switch (_playerActionSelected)
        {
            case PlayerAction.MORE_OPTIONS:
                GameEvents.ShowMoreOptions();
                break;
            case PlayerAction.INVENTORY:
                Inventory.ShowInventory();
                break;
            case PlayerAction.INTERACT:
                var interactale = Movement.GetInteractable();
                if (interactale != null)
                    interactale.Interact();
                break;
            default:
                break;
        }
    }

}
