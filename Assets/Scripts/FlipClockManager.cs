using System.Collections;
using UnityEngine;

public class FlipClockManager : MonoBehaviour
{
    [SerializeField] GameObject[] wheels;

    int score;
    bool isSpinning;

    float[] turnSpeeds = { 0, 0, 0 };

    private void Start()
    {
        StartCoroutine(TurnWheels(172));
    }

    private void Update()
    {
        // spin the wheels frantically if isspinning is true
        if (isSpinning)
        {
                wheels[0].transform.Rotate(turnSpeeds[0] * Time.deltaTime, 0, 0);
                wheels[1].transform.Rotate(turnSpeeds[1] * Time.deltaTime, 0, 0);
                wheels[2].transform.Rotate(turnSpeeds[2] * Time.deltaTime, 0, 0);
        }
    }

    public IEnumerator TurnWheels(int gold)
    {
        isSpinning = true;
        for (int i = 0; i < 3; i++)
        {
            turnSpeeds[i] = Random.Range(1800, 3600) * Mathf.Sign(Random.Range(-1, 1));
        }
        yield return new WaitForSeconds(1.5f);
        isSpinning = false;
        score += gold;
        if (score > 999)
        {
            score = 0;
        }
        int[] digits = new int[3];
        digits[0] = (score % 1000) / 100;
        digits[1] = (score % 100) / 10;
        digits[2] = score % 10;
        for (int i = 0; i < 3; i++)
        {
            wheels[i].transform.localRotation = Quaternion.Euler(-36 * digits[i], 0, 0);
        }
    }

}
