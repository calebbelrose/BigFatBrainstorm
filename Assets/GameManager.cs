using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float TimeRemaining { get { return timeRemaining; } }
    public bool TimerIsRunning { get { return timerIsRunning; } }

    [SerializeField] private GameObject correctFish;
    [SerializeField] private GameObject wrongFish;
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreText;
    private List<GameObject> fish = new List<GameObject>();
    private int fishRemaining = 0;
    private int score = 0;
    private int fishToSpawn = 0;
    private float timeRemaining = 30;
    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        Spawn();
    }

    void Spawn()
    {
        fishRemaining = ++fishToSpawn;

        for (int i = 0; i < fishToSpawn; i++)
        {
            fish.Add(Instantiate(correctFish));
            fish.Add(Instantiate(wrongFish));
        }
    }

    // Updates time remaining
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
                timeRemaining -= Time.deltaTime;
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    // Displays time
    public void DisplayTime(float _timeToDisplay)
    {
        _timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(_timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(_timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EatFish(GameObject _fish)
    {
        if (_fish.CompareTag("Correct"))
        {
            Destroy(_fish.gameObject);
            timeRemaining += 15f;
            fishRemaining--;
            score++;
            scoreText.text = score.ToString();
        }
        else if (_fish.CompareTag("Wrong"))
        {
            Destroy(_fish.gameObject);
            timeRemaining -= 10f;
            score--;
            scoreText.text = score.ToString();
        }

        if (fishRemaining == 0)
            Spawn();
    }
}
