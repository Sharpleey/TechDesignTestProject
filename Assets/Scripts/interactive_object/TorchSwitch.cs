using UnityEngine;
using Zenject;

public class TorchSwitch : MonoBehaviour
{
    [SerializeField] private GameObject _flame;

    private AudioManager _audioManager;

    public bool IsEnable
    {
        get => _flame.activeSelf;
        set
        {
            _audioManager?.PlaySound(AudioManager.SoundType.TorchSwitch);

            _flame.SetActive(value);
        }
    }

    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    private void Start()
    {
        _flame.SetActive(false);
    }
}
