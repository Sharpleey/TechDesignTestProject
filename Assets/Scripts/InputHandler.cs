using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _camera;

    private RaycastHit2D _rayHit;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        _rayHit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!_rayHit.collider)
            return;

        CheckClickedOnTorch();
        CheckClickedOnTreasureChest();
    }

    private void CheckClickedOnTorch()
    {
        if (_rayHit.collider.TryGetComponent(out TorchSwitch torchSwitch))
            torchSwitch.IsEnable = !torchSwitch.IsEnable;
    }

    private void CheckClickedOnTreasureChest()
    {
        if (_rayHit.collider.TryGetComponent(out TreasureChest treasureChest))
            treasureChest.Open();
    }
}
