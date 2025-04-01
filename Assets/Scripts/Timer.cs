using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] bool isInfinite;
    [SerializeField] float duration;
    [SerializeField] TMPro.TextMeshProUGUI timerText;
    [SerializeField] MusicManager music;
    float t, musicTimer;
    bool isPaused;
    [HideInInspector] public bool timerFinished;

    private void Start()
    {
        if (isInfinite) t = 0;
        else t = duration;
        music.SetIntensity(0);
    }

    private void Update()
    {
        if (isInfinite && !isPaused)
        {
            t += Time.deltaTime;
            musicTimer += Time.deltaTime;
            music.SetIntensity(Mathf.FloorToInt(musicTimer / 60));
        }
        else if (!isPaused)
        {
            t -= Time.deltaTime;
            musicTimer += Time.deltaTime;
            music.SetIntensity(Mathf.FloorToInt(musicTimer / 60));
        }

        if (t <= 0)
        {
            timerFinished = true;
        }
        else
        {
            timerFinished = false;
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
