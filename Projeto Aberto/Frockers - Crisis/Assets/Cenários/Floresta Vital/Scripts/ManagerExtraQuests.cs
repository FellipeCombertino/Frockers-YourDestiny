using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerExtraQuests : MonoBehaviour
{
    public GameObject loui;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnableLoui();
        SkipQuest4and5();
        SkipQuest10();
    }

    void SkipQuest10()
    {
        if (Npc.currentNPCForQuest == 10)
        {
            Npc.currentNPCForQuest = 11;
        }
    }

    void SkipQuest4and5()
    {
        if (Npc.currentNPCForQuest == 4)
        {
            Npc.currentNPCForQuest = 6;
        }
    }

    void EnableLoui()
    {
        if (Npc.currentNPCForQuest == 2)
        {
            loui.SetActive(true);
        }
        else
        {
            loui.SetActive(false);
        }
    }
}
