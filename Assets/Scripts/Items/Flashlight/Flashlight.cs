using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light _light;

    private void Awake()
    {
        enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _light.enabled = !_light.enabled;
    }
}
