using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    // Updates timer
    void Update()
    {
        if (gameManager.TimerIsRunning)
            gameManager.DisplayTime(gameManager.TimeRemaining);
        else
            Destroy(this);
    }
}