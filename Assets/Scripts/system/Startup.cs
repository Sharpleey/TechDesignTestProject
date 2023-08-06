using UnityEngine;
using Zenject;

public class Startup : MonoBehaviour
{
    private GameSceneManager _gameSceneManager;

    [Inject]
    private void Construct(GameSceneManager gameSceneManager)
    {
        _gameSceneManager = gameSceneManager;
    }

    private void Start()
    {
        _gameSceneManager.SwitchToScene(GameSceneManager.Scene.MainScene);
    }
}
