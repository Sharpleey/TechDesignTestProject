using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �������� �������� �� ������������ ����, ������������ ����, ����� ����
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
    /// ����� ��� ����������� �������� �����
    /// </summary>
    /// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
    /// <returns></returns>
    private async Task LoadAsyncScene(string sceneName)
	{
		// ���������� �������� ��������� ������ ��������
		_loadingScreen?.Show();

		// ������������� ���������� ���������� ���� ���� �� �������� �������� ������ ������ ��������
		if(_loadingScreen != null)
			while (_loadingScreen.IsShowing)
			{
				await Task.Yield();
			}

		// ��������� ����� � ����������� ������
		_asyncOperationLoadingScene = SceneManager.LoadSceneAsync(sceneName);

		// ������������� ���������� ���������� ����, ���� ���� �������� �����
		while (!_asyncOperationLoadingScene.isDone)
		{
			_loadingScreen?.SetValueProgressBar(_asyncOperationLoadingScene.progress);

			await Task.Yield();
		}

		//
		// ����� �������� ���������� �������������� ������� �� �����
		//

		// ������� ���� � �����, ���� ��� ���� �� �����
		if (_isGamePaused)
			PauseGame(false);

		// ������� �������� ������ ��������
		_loadingScreen.Hide();

		// �������� ������ �������� �������� �����
		_asyncOperationLoadingScene = null;
	}

	#endregion Private methods

	#region Public methods

	/// <summary>
	/// ����� ������ ���� �� �����
	/// </summary>
	/// <param name="isPaused">��������� ���� �� ����� ��� ���</param>
	public void PauseGame(bool isPaused)
	{
		_isGamePaused = isPaused;

		Time.timeScale = isPaused ? 0 : 1;
	}

	/// <summary>
	/// ����� ����� �����
	/// </summary>
	/// <param name="scene">C����, �� ������� ���������� �������</param>
	public void SwitchToScene(Scene scene)
    {
		SwitchToScene(_scenes[scene]);
	}

	/// <summary>
	/// ����� ����� �����
	/// </summary>
	/// <param name="sceneName">�������� �����, �� ������� ���������� �������</param>
	public async void SwitchToScene(string sceneName)
	{
		_currentScene = sceneName;

		await LoadAsyncScene(_currentScene);
	}

	/// <summary>
	/// ����� ������������� ������� ���������� �����
	/// </summary>
	public async void RestartScene()
    {
		await LoadAsyncScene(_currentScene);
	}

    #endregion Public methods
}
