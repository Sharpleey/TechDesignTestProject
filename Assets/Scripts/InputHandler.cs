using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("OnClick");

        if (!context.started)
            return;

        RaycastHit2D rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit.collider)
            return;

        if(rayHit.collider.TryGetComponent<TorchSwitch>(out TorchSwitch torchSwitch))
            torchSwitch.IsEnable = !torchSwitch.IsEnable;
;    }
}
