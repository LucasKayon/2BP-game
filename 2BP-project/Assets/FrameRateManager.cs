using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    void Start()
    {
        // Set target frame rate to 60
        Application.targetFrameRate = 60;
    }
}