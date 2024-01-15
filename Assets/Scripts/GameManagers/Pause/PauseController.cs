using UnityEngine;

public class PauseController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseManager.IsPaused)
				PauseManager.Unpause();
			else
				PauseManager.Pause();
		}
    }
}
