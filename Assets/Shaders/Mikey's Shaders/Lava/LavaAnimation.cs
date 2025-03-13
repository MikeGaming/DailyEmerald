using UnityEngine;

public class LavaAnimation : MonoBehaviour
{
    [SerializeField] float fillTime, fillDelay, moveTime, moveDelay;
    [SerializeField] float fadeTime, fadeDelay;
    [SerializeField] Vector3 movement;
    MeshRenderer meshRenderer;
    [SerializeField] Oven oven;
    float time;

    Vector3 startPos, endPos;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        startPos = transform.position;
        endPos = startPos + movement;
    }

    private void Update()
    {
        if (oven.melted)
        {
            time += Time.deltaTime;
            // Fill "_Fill" property of the shader to 1 after fillDelay seconds, over the course of fillTime seconds
            float fill = Mathf.Clamp01((time - fillDelay) / fillTime);
            meshRenderer.material.SetFloat("_Fill", fill);
            // Move the object from startPos to endPos after moveDelay seconds, over the course of moveTime seconds
            float move = Mathf.Clamp01((time - moveDelay) / moveTime);
            transform.position = Vector3.Lerp(startPos, endPos, move);
            // Fill the "_Fill" property of the shader to 0 after fadeDelay seconds, over the course of fadeTime seconds
            if (time > fadeDelay)
            {
                float fade = Mathf.Clamp01((time - fadeDelay) / fadeTime);

                meshRenderer.material.SetFloat("_Fill", 1 - fade);
            }

        }
        else
        {
            time = 0;
        }
    }
}
