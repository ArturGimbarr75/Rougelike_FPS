using UnityEngine.SceneManagement;

public static partial class SceneSwitcher
{
	public static void LoadMovementTestScene()
	{
		LoadScene(SceneNames.TEST_MOVEMENT);
	}

	public static void LoadPauseTestScene()
	{
		LoadScene(SceneNames.TEST_PAUSE);
	}

	public static void LoadHealthDamageTestScene()
	{
		LoadScene(SceneNames.TEST_HEALTH_DAMAGE);
	}

	public static void LoadGameOverTestScene()
	{
		LoadScene(SceneNames.TEST_GAME_OVER);
	}
}
