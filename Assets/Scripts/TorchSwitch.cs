using UnityEngine;

public class TorchSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _flame;

    public bool IsEnable
    {
        get => _flame.activeSelf;
        set => _flame.SetActive(value);
    }

    private void Start()
    {
        IsEnable = false;
    }
}
