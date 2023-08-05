using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Менеджер отвечает за переключение сцен, перезагрузку сцен, паузу игры
/// </summary>
public class GameSceneManager: MonoBehaviour
{
	[SerializeField] private GameSceneManagerConfig _config;
	[SerializeField] private LoadingScreenController _loadingScreen;

	public enum Scene
	{
		MainScene,
		Level_1,
	}

	public class HashNameScene
    {
		public const string MAIN_SCENE = "main_scene";
		public const string LEVEL_1 = "level_1";
	}

	#region Private fields
	private AsyncOperation _asyncOperationLoadingScene;
	private string _currentScene;
	private bool _isGamePaused;

	private Dictionary<Scene, string> _scenes;
    #endregion

    private void Awake()
    {
		_scenes = new Dictionary<Scene, string>();

		_scenes.Add(Scene.MainScene, HashNameScene.MAIN_SCENE);
		_scenes.Add(Scene.Level_1, HashNameScene.LEVEL_1);
	}


    #region Private methods

    /// <summary>
    /// Метод для аминхронной загрузки сцены
    /// </summary>
    /// <param name="sceneName">Название сцены, на которую необходимо перейти</param>
    /// <returns></returns>
    private async Task LoadAsyncScene(string sceneName)
	{
		// Показываем анимацию появления экрана загрузки
		_loadingScreen?.Show();

		// Останавливаем дальнейшее выполнение кода пока не окончена анимация показа экрана загрузки
		if(_loadingScreen != null)
			while (_loadingScreen.IsShowing)
			{
				await Task.Yield();
			}

		// Загружаем сцену в асинхронном режиме
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(sceneName);

		// Останавливаем дальнейшее выполнение кода, пока идет загрузка сцены
		while (!_asyncOperationLoadingScene.isDone)
		{
			_loadingScreen?.SetValueProgressBar(_asyncOperationLoadingScene.progress);

			await Task.Yield();
		}

		//
		// После загрузки происходит автоматический переход на сцену
		//

		// Снимаем игру с паузы, если она была на паузе
		if (_isGamePaused)
			PauseGame(false);

		// Плавное скрываем экрана загрузки
		_loadingScreen.Hide();

		// Обнуляем данные операции загрузки сцены
		_asyncOperationLoadingScene = null;
	}

	#endregion Private methods

	#region Public methods

	/// <summary>
	/// Метод ставит игру на паузу
	/// </summary>
	/// <param name="isPaused">Поставить игру на паузу или нет</param>
	public void PauseGame(bool isPaused)
	{
		_isGamePaused = isPaused;

		Time.timeScale = isPaused ? 0 : 1;
	}

	/// <summary>
	/// Метод смены сцены
	/// </summary>
	/// <param name="scene">Cцена, на которую необходимо перейти</param>
	public void SwitchToScene(Scene scene)
    {
		SwitchToScene(_scenes[scene]);
	}

	/// <summary>
	/// Метод смены сцены
	/// </summary>
	/// <param name="sceneName">Название сцены, на которую необходимо перейти</param>
	public async void SwitchToScene(string sceneName)
	{
		_currentScene = sceneName;

		await LoadAsyncScene(_currentScene);
	}

	/// <summary>
	/// Метод перезапускает текущую запущенную сцену
	/// </summary>
	public async void RestartScene()
    {
		await LoadAsyncScene(_currentScene);
	}

    #endregion Public methods
}
