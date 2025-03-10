using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class OreDeposit : MonoBehaviour
{
    [SerializeField] private Enums.MaterialType oreType;
    [SerializeField] private GameObject[] objStates;
    [SerializeField] private GameObject orePrefab;
    [SerializeField] private Transform oreSpawnPoint;
    [SerializeField] private float hitCooldown = 0.25f;
    [SerializeField] private float regenCooldown = 10f;

    private int currentState;
    private float lastHitTime;
    private MeshCollider meshCol;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pickaxe")
        {
            if (lastHitTime + hitCooldown > Time.realtimeSinceStartup) return;

            if (currentState != 4)
            {
                lastHitTime = Time.realtimeSinceStartup;
                Instantiate(orePrefab, oreSpawnPoint.position, oreSpawnPoint.rotation);
                ChangeState(true);
            }
        }
    }
    
    void ChangeState(bool decrease)
    {
        if (decrease)
        {
            currentState = Mathf.Clamp(currentState + 1, 0, 4);
        }
        else
        {
            currentState = Mathf.Clamp(currentState - 1, 0, 4);
        }

        ResetObjects();
        objStates[currentState].SetActive(true);
        meshCol.sharedMesh = objStates[currentState].GetComponent<MeshFilter>().sharedMesh;
    }

    void ResetObjects()
    {
        foreach(GameObject obj in objStates)
        {
            obj.SetActive(false);
        }
    }

    void Regenerate()
    {
        //if the deposit hasn't been hit in the past regenCooldown seconds, increase state by 1
        if(Time.realtimeSinceStartup > lastHitTime + regenCooldown)
        {
            ChangeState(false);
        }
    }

    private void Start()
    {
        //run Regenerate() every 5 seconds.
        InvokeRepeating(nameof(Regenerate), 2f, 5f);

        meshCol = GetComponent<MeshCollider>();
    }
}
