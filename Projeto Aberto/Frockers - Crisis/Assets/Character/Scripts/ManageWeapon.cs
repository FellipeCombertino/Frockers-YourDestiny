using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapon : MonoBehaviour
{

    public GameObject picaretaNormalAtk, picaretaMelhoradaAtk;
    public MeshRenderer picaretaNormal, picaretaMelhorada;
    bool atacando;

    // Update is called once per frame
    void Update()
    {

        atacando = picaretaNormalAtk.activeInHierarchy == true || picaretaMelhoradaAtk.activeInHierarchy == true;

        if (Npc.currentNPCForQuest > 11)
        {
            if (atacando)
            {
                picaretaNormalAtk.SetActive(false);
                picaretaMelhoradaAtk.SetActive(true);
            }
            else
            {
                picaretaNormal.enabled = false;
                picaretaMelhorada.enabled = true;
            }
        }
        else
        {
            if (atacando)
            {
                picaretaNormalAtk.SetActive(true);
                picaretaMelhoradaAtk.SetActive(false);
            }
            else
            {
                picaretaNormal.enabled = true;
                picaretaMelhorada.enabled = false;
            }
        }
    }
}
