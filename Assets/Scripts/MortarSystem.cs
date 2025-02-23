using UnityEngine;

public class MortarSystem : MonoBehaviour
{

    [SerializeField] private LayerMask crystalLayer;
    [SerializeField] private GameObject crystalBitsPrefab;
    [SerializeField] private float crystalGrindTime = 10f;
    [SerializeField] private int maxCrystalBits = 100;
    [SerializeField] private float crystalBitsRate = 0.1f;

    private bool isPestleColliding;
    private float timer;
    private int counter;

    private void Start()
    {
        
    }

    private void Update()
    {
        
        Collider[] crystals = Physics.OverlapSphere(transform.position, 0.5f, crystalLayer);
        
        if(crystals.Length > 0 && isPestleColliding)
        {
            timer += Time.deltaTime;
            // scale crystals[0] down by lerping over a set period of time
            crystals[0].transform.localScale = Vector3.Lerp(crystals[0].transform.localScale, Vector3.zero, Time.deltaTime / crystalGrindTime);
            if (timer >= crystalBitsRate)
            {
                Instantiate(crystalBitsPrefab, crystals[0].transform.position, Quaternion.identity);
                timer = 0;
                counter++;
            }
            else if (timer >= crystalGrindTime || counter >= maxCrystalBits)
            {
                Destroy(crystals[0].gameObject);
                crystals = new Collider[0];
                timer = 0;
            }
        }

    }

    public void TogglePestleCollision(bool state)
    {
        isPestleColliding = state;
    }

}
