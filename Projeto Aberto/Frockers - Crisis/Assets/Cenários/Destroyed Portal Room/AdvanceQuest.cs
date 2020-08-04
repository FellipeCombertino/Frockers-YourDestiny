using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceQuest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Npc.currentNPCForQuest == 19)
            {
                Npc.currentNPCForQuest = 20;
            }
        }
    }
}
