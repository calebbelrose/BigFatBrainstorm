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
    [SerializeField] private Text gameOverScore;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject shark;
    [SerializeField] private Timer timer;
    private List<GameObject> fish = new List<GameObject>();
    private int fishRemaining = 0;
    private int score;
    private int fishToSpawn;
    private float timeRemaining;
    private bool timerIsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
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
        else
        {
            gameOverScore.text = "YOU SCORED " + score;
            shark.SetActive(false);
            gameOverPanel.SetActive(true);

            while (fish.Count > 0)
            {
                Destroy(fish[0]);
                fish.RemoveAt(0);
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

    public void Reset()
    {
        timeRemaining = 30f;
        fishToSpawn = 0;
        timerIsRunning = true;
        score = 0;
        scoreText.text = "0";
        shark.SetActive(true);
        timer.enabled = true;

        Spawn();
    }
}
