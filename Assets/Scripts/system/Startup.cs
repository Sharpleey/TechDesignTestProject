using UnityEngine;
using Zenject;

public class Startup : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;
    private AudioManager _audioManager;

    [Inject]
    private void Construct(GameSceneManager gameSceneManager, AudioManager audioManager)
    {
        _gameSceneManager = gameSceneManager;
        _audioManager = audioManager;
    }

    private void Start()
    {
        _audioManager?.PlaySound(AudioManager.SoundType.AmbientMusic);

        _gameSceneManager.SwitchToScene(GameSceneManager.Scene.MainScene);
    }
}
