using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabWeapon : MonoBehaviour
{
    public GameObject pickAxeToAttack;
    bool isArmed;
    MeshRenderer myMesh;

    private void Start()
    {
        myMesh = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PunhoPlayer"))
        {            
            if (!isArmed)
            {
                pickAxeToAttack.SetActive(true);
                myMesh.enabled = false;
            }
            else
            {
                pickAxeToAttack.SetActive(false);
                myMesh.enabled = true;
            }
            isArmed = !isArmed;
        }
    }
}
