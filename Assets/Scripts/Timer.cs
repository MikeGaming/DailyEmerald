using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] bool isInfinite;
    [SerializeField] float duration;
    [SerializeField] TMPro.TextMeshProUGUI timerText;
    [SerializeField] CustomerManager customerManager;
    [SerializeField] MusicManager music;
    float t, musicTimer;
    bool isPaused;
    [HideInInspector] public bool timerFinished;

    [Header("Tutorial")]
    [SerializeField] private bool isTutorial;

    private void Start()
    {
        if (isInfinite) t = 0;
        else t = duration;
        music.SetIntensity(0);

        isPaused = isTutorial;
    }

    private void Update()
    {
        if (isInfinite && !isPaused)
        {
            t += Time.deltaTime;
            musicTimer += Time.deltaTime;
            music.SetIntensity(musicTimer / 60);
        }
        else if (!isPaused)
        {
            t -= Time.deltaTime;
            musicTimer += Time.deltaTime;
            music.SetIntensity(musicTimer / 60);
        }

        if (t <= 0 && !timerFinished)
        {
            timerFinished = true;
            t = 0;
            isPaused = true;
            customerManager.EndGame();
        }

        if (timerText != null)
        {
            // Display the time in minutes and seconds
            timerText.text = Mathf.Floor(t / 60).ToString("00") + ":" + Mathf.Floor(t % 60).ToString("00");
        }
    }

    public void ResetTimer()
    {
        if (isInfinite)
        {
            t = 0;
            musicTimer = 0;
        }
        else t = duration;
    }

    public float GetTime()
    {
        return t;
    }
}
