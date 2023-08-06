using UnityEngine;

[CreateAssetMenu(menuName = "ManagerConfig/AudioManagerConfig", fileName = "AudioManagerConfig", order = 0)]
public class AudioManagerConfig : ScriptableObject
{
    [SerializeField] private AudioClip _ambientMusic;
    [SerializeField] private AudioClip _chestCollected;
    [SerializeField] private AudioClip _torchSwitch;
    [SerializeField] private AudioClip _buttonSelected;
    [SerializeField] private AudioClip _buttonClick;

    public AudioClip AmbientMusic => _ambientMusic;
    public AudioClip ChestCollected => _chestCollected;
    public AudioClip TorchSwitch => _torchSwitch;
    public AudioClip ButtonSelected => _buttonSelected;
    public AudioClip ButtonClick => _buttonClick;
}
