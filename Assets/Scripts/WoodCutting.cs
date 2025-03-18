using UnityEngine;

public class WoodCutting : MonoBehaviour
{

    [SerializeField] GameObject woodPiecePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float growRate = 0.5f;
    float time;
    bool isReady;

    float initialYScale;

    private void Start()
    {
        initialYScale = transform.localScale.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Axe") && isReady)
        {
            isReady = false;
            Instantiate(woodPiecePrefab, spawnPoint.position, Quaternion.identity);
            transform.localScale = new Vector3(transform.localScale.x, initialYScale * 0.5f, transform.localScale.z);
        }
    }

    private void Update()
    {

        if (transform.localScale.y < initialYScale)
        {
            time += Time.deltaTime;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(initialYScale * 0.5f, initialYScale, growRate * time), transform.localScale.z);
            isReady = false;
        }
        else
        {
            time = 0;
            isReady = true;
        }
    }
}
