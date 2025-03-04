using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

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

    Collider[] crystals;
    List<Collider> crystalList = new List<Collider>();


    private void Start()
    {
        
    }

    private void Update()
    {
        
        crystals = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (Collider crystal in crystals)
        {
            if (crystal.CompareTag("Crystal"))
            {
                crystalList.Add(crystal);
            }
        }

        crystalList.Sort((x, y) => { return (transform.position - x.transform.position).sqrMagnitude.CompareTo((transform.position - y.transform.position).sqrMagnitude); });

        if (crystalList.Count > 0 && isPestleColliding)
        {
            timer += Time.deltaTime;
            // scale crystals[0] down by lerping over a set period of time
            crystalList[0].transform.localScale = Vector3.Lerp(crystalList[0].transform.localScale, Vector3.zero, Time.deltaTime / crystalGrindTime);
            if (timer >= crystalBitsRate)
            {
                GameObject temp = Instantiate(crystalBitsPrefab, crystalList[0].transform.position, Quaternion.identity, transform.parent);
                temp.transform.localScale = Vector3.one;
                temp.GetComponentInChildren<MeshRenderer>().material = crystalList[0].GetComponent<MeshRenderer>().material;
                timer = 0;
                counter++;
            }
            else if (timer >= crystalGrindTime || counter >= maxCrystalBits)
            {
                Destroy(crystalList[0].gameObject);
                crystalList.Clear();
                timer = 0;
                counter = 0;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pestle"))
        {
            isPestleColliding = true;
        }

        if (other.transform.GetComponentInParent<XRGrabInteractable>())
        {
            Vector3 baseScale = other.transform.parent.lossyScale;
            other.transform.parent.parent = transform.parent;
            other.transform.parent.localScale = baseScale;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pestle"))
        {
            isPestleColliding = false;
        }

        if(other.transform.GetComponentInParent<XRGrabInteractable>())
        {
            Vector3 baseScale = other.transform.parent.lossyScale;
            other.transform.parent.parent = null;
            other.transform.parent.localScale = baseScale;
        }
    }



}
