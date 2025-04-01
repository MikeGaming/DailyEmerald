using UnityEngine;

public class LavaAnimation : MonoBehaviour
{
    [SerializeField] float fillTime, fillDelay, moveTime, moveDelay;
    [SerializeField] float fadeTime, fadeDelay;
    [SerializeField] Vector3 movement;
    MeshRenderer meshRenderer;
    [SerializeField] LavaAnimation nextAnim;
    [SerializeField] bool isLastAnim;
    public bool canStart;
    float time, lavaTimer;
    int lavaCount;

    [SerializeField] Transform lavaSpawnPoint;
    [SerializeField] GameObject lavaPrefab;
    [SerializeField] Oven oven;

    Vector3 startPos, endPos;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        startPos = transform.position;
        endPos = startPos + movement;
    }

    private void Update()
    {
        if (canStart)
        {
            time += Time.deltaTime;
            // Fill "_Fill" property of the shader to 1 after fillDelay seconds, over the course of fillTime seconds
            float fill = Mathf.Clamp01((time - fillDelay) / fillTime);
            meshRenderer.material.SetFloat("_Fill", fill);
            // Move the object from startPos to endPos after moveDelay seconds, over the course of moveTime seconds
            float move = Mathf.Clamp01((time - moveDelay) / moveTime);
            transform.position = Vector3.Lerp(startPos, endPos, move);
            // Fill the "_Fill" property of the shader to 0 after fadeDelay seconds, over the course of fadeTime seconds

            if (time > fillDelay + fillTime)
            {
                if (nextAnim != null) nextAnim.canStart = true;
            }

            if (time > fadeDelay)
            {
                float fade = Mathf.Clamp01((time - fadeDelay) / fadeTime);

                meshRenderer.material.SetFloat("_Fill", 1 - fade);
            }

            if (time > fadeDelay + fadeTime)
            {
                if (isLastAnim)
                {
                    time = 0;
                    canStart = false;
                }
                else
                {
                    time = 0;
                    canStart = false;
                }
            }

            if (isLastAnim)
            {
                if (lavaTimer >= 0.25f && lavaCount <= 100)
                {
                    GameObject temp = Instantiate(lavaPrefab, lavaSpawnPoint.position, Quaternion.identity);
                    temp.GetComponent<LavaBall>().materialType = oven.meltMaterial;
                    temp.transform.parent = lavaSpawnPoint;
                    lavaTimer = 0;
                    lavaCount++;
                }
                else
                {
                    lavaTimer += Time.deltaTime;
                }
            }

        }
        else
        {
            time = 0;
        }
    }
}
