using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    public GameObject boxBreak;
    public int life;
    public AudioSource fonteSom;
    public AudioClip somPicareta;
    public BlakeController blakeCheck;

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Instantiate(boxBreak, transform.position, transform.rotation);
            Npc.currentNPCForQuest = 18;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Npc.currentNPCForQuest == 17)
        {        
            if (other.gameObject.CompareTag("PicaretaPlayer"))
            {
                if (blakeCheck.blakeCurrentState == BlakeController.BlakeState.AttackingWeapon)
                {
                    life--;
                    if (!fonteSom.isPlaying)
                    {
                        fonteSom.PlayOneShot(somPicareta);
                    }
                }
            }
        }
    }
}
