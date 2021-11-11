using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRHandController : MonoBehaviour
{
    [SerializeField] private InputActionReference controllerActionTrigger;
    [SerializeField] private InputActionReference controllerActionTouch;
    [SerializeField] private InputActionReference controllerActionGrip;

    public float thumbMoveSpeed = 0.1f;
    private bool touchIsPressed;
    private float thumbValue;
    private Animator _handAnimator;

    private void Awake()
    {
        SubscribeAll(this.controllerActionTrigger.action, OnTriggerChanged);
        SubscribeAll(this.controllerActionTouch.action, OnTouchChanged);
        SubscribeAll(this.controllerActionGrip.action, OnGripChanged);

        this._handAnimator = GetComponent<Animator>();
    }

    private void SubscribeAll(InputAction inputEvent, Action<InputAction.CallbackContext> subscriberMethod)
    {
        inputEvent.started += subscriberMethod;
        inputEvent.performed += subscriberMethod;
        inputEvent.canceled += subscriberMethod;
    }

    private void Update()
    {
        AnimateHand();
    }

    private void OnGripChanged(InputAction.CallbackContext obj)
    {
        var val = obj.ReadValue<float>();
        Debug.Log("grip:" + val);
        _handAnimator.SetFloat("ThreeFingers", val);
    }

    private void OnTouchChanged(InputAction.CallbackContext obj) =>
        this.touchIsPressed = obj.ReadValue<float>() > 0;

    private void OnTriggerChanged(InputAction.CallbackContext obj) =>
        _handAnimator.SetFloat("Index", obj.ReadValue<float>());

    private void AnimateHand()
    {
        if (this.touchIsPressed)
        {
            this.thumbValue += this.thumbMoveSpeed;
        }
        else
        {
            this.thumbValue -= this.thumbMoveSpeed;
        }

        this.thumbValue = Mathf.Clamp(this.thumbValue, 0, 1);

        this._handAnimator.SetFloat("Thumb", this.thumbValue);
    }
}
