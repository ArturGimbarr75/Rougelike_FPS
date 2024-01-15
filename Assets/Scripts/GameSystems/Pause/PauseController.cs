using UnityEngine;

public class PauseController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseSystem.IsPaused)
				PauseSystem.Unpause();
			else
				PauseSystem.Pause();
		}
    }
}
