using UnityEngine.SceneManagement;

public static partial class SceneSwitcher
{
	public static void RestartScene()
	{
		PauseSystem.Unpause();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public static void LoadMainMenu()
	{
		LoadScene(SceneNames.MAIN_MENU);
	}

	private static void LoadScene(string sceneName)
	{
		PauseSystem.Unpause();
		SceneManager.LoadScene(sceneName);
	}
}
