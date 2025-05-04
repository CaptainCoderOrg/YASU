using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private CharacterController2D _character;
    [SerializeField] private float _movementMultiplier = 5;
    [SerializeField] private float _movement;
    [SerializeField] private bool _jump;
    [SerializeField] private bool _crouch;
    [SerializeField] private Vector2 _aim;
    private bool _aiming;
    [SerializeField] private CharacterAimController _aimController;

    void Update()
    {
        
        if (_aiming)
        {
            _aimController.AimWithController(_aim);
        }
    }

    void FixedUpdate()
    {
        _character.Move(_movement, _crouch, _jump);
        _jump = false;
    }

    public void HandleMoveInput(CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            _movement = callbackContext.ReadValue<float>()*_movementMultiplier;
        }
        else if (callbackContext.canceled)
        {
            _movement = 0;
        }
    }

    public void HandleFire(CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            if(callbackContext.ReadValueAsButton())
            {
                _aimController.Fire();
            }
        }
    }

    public void HandleJumpInput(CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            _jump = callbackContext.ReadValueAsButton();
        }
        else if (callbackContext.canceled)
        {
            _jump = false;
        }
    }

    public void HandleCrouch(CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            _crouch = callbackContext.ReadValueAsButton();
        }
        else if (callbackContext.canceled)
        {
            _crouch = false;
        }
    }

    public void HandleAim(CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            _aiming = true;
            _aim = callbackContext.ReadValue<Vector2>().normalized;
        }
        else if (callbackContext.canceled)
        {
            _aim = Vector2.zero;
            _aiming = false;
            _aimController.AimWithController(_aim);
        }
    }
}
